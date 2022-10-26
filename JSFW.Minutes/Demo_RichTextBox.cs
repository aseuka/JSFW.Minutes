using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.Minutes
{
    public partial class Demo_RichTextBox : Form
    { 
        public Demo_RichTextBox()
        {
            InitializeComponent();
            richTextBox1.LinkClicked += RichTextBox1_LinkClicked;
        } 
        protected override void OnFormClosing(FormClosingEventArgs e)
        { 
            base.OnFormClosing(e);
        } 

        private void btnInsertImage_Click(object sender, EventArgs e)
        {
            string imageFilePath = @"C:\Users\aseuk\Pictures\11.jpg";
            using (Image img = Bitmap.FromFile(imageFilePath))
            {
                richTextBox1.InsertImage(img.Clone() as Image);
                richTextBox1.AppendText(Environment.NewLine);
            } 
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                richTextBox1.AppendText( textBox1.Text );
                richTextBox1.AppendText( Environment.NewLine );
                e.Handled = true; 
            }
        }


        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                //if (e.Handled)
                {
                    textBox1.ResetText();
                }
            }
        }


        int num = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            // 하이퍼링크 넣기 
            //richTextBox1.InsertLink("후.. 한글이 이제 되는구낭. " + ( num ++), "www.daum.net");

            //동일한 텍스트와 링크를 넣으면 >> 복원했을때 1개로 인식함. ( 거지 같네? )
            richTextBox1.InsertLink("후.. 한글이 이제 되는구낭. ", "www.daum.net");
            //string temprtf = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\froman\\fprq2\\fcharset0 Times New Roman;}}\r\n{\\colortbl ;\\red0\\green0\\blue255;}\r\n\\viewkind4\\uc1\\pard\\ltrpar\\cf1\\ul\\f0\\fs24 마소<http://microsoft.com>\\cf0\\ulnone\\par\r\n}\r\n";
            //richTextBox1.SelectedRtf = temprtf.Replace("microsoft.com", "amazon.com");
            //richTextBox1.DetectUrls = true;

            if (num == 3) num = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string imageFilePath = @"C:\Users\aseuk\Pictures\11.jpg";
            using (Image img = Bitmap.FromFile(imageFilePath))
            {
                richTextBox1.InsertImage( img.Clone() as Image );
                richTextBox1.SelectedText += Environment.NewLine;
            }
        }

        private void RichTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string[] url = richTextBox1.ParseLink(e.LinkText);
            url?[1]?.Alert();
        }

        private void Demo_RichTextBox_linkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //e.Link.LinkData?.ToString().Alert();
            e.Link?.ToString().Alert();
        }

        string rtf = "";
        List<Controls.HyperLink> Links = new List<Minutes.Controls.HyperLink>();
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            rtf = richTextBox1.Rtf;
            Links.Clear();
            Links.AddRange(richTextBox1.Links.ToArray());
            richTextBox1.Links.Clear();
            richTextBox1.ResetText();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Links.Clear();
            richTextBox1.Rtf = rtf;
            richTextBox1.Links.AddRange(Links.ToArray());
            richTextBox1.RestoreHyperLinks();    
        }

    }
     
}
