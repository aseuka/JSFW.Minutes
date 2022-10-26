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
    public partial class MinutesItemControl : UserControl
    {
        public event EventHandler<ItemSelectedEventArgs<MinutesItemControl>> ItemSelected = null;
        public event EventHandler<ItemSelectedEventArgs<MinutesItemControl>> ItemDbClick = null;

        internal MinutesUnit Unit { get; private set; }

        public MinutesItemControl()
        {
            InitializeComponent();

            this.Disposed += MinutesItemControl_Disposed;
        }

        private void MinutesItemControl_Disposed(object sender, EventArgs e)
        {
            // 제거 
            Unit = null;
        }

        public void SetUnit(MinutesUnit unit)
        {
            Unit = unit;
            DataBind();
        }

        private void DataBind()
        {
            DataClear();

            if (Unit != null)
            {
                // 바인딩
                lbID.Text = Unit.Subject;
                lbText.Text = Unit.Desc; 
            }
        }

        private void DataClear()
        {
            HideDeleteCheckBox();
            // 데이타 클리어


        }

        private void lbID_Click(object sender, EventArgs e)
        {
            if (checkBox1.Visible)
                checkBox1.Checked = !checkBox1.Checked;

            if (ItemSelected != null)
            {
                using (var args = new ItemSelectedEventArgs<MinutesItemControl>(this))
                {
                    ItemSelected(this, args);
                }
            }

        }

        private void lbText_Click(object sender, EventArgs e)
        {
            if (checkBox1.Visible)
                checkBox1.Checked = !checkBox1.Checked;

            if (ItemSelected != null)
            {
                using (var args = new ItemSelectedEventArgs<MinutesItemControl>(this))
                {
                    ItemSelected(this, args);
                }
            }
        }

        private void lbID_DoubleClick(object sender, EventArgs e)
        {
            if (ItemDbClick != null)
            {
                using (var args = new ItemSelectedEventArgs<MinutesItemControl>(this))
                {
                    ItemDbClick(this, args);
                    lbID.Text = args.Item.Unit.Subject;
                    lbText.Text = args.Item.Unit.Desc;
                }
            }
        }

        private void lbText_DoubleClick(object sender, EventArgs e)
        {
            if (ItemDbClick != null)
            {
                using (var args = new ItemSelectedEventArgs<MinutesItemControl>(this))
                {
                    ItemDbClick(this, args);
                    lbID.Text = args.Item.Unit.Subject;
                    lbText.Text = args.Item.Unit.Desc;
                }
            }
        }

        #region 삭제 처리용.  
        public bool IsDeleteSelected { get { return checkBox1.Checked; } }

        public void ShowDeleteCheckBox()
        {
            checkBox1.Checked = false;
            checkBox1.Visible = true;
        }

        public void HideDeleteCheckBox()
        {
            checkBox1.Checked = false;
            checkBox1.Visible = false;
        }
        #endregion
    }
}
