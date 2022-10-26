using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.Minutes.Controls
{
    public partial class ChattingForm : UserControl
    {
        public string SEQ { get; set; }
        public string PlayerName { get { return label1.Text; } set { label1.Text = value; } }
        
        public string Chatt { get { return textBox1.Text; } }

        public event EventHandler Commit = null;

        public ChattingForm()
        {
            InitializeComponent();
        }
         
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Control)
                {
                    OnCommit();
                }
            }
        }

        private void OnCommit()
        {
            if (Commit != null)
            {
                Commit(this, EventArgs.Empty);
            }
        }

        // 비동기 콜!
        public void ClearText()
        {
            this.DoAsync(t =>
            {
                textBox1.ResetText();
                textBox1.Text = "";
                textBox1.Modified = false;
            });
        }
    }
}
