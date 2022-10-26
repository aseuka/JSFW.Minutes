using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using System.Collections.Generic;

namespace JSFW.Minutes.Controls
{
    /// <summary>
    /// https://www.codeproject.com/Articles/9196/Links-with-arbitrary-text-in-a-RichTextBox
    /// </summary>
    public class RichTextBoxEx : RichTextBox
    {
        [Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public List<HyperLink> Links { get; set; }

        #region Interop-Defines
        [StructLayout(LayoutKind.Sequential)]
        private struct CHARFORMAT2_STRUCT
        {
            public UInt32 cbSize;
            public UInt32 dwMask;
            public UInt32 dwEffects;
            public Int32 yHeight;
            public Int32 yOffset;
            public Int32 crTextColor;
            public byte bCharSet;
            public byte bPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szFaceName;
            public UInt16 wWeight;
            public UInt16 sSpacing;
            public int crBackColor; // Color.ToArgb() -> int
            public int lcid;
            public int dwReserved;
            public Int16 sStyle;
            public Int16 wKerning;
            public byte bUnderlineType;
            public byte bAnimation;
            public byte bRevAuthor;
            public byte bReserved1;
        }
         
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private const int WM_USER = 0x0400;
        private const int EM_GETCHARFORMAT = WM_USER + 58;
        private const int EM_SETCHARFORMAT = WM_USER + 68;

        private const int SCF_SELECTION = 0x0001;
        private const int SCF_WORD = 0x0002;
        private const int SCF_ALL = 0x0004;

        #region CHARFORMAT2 Flags
        private const UInt32 CFE_BOLD = 0x0001;
        private const UInt32 CFE_ITALIC = 0x0002;
        private const UInt32 CFE_UNDERLINE = 0x0004;
        private const UInt32 CFE_STRIKEOUT = 0x0008;
        private const UInt32 CFE_PROTECTED = 0x0010;
        private const UInt32 CFE_LINK = 0x0020;
        private const UInt32 CFE_AUTOCOLOR = 0x40000000;
        private const UInt32 CFE_SUBSCRIPT = 0x00010000;        /* Superscript and subscript are */
        private const UInt32 CFE_SUPERSCRIPT = 0x00020000;      /*  mutually exclusive			 */

        private const int CFM_SMALLCAPS = 0x0040;           /* (*)	*/
        private const int CFM_ALLCAPS = 0x0080;         /* Displayed by 3.0	*/ 
        private const int CFM_HIDDEN = 0x0100;          /* Hidden by 3.0 */
        private const int CFM_OUTLINE = 0x0200;         /* (*)	*/
        private const int CFM_SHADOW = 0x0400;          /* (*)	*/
        private const int CFM_EMBOSS = 0x0800;          /* (*)	*/
        private const int CFM_IMPRINT = 0x1000;         /* (*)	*/ 
        private const int CFM_DISABLED = 0x2000;
        private const int CFM_REVISED = 0x4000;

        private const int CFM_BACKCOLOR = 0x04000000;
        private const int CFM_LCID = 0x02000000;
        private const int CFM_UNDERLINETYPE = 0x00800000;       /* Many displayed by 3.0 */
        private const int CFM_WEIGHT = 0x00400000;
        private const int CFM_SPACING = 0x00200000;     /* Displayed by 3.0	*/
        private const int CFM_KERNING = 0x00100000;     /* (*)	*/
        private const int CFM_STYLE = 0x00080000;       /* (*)	*/
        private const int CFM_ANIMATION = 0x00040000;       /* (*)	*/
        private const int CFM_REVAUTHOR = 0x00008000;


        private const UInt32 CFM_BOLD = 0x00000001;
        private const UInt32 CFM_ITALIC = 0x00000002;
        private const UInt32 CFM_UNDERLINE = 0x00000004;
        private const UInt32 CFM_STRIKEOUT = 0x00000008;
        private const UInt32 CFM_PROTECTED = 0x00000010;
        private const UInt32 CFM_LINK = 0x00000020;
        private const UInt32 CFM_SIZE = 0x80000000;
        private const UInt32 CFM_COLOR = 0x40000000;
        private const UInt32 CFM_FACE = 0x20000000;
        private const UInt32 CFM_OFFSET = 0x10000000;
        private const UInt32 CFM_CHARSET = 0x08000000;
        private const UInt32 CFM_SUBSCRIPT = CFE_SUBSCRIPT | CFE_SUPERSCRIPT;
        private const UInt32 CFM_SUPERSCRIPT = CFM_SUBSCRIPT;

        private const byte CFU_UNDERLINENONE = 0x00000000;
        private const byte CFU_UNDERLINE = 0x00000001;
        private const byte CFU_UNDERLINEWORD = 0x00000002; /* (*) displayed as ordinary underline	*/
        private const byte CFU_UNDERLINEDOUBLE = 0x00000003; /* (*) displayed as ordinary underline	*/
        private const byte CFU_UNDERLINEDOTTED = 0x00000004;
        private const byte CFU_UNDERLINEDASH = 0x00000005;
        private const byte CFU_UNDERLINEDASHDOT = 0x00000006;
        private const byte CFU_UNDERLINEDASHDOTDOT = 0x00000007;
        private const byte CFU_UNDERLINEWAVE = 0x00000008;
        private const byte CFU_UNDERLINETHICK = 0x00000009;
        private const byte CFU_UNDERLINEHAIRLINE = 0x0000000A; /* (*) displayed as ordinary underline	*/

        #endregion

        #endregion

        public RichTextBoxEx()
        {
            // Otherwise, non-standard links get lost when user starts typing
            // next to a non-standard link
            this.DetectUrls = false;
            Links = new List<HyperLink>();
        }

        [DefaultValue(false)]
        public new bool DetectUrls
        {
            get { return base.DetectUrls; }
            set { base.DetectUrls = value; }
        }

        /// <summary>
        /// Insert a given text as a link into the RichTextBox at the current insert position.
        /// </summary>
        /// <param name="text">Text to be inserted</param>
        public void InsertLink(string text)
        {
            InsertLink(text, this.SelectionStart);
        }

        /// <summary>
        /// Insert a given text at a given position as a link. 
        /// </summary>
        /// <param name="text">Text to be inserted</param>
        /// <param name="position">Insert position</param>
        public void InsertLink(string text, int position)
        {
            if (position < 0 || position > this.Text.Length)
                throw new ArgumentOutOfRangeException("position"); 

            this.SelectionStart = position;
            this.SelectedText = text;
            this.Select(position, text.Length); 
            Links.Add(new HyperLink() { Position = position, Text = text, Link = text });
            this.SetSelectionLink(true);
            this.Select(position + text.Length, 0);
        }

        /// <summary>
        /// Insert a given text at at the current input position as a link.
        /// The link text is followed by a hash (#) and the given hyperlink text, both of
        /// them invisible.
        /// When clicked on, the whole link text and hyperlink string are given in the
        /// LinkClickedEventArgs.
        /// </summary>
        /// <param name="text">Text to be inserted</param>
        /// <param name="hyperlink">Invisible hyperlink string to be inserted</param>
        public void InsertLink(string text, string hyperlink)
        {
            InsertLink(text, hyperlink, this.SelectionStart);
        }

        /// <summary>
        /// Insert a given text at a given position as a link. The link text is followed by
        /// a hash (#) and the given hyperlink text, both of them invisible.
        /// When clicked on, the whole link text and hyperlink string are given in the
        /// LinkClickedEventArgs.
        /// </summary>
        /// <param name="text">Text to be inserted</param>
        /// <param name="hyperlink">Invisible hyperlink string to be inserted</param>
        /// <param name="position">Insert position</param>
        public void InsertLink(string text, string hyperlink, int position)
        {
            if (position < 0 || position > this.Text.Length)
                throw new ArgumentOutOfRangeException("position");
             
            this.SelectionStart = position;
            // 한글로 만들려면 : \ansicpg949\deff0\deflang1033\deflangfe1042 이거넹?
            // LinkText를 #으로 파싱해야 됨. 
            this.SelectedRtf = @"{\rtf1\ansi\ansicpg949\deff0\deflang1033\deflangfe1042 " + text + @"\v #" + hyperlink + @"\v0}";
            this.Select(position, text.Length + hyperlink.Length  + 1);
            this.SetSelectionLink(true);
            Links.Add(new HyperLink() { Position = position, Text = text, Link = hyperlink });
            this.Select(position + text.Length + hyperlink.Length + 1, 0);
            this.SelectedText = "  ";
        }

        /// <summary>
        /// Set the current selection's link style
        /// </summary>
        /// <param name="link">true: set link style, false: clear link style</param>
        public void SetSelectionLink(bool link)
        {
            SetSelectionStyle(CFM_LINK, link ? CFE_LINK : 0);
        }
        /// <summary>
        /// Get the link style for the current selection
        /// </summary>
        /// <returns>0: link style not set, 1: link style set, -1: mixed</returns>
        public int GetSelectionLink()
        {
            return GetSelectionStyle(CFM_LINK, CFE_LINK);
        }


        private void SetSelectionStyle(UInt32 mask, UInt32 effect)
        {
            CHARFORMAT2_STRUCT cf = new CHARFORMAT2_STRUCT();
            cf.cbSize = (UInt32)Marshal.SizeOf(cf);
            cf.dwMask = mask;
            cf.dwEffects = effect;

            IntPtr wpar = new IntPtr(SCF_SELECTION);
            IntPtr lpar = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
            Marshal.StructureToPtr(cf, lpar, false);

            IntPtr res = SendMessage(Handle, EM_SETCHARFORMAT, wpar, lpar);

            Marshal.FreeCoTaskMem(lpar);
        }

        private int GetSelectionStyle(UInt32 mask, UInt32 effect)
        {
            CHARFORMAT2_STRUCT cf = new CHARFORMAT2_STRUCT();
            cf.cbSize = (UInt32)Marshal.SizeOf(cf);
            cf.szFaceName = new char[32];

            IntPtr wpar = new IntPtr(SCF_SELECTION);
            IntPtr lpar = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
            Marshal.StructureToPtr(cf, lpar, false);

            IntPtr res = SendMessage(Handle, EM_GETCHARFORMAT, wpar, lpar);

            cf = (CHARFORMAT2_STRUCT)Marshal.PtrToStructure(lpar, typeof(CHARFORMAT2_STRUCT));

            int state;
            // dwMask holds the information which properties are consistent throughout the selection:
            if ((cf.dwMask & mask) == mask)
            {
                if ((cf.dwEffects & effect) == effect)
                    state = 1;
                else
                    state = 0;
            }
            else
            {
                state = -1;
            }

            Marshal.FreeCoTaskMem(lpar);
            return state;
        }

        /// <summary>
        /// 참고 URL : https://stackoverflow.com/questions/18017044/insert-image-at-cursor-position-in-rich-text-box
        /// </summary>
        /// <param name="image"></param>
        internal void InsertImage(Image image)
        {
            var rtf = new System.Text.StringBuilder(); 
            // Append the RTF header
            rtf.Append(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033");
            // Create the font table using the RichTextBox's current font and append
            // it to the RTF string
            rtf.Append(GetFontTable(this.Font));
            // Create the image control string and append it to the RTF string
            rtf.Append(GetImagePrefix(image));
            // Create the Windows Metafile and append its bytes in HEX format
            rtf.Append(getRtfImage(image));
            // Close the RTF image control string  
            rtf.Append(@"}");
            SelectedRtf = rtf.ToString(); 
        }

        private enum EmfToWmfBitsFlags
        {
            EmfToWmfBitsFlagsDefault = 0x00000000,
            EmfToWmfBitsFlagsEmbedEmf = 0x00000001,
            EmfToWmfBitsFlagsIncludePlaceable = 0x00000002,
            EmfToWmfBitsFlagsNoXORClip = 0x00000004
        };

        private struct RtfFontFamilyDef
        {
            public const string Unknown = @"\fnil";
            public const string Roman = @"\froman";
            public const string Swiss = @"\fswiss";
            public const string Modern = @"\fmodern";
            public const string Script = @"\fscript";
            public const string Decor = @"\fdecor";
            public const string Technical = @"\ftech";
            public const string BiDirect = @"\fbidi";
        }

        [DllImport("gdiplus.dll")]
        private static extern uint GdipEmfToWmfBits(IntPtr _hEmf,
          uint _bufferSize, byte[] _buffer,
          int _mappingMode, EmfToWmfBitsFlags _flags);


        private string GetFontTable(Font font)
        {
            var fontTable = new System.Text.StringBuilder();
            // Append table control string
            fontTable.Append(@"{\fonttbl{\f0");
            fontTable.Append(@"\");
            var rtfFontFamily = new System.Collections.Specialized.HybridDictionary();
            rtfFontFamily.Add(FontFamily.GenericMonospace.Name, RtfFontFamilyDef.Modern);
            rtfFontFamily.Add(FontFamily.GenericSansSerif, RtfFontFamilyDef.Swiss);
            rtfFontFamily.Add(FontFamily.GenericSerif, RtfFontFamilyDef.Roman);
            rtfFontFamily.Add("UNKNOWN", RtfFontFamilyDef.Unknown);

            // If the font's family corresponds to an RTF family, append the
            // RTF family name, else, append the RTF for unknown font family.
            fontTable.Append(rtfFontFamily.Contains(font.FontFamily.Name) ? rtfFontFamily[font.FontFamily.Name] : rtfFontFamily["UNKNOWN"]);
            // \fcharset specifies the character set of a font in the font table.
            // 0 is for ANSI.
            fontTable.Append(@"\fcharset0 ");
            // Append the name of the font
            fontTable.Append(font.Name);
            // Close control string
            fontTable.Append(@";}}");
            return fontTable.ToString();
        }

        private string GetImagePrefix(Image _image)
        {
            float xDpi, yDpi;
            var rtf = new System.Text.StringBuilder();
            using (Graphics graphics = CreateGraphics())
            {
                xDpi = graphics.DpiX;
                yDpi = graphics.DpiY;
            }
            // Calculate the current width of the image in (0.01)mm
            var picw = (int)Math.Round((_image.Width / xDpi) * 2540);
            // Calculate the current height of the image in (0.01)mm
            var pich = (int)Math.Round((_image.Height / yDpi) * 2540);
            // Calculate the target width of the image in twips
            var picwgoal = (int)Math.Round((_image.Width / xDpi) * 1440);
            // Calculate the target height of the image in twips
            var pichgoal = (int)Math.Round((_image.Height / yDpi) * 1440);
            // Append values to RTF string
            rtf.Append(@"{\pict\wmetafile8");
            rtf.Append(@"\picw");
            rtf.Append(picw);
            rtf.Append(@"\pich");
            rtf.Append(pich);
            rtf.Append(@"\picwgoal");
            rtf.Append(picwgoal);
            rtf.Append(@"\pichgoal");
            rtf.Append(pichgoal);
            rtf.Append(" ");

            return rtf.ToString();
        }

        private string getRtfImage(Image image)
        {
            // Used to store the enhanced metafile
            MemoryStream stream = null;
            // Used to create the metafile and draw the image
            Graphics graphics = null;
            // The enhanced metafile
            Metafile metaFile = null;
            try
            {
                var rtf = new StringBuilder();
                stream = new MemoryStream();
                // Get a graphics context from the RichTextBox
                using (graphics = CreateGraphics())
                {
                    // Get the device context from the graphics context
                    IntPtr hdc = graphics.GetHdc();
                    // Create a new Enhanced Metafile from the device context
                    metaFile = new Metafile(stream, hdc);
                    // Release the device context
                    graphics.ReleaseHdc(hdc);
                }

                // Get a graphics context from the Enhanced Metafile
                using (graphics = Graphics.FromImage(metaFile))
                {
                    // Draw the image on the Enhanced Metafile
                    graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
                }

                // Get the handle of the Enhanced Metafile
                IntPtr hEmf = metaFile.GetHenhmetafile();
                // A call to EmfToWmfBits with a null buffer return the size of the
                // buffer need to store the WMF bits.  Use this to get the buffer
                // size.
                uint bufferSize = GdipEmfToWmfBits(hEmf, 0, null, 8, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
                // Create an array to hold the bits
                var buffer = new byte[bufferSize];
                // A call to EmfToWmfBits with a valid buffer copies the bits into the
                // buffer an returns the number of bits in the WMF.  
                uint _convertedSize = GdipEmfToWmfBits(hEmf, bufferSize, buffer, 8, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
                // Append the bits to the RTF string
                foreach (byte t in buffer)
                {
                    rtf.Append(String.Format("{0:X2}", t));
                }
                return rtf.ToString();
            }
            finally
            {
                if (graphics != null)
                    graphics.Dispose();
                if (metaFile != null)
                    metaFile.Dispose();
                if (stream != null)
                    stream.Close();
            }
        }

        internal string[] ParseLink(string linkText)
        {
            string[] datas = linkText.Split('#');
            if (0 < datas.Length)
            {
                return datas;
            }
            return null;
        }
         
        internal void RestoreHyperLinks()
        {
            int totalLength = Text.Length;
            foreach (HyperLink lnk in Links)
            {
                //SelectionStart = lnk.Position;// 공백두개
                int length = (lnk.Text + lnk.Link).Length - "#  \n".Length;
                if (totalLength < lnk.Position + length)
                {
                    length = totalLength - lnk.Position;
                }
                Select(lnk.Position, length );                
                SetSelectionLink(true);
            }
        }
    }

    public class HyperLink
    {
        public int Position { get; set; }        
        public string Link { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Position} {Text} : {Link}";
        }
    }
}