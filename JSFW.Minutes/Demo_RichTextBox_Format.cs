using JSFW.Minutes.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.Minutes
{ 
    /// <summary>
    /// 레코드를 받아서 디스플레이 하는 화면으로 미리 테스트 함. 
    /// </summary>
    public partial class Demo_RichTextBox_Format : Form
    {
        public Demo_RichTextBox_Format()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Record rec = new Record() {
                Command = "SELECT",
                TypeName = "SUBJECT",
                TargetID = "", 
                Data = "1. 안건", 
                Tick = DateTime.Now.Ticks
            };

            RecordView.Write(richTextBoxEx1, rec);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //자료입력
            string imageFilePath = @"C:\Users\aseuk\Pictures\11.jpg";
            Record rec = new Record()
            {
                Command = "CHAGE",
                TypeName = "IMAGE",
                TargetID = "",
                Data = imageFilePath,
                Tick = DateTime.Now.Ticks
            };

            RecordView.Write(richTextBoxEx1, rec);

            //string imageFilePath = @"C:\Users\aseuk\Pictures\11.jpg";
            //using (Image img = Bitmap.FromFile(imageFilePath))
            //{
            //    richTextBoxEx1.InsertImage(img.Clone() as Image);
            //    richTextBoxEx1.AppendText(Environment.NewLine);
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 챗창 입력
            string text = @"텍스트 입력
가나다라
abcd ef";
            Record rec = new Record()
            {
                Command = "INPUT",
                TypeName = "CHAT",
                TargetID = "개발자SEQ",
                Player = "개발자 아무개",
                Data = text,
                Tick = DateTime.Now.Ticks
            };
            RecordView.Write(richTextBoxEx1, rec);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string rtf = richTextBoxEx1.Rtf;
        }
    }

    public static class RecordView
    {
        public static void Write(this RichTextBoxEx box, Record rec)
        {
            if (rec.TypeName == "SUBJECT")
            {
                // 안건 선택
                box.Append(rec);
            }
            else if (rec.TypeName == "IMAGE")
            {
                if (rec.Command == "COMMENT")
                {
                    box.AppendComment(rec);
                }
                else
                {
                    // 이미지!
                    box.Draw(rec);
                }
            }
            else if (rec.TypeName == "CHAT")
            {
                box.InputChat(rec);
            }
            else if (rec.TypeName == "COMMONMSG")
            {
                box.CommonMessage(rec);
            }
        }

        private static void Append(this RichTextBoxEx box, Record rec)
        {
            // "2. 안건2\2.1. 항목2\2.1.1. 항목21\2.1.1.1. 항목22 [2018-03-09 18:50:41]" 
            string[] txts = ("" + rec.Data).Split('\\');
            List<string> lst = new List<string>();
          
            int tabCount = 0;
            foreach (var txt in txts)
            {
                lst.Add(new string(' ', (4 * tabCount++)) + txt); // + ( tabCount == 1 ? $" [{ new DateTime(rec.Tick).ToString("yyyy-MM-dd HH:mm:ss")}]" : ""));
            }
            //lst.Add($" [{ new DateTime(rec.Tick).ToString("HH:mm:ss")}]");

            string subject = string.Join(Environment.NewLine, lst.ToArray()) + $" [{ new DateTime(rec.Tick).ToString("HH:mm:ss")}]"; 
            int position = box.TextLength;
            box.AppendText(subject); 
            box.Select(position, subject.Length + 1);
            box.SelectionFont = new Font("맑은 고딕", 10f, FontStyle.Bold);
            box.SelectionColor = Color.Coral;
            box.SelectionAlignment = HorizontalAlignment.Left;
            box.Select(position + subject.Length + 1, 0);
            box.AppendText(Environment.NewLine);
            box.ScrollToCaret();
        }

        private static void AppendComment(this RichTextBoxEx box, Record rec)
        {
            string text = "@ 자료변경" + $"[{ new DateTime(rec.Tick).ToString("HH:mm:ss")}]";
            int position = box.TextLength;
            box.AppendText(text);
            box.Select(position, text.Length + 1);
            box.SelectionFont = new Font("맑은 고딕", 10f, FontStyle.Bold);
            box.SelectionColor = Color.Black;
            box.SelectionAlignment = HorizontalAlignment.Left;
            box.Select(position + text.Length + 1, 0);
            box.AppendText(Environment.NewLine);

            text = "" + rec.Data;  // 코멘트.. 
            position = box.TextLength;
            box.AppendText(text);
            box.Select(position, text.Length + 1);
            box.SelectionFont = new Font("맑은 고딕", 10f, FontStyle.Bold);
            box.SelectionColor = Color.Black;
            box.SelectionAlignment = HorizontalAlignment.Left;
            box.Select(position + text.Length + 1, 0);
            box.AppendText(Environment.NewLine); 
            box.ScrollToCaret();
        }

        private static void Draw(this RichTextBoxEx box, Record rec)
        {
            string imagePath = "" + rec.Data;
            if (File.Exists(imagePath))
            {
                string text = "@ 자료변경" + $"[{ new DateTime(rec.Tick).ToString("HH:mm:ss")}]";
                int position = box.TextLength;
                box.AppendText(text);
                box.Select(position, text.Length + 1);
                box.SelectionFont = new Font("맑은 고딕", 10f, FontStyle.Bold);
                box.SelectionColor = Color.Black;
                box.SelectionAlignment = HorizontalAlignment.Left;
                box.Select(position + text.Length + 1, 0);
                box.AppendText(Environment.NewLine);
                 
                using (Image img = Bitmap.FromFile(imagePath))
                {
                    // 비율 계산 
                    // img.w : img.h = toW : toH
                    // img.H * toW = img.W * toH
                    // toH = ( img.H * toW ) / img.W
                    // toW = ( img.W * toH ) / img.H
                    int height = ( img.Height * box.Width - 16 ) / img.Width; 
                    box.InsertImage(img.GetThumbnailImage(box.Width, height, delegate { return false; }, IntPtr.Zero));
                    box.AppendText(Environment.NewLine);
                } 
                box.InsertLink( Path.GetFileName( imagePath.Replace("\\", "\\\\") ), imagePath.Replace("\\", "\\\\"));
                box.AppendText(Environment.NewLine);
                box.ScrollToCaret();
            }
        }

        private static void InputChat(this RichTextBoxEx box, Record rec)
        {
            string subject = rec.Player + $" 님" + $" [{ new DateTime(rec.Tick).ToString("HH:mm:ss")}]";
            int position = box.TextLength;
            box.AppendText(subject);
            box.Select(position, subject.Length + 1);
            box.SelectionFont = new Font("맑은 고딕", 10f, FontStyle.Bold);
            box.SelectionColor = Color.Black;
            box.SelectionAlignment = HorizontalAlignment.Left;
            box.Select(position + subject.Length + 1, 0);
            box.AppendText(Environment.NewLine);

            subject = "   " + ("" + rec.Data).Trim().Replace(Environment.NewLine, Environment.NewLine + "   ");
            position = box.TextLength;
            box.AppendText(subject);
            box.Select(position, subject.Length + 1);            
            box.SelectionFont = new Font("맑은 고딕", 9f, FontStyle.Regular);
            box.SelectionColor = Color.Black;
            box.SelectionAlignment = HorizontalAlignment.Left;
            box.Select(position + subject.Length + 1, 0);
            box.AppendText(Environment.NewLine);

            box.ScrollToCaret();
        }
         
        private static void CommonMessage(this RichTextBoxEx box, Record rec)
        {
            string subject = "※" + ("" + rec.Data).Trim().Replace(Environment.NewLine, Environment.NewLine + "   ").Trim() + $" [{ new DateTime(rec.Tick).ToString("HH:mm:ss")}]" + "※";
            int position = box.TextLength;
            box.AppendText(subject);
            box.Select(position, subject.Length + 1);
            box.SelectionFont = new Font("맑은 고딕", 12f, FontStyle.Bold);
            box.SelectionColor = Color.DodgerBlue;
            box.SelectionAlignment = HorizontalAlignment.Center;
            box.Select(position + subject.Length + 1, 0);
            box.AppendText(Environment.NewLine); 
            box.ScrollToCaret();
        }
    }
}
