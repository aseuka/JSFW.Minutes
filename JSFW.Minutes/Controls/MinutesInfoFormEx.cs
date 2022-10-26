using Greenshot;
using Greenshot.Destinations;
using Greenshot.Helpers;
using Greenshot.IniFile;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using GreenshotPlugin.UnmanagedHelpers;
using PopupControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.Minutes.Controls
{
    partial class MinutesInfoForm
    {  
        void CaptureWin()
        { 
            try
            { 
                CaptureHelper.Install(Handle);
                if (MainForm._conf.CaptureWindowsInteractive)
                {
                    CaptureHelper.CaptureWindowInteractive(true);
                }
                else
                {
                    CaptureHelper.CaptureWindow(true);
                }
            }
            finally
            {
                CaptureHelper.UnInstall();
                string dir = Unit.GetFileFolderName();
                string fileName = Path.GetFileName(MainForm._conf.OutputFileAsFullpath);

                //파일 카피
                if (File.Exists(MainForm._conf.OutputFileAsFullpath))
                {
                    File.Copy(MainForm._conf.OutputFileAsFullpath, dir + fileName, true);
                }

                //string[] imageFiles = new string[] { @"C:\Users\aseuk\Pictures\11.jpg", @"C:\Users\aseuk\Pictures\vex.PNG" };

                string imageFilePath = dir + fileName;

                using (Image img = Bitmap.FromFile(imageFilePath))
                {
                    pictureBox1.Image = img.Clone() as Image;
                }

                Record rec = new Record()
                {
                    Command = "CHAGE",
                    TypeName = "IMAGE",
                    TargetID = "",
                    Data = imageFilePath,
                    Tick = DateTime.Now.Ticks
                };

                if (string.IsNullOrWhiteSpace(EndTime))
                {
                    RecordView.Write(rtxChattings, rec);

                    if (rtxAttachFiles.Links.Any(l => l.Link == imageFilePath.Replace("\\", "\\\\")) == false)
                    {
                        //링크가 없는 것만 추가함... 
                        rtxAttachFiles.InsertLink(Path.GetFileName(imageFilePath.Replace("\\", "\\\\")), imageFilePath.Replace("\\", "\\\\"));
                        rtxAttachFiles.AppendText(Environment.NewLine);
                    }
                }

                if (IsRunnigTime())
                {
                    Records.Add(rec); IsDirty = true;
                    Info.Records.Add(rec);
                }

                IsDirty = true;
                Log("자료 변경", imageFilePath);

                IsDirty = true;
                this.Activate();
            }
        }
         
        private void CaptureIE()
        { 
            try
            { 
                CaptureHelper.Install(Handle);
                CaptureHelper.CaptureIe(true, null);
            }
            finally
            {
                CaptureHelper.UnInstall();
                string dir = Unit.GetFileFolderName();
                string fileName = Path.GetFileName(MainForm._conf.OutputFileAsFullpath);

                //파일 카피
                if (File.Exists(MainForm._conf.OutputFileAsFullpath))
                {
                    File.Copy(MainForm._conf.OutputFileAsFullpath, dir + fileName, true);
                }

                //string[] imageFiles = new string[] { @"C:\Users\aseuk\Pictures\11.jpg", @"C:\Users\aseuk\Pictures\vex.PNG" };

                string imageFilePath = dir + fileName;

                using (Image img = Bitmap.FromFile(imageFilePath))
                {
                    pictureBox1.Image = img.Clone() as Image;
                }

                Record rec = new Record()
                {
                    Command = "CHAGE",
                    TypeName = "IMAGE",
                    TargetID = "",
                    Data = imageFilePath,
                    Tick = DateTime.Now.Ticks
                };

                if (string.IsNullOrWhiteSpace(EndTime))
                {
                    RecordView.Write(rtxChattings, rec);

                    if (rtxAttachFiles.Links.Any(l => l.Link == imageFilePath.Replace("\\", "\\\\")) == false)
                    {
                        //링크가 없는 것만 추가함... 
                        rtxAttachFiles.InsertLink(Path.GetFileName(imageFilePath.Replace("\\", "\\\\")), imageFilePath.Replace("\\", "\\\\"));
                        rtxAttachFiles.AppendText(Environment.NewLine);
                    }
                }

                if (IsRunnigTime())
                {
                    Records.Add(rec); IsDirty = true;
                    Info.Records.Add(rec);
                }

                IsDirty = true;
                Log("자료 변경", imageFilePath);

                IsDirty = true;
                this.Activate();
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (HandleMessages(ref m))
            {
                return;
            }
            // BUG-1809 prevention, filter the InputLangChange messages
            if (PreFilterMessageExternal(ref m))
            {
                return;
            }
            base.WndProc(ref m);
        }
         
        public static void UnregisterHotkeys()
        {
            foreach (int hotkey in KeyHandlers.Keys)
            {
                UnregisterHotKey(_hotkeyHwnd, hotkey);
            }
            // Remove all key handlers
            KeyHandlers.Clear();
        }

        /// <summary>
        /// Also used in the MainForm WndProc
        /// </summary>
        /// <param name="m">Message</param>
        /// <returns>true if the message should be filtered</returns>
        public static bool PreFilterMessageExternal(ref Message m)
        {
            WindowsMessages message = (WindowsMessages)m.Msg;
            if (message == WindowsMessages.WM_INPUTLANGCHANGEREQUEST || message == WindowsMessages.WM_INPUTLANGCHANGE)
            {
                // For now we always return true
                return true;
                // But it could look something like this:
                //return (m.LParam.ToInt64() | 0x7FFFFFFF) != 0;
            }
            return false;
        }

        /// <summary>
        /// Handle WndProc messages for the hotkey
        /// </summary>
        /// <param name="m"></param>
        /// <returns>true if the message was handled</returns>
        public static bool HandleMessages(ref Message m)
        {
            if (m.Msg != WM_HOTKEY)
            {
                return false;
            }

            HotKeyHandler handler;
            if (KeyHandlers.TryGetValue((int)m.WParam, out handler))
            {
                handler();
            }
            return true;
        }
         
        // Delegates for hooking up events.
        public delegate void HotKeyHandler();

        private static bool RegisterWrapper(Keys modifierKeyCode, Keys virtualKeyCode, IntPtr _hotkeyHwnd, HotKeyHandler handler, bool ignoreFailedRegistration)
        {
            int success = RegisterHotKey(modifierKeyCode, virtualKeyCode, _hotkeyHwnd, handler);
            return 0 < success;
        }

        // Holds the list of hotkeys
        private static readonly IDictionary<int, HotKeyHandler> KeyHandlers = new Dictionary<int, HotKeyHandler>();
        private static int _hotKeyCounter = 1;
        private const uint WM_HOTKEY = 0x312;
        private static IntPtr _hotkeyHwnd;

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public enum Modifiers : uint
        {
            NONE = 0,
            ALT = 1,
            CTRL = 2,
            SHIFT = 4,
            WIN = 8,
            NO_REPEAT = 0x4000
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint virtualKeyCode);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int GetKeyNameText(uint lParam, [Out] StringBuilder lpString, int nSize);

        /// <summary>
        /// Register a hotkey
        /// </summary>
        /// <param name="modifierKeyCode">The modifier, e.g.: Modifiers.CTRL, Modifiers.NONE or Modifiers.ALT</param>
        /// <param name="virtualKeyCode">The virtual key code</param>
        /// <param name="handler">A HotKeyHandler, this will be called to handle the hotkey press</param>
        /// <returns>the hotkey number, -1 if failed</returns>
        public static int RegisterHotKey(Keys modifierKeyCode, Keys virtualKeyCode, IntPtr _hotkeyHwnd, HotKeyHandler handler)
        {
            if (virtualKeyCode == Keys.None)
            {

                return 0;
            }
            // Convert Modifiers to fit HKM_SETHOTKEY
            uint modifiers = 0;
            if ((modifierKeyCode & Keys.Alt) > 0)
            {
                modifiers |= (uint)Modifiers.ALT;
            }
            if ((modifierKeyCode & Keys.Control) > 0)
            {
                modifiers |= (uint)Modifiers.CTRL;
            }
            if ((modifierKeyCode & Keys.Shift) > 0)
            {
                modifiers |= (uint)Modifiers.SHIFT;
            }
            if (modifierKeyCode == Keys.LWin || modifierKeyCode == Keys.RWin)
            {
                modifiers |= (uint)Modifiers.WIN;
            }
            // Disable repeating hotkey for Windows 7 and beyond, as described in #1559
            //if (IsWindows7OrOlder)
            //{
            //    modifiers |= (uint)Modifiers.NO_REPEAT;
            //}
            if (RegisterHotKey(_hotkeyHwnd, _hotKeyCounter, modifiers, (uint)virtualKeyCode))
            {
                KeyHandlers.Add(_hotKeyCounter, handler);
                return _hotKeyCounter++;
            }
            else
            {
                return -1;
            }
        }

    }
}
