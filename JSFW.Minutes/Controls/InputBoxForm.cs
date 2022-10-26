using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.Minutes.Controls
{
    public partial class InputBoxForm : Form
    {
        public string Title { get { return label1.Text; } set { label1.Text = value; } }

        public string Content { get; set; }

        public string ASIS { get { return label2.Text; } set { label2.Text = value; } }

        public string TOBE { get { return label3.Text; } set { label3.Text = value; } }

        public InputBoxForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                
            }

            Content = textBox1.Text.Trim();

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Control && e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick(); 
            }
        }

        internal void SetContent(string text)
        {
            textBox1.Text = text;
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = label2.Text;
        }
    }
}
