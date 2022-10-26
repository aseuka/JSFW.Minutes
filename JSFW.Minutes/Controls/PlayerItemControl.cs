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
    public partial class PlayerItemControl : UserControl
    {
        public event EventHandler<ItemSelectedEventArgs<PlayerItemControl>> ItemSelected = null;
        public event EventHandler<ItemSelectedEventArgs<PlayerItemControl>> ItemDbClick = null; 

        /// <summary>
        /// 플레이어 이름 수정하면 ... 
        /// </summary>
        public event EventHandler<ItemSelectedEventArgs<PlayerItemControl>> DataUpdate = null;

        public string SEQ { get; private set; }
        public string PlayerName { get; set; }
         
        public PlayerItemControl()
        {
            InitializeComponent(); 
            this.Disposed += PlayerItemControl_Disposed;
        }

        private void PlayerItemControl_Disposed(object sender, EventArgs e)
        {
            // 제거  
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(System.Drawing.Pens.Gray, 0, this.Height - 1, this.Width, this.Height - 1);
        }

        internal void Set(string player)
        {
            if (player.IndexOf(":::") < 0)
            {            
                PlayerName = player;
            }
            else
            {
                string[] data = player.Split(":::".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                
                if (0 < data.Length) PlayerName = data[0];
                if (1 < data.Length) SEQ = data[1];
            }

            if (string.IsNullOrWhiteSpace(SEQ))
            {
                SEQ = Guid.NewGuid().ToString("N");
            }
         
            DataBind();
        }

        public void SetUnit(string seq, string playerName )
        {
            SEQ = seq; 
            PlayerName = playerName;
            DataBind();
        }

        private void DataBind()
        {
            DataClear();

            string fmt = "";
            if (string.IsNullOrWhiteSpace(PlayerName) == false)
                fmt += PlayerName.Trim() + " ";

            if (string.IsNullOrWhiteSpace(SEQ) == false)
                fmt += ":::" + SEQ.Trim();

            lbPlayer.Text = fmt;
        }

        private void DataClear()
        {
            HideDeleteCheckBox();
            // 데이타 클리어 
            lbPlayer.Text = "";
        }

        private void lbPlayer_Click(object sender, EventArgs e)
        {
            if (checkBox1.Visible)
                checkBox1.Checked = !checkBox1.Checked;

            if (ItemSelected != null)
            {
                using (var args = new ItemSelectedEventArgs<PlayerItemControl>(this))
                {
                    ItemSelected(this, args);
                }
            }

        } 

        private void lbPlayer_DoubleClick(object sender, EventArgs e)
        {
            if (ItemDbClick != null)
            {
                ToggleEdit();

                if (txtPlayerName.Visible)
                { 
                    txtPlayerName.Text = PlayerName;
                    txtPlayerName.Modified = false;

                    txtPlayerName.Focus();
                    txtPlayerName.DeselectAll();
                }

                using (var args = new ItemSelectedEventArgs<PlayerItemControl>(this))
                {
                    ItemDbClick(this, args);
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

        private void txtPlayerName_Leave(object sender, EventArgs e)
        {
            Commit();
        }

    
        private void txtPlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ToggleEdit();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Commit();
            }
        }


        private void Commit()
        {
            if (txtPlayerName.Visible)
            {
                ToggleEdit();

                if (!txtPlayerName.Visible)
                {
                    PlayerName = lbPlayer.Text = txtPlayerName.Text;
                    if (DataUpdate != null && txtPlayerName.Modified)
                    {
                        using (var args = new ItemSelectedEventArgs<PlayerItemControl>(this))
                        {
                            DataUpdate(this, args);
                        } 
                    }
                }
            }
        }
         
        private void ToggleEdit()
        {
            txtPlayerName.Visible = !txtPlayerName.Visible;
            lbPlayer.Visible = !txtPlayerName.Visible;
        }

        internal void CloseEdit()
        {
            Commit();
        }

        internal void Edit()
        {
            ToggleEdit();
            txtPlayerName.Focus();  
        }






        // 드래그 드랍. PlayerViewControl
        bool isMouseDown = false;
        Point pt; 
        private void lbPlayer_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = false; 
            {
                if (e.Button == MouseButtons.Left)
                {
                    isMouseDown = true;
                }
            }
            pt = e.Location;
        }

        private void lbPlayer_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int x = e.Location.X - pt.X;
                int y = e.Location.Y - pt.Y;
                int z = (int)Math.Sqrt(Math.Pow((double)Math.Abs(x), 2d) + Math.Pow((double)Math.Abs(y), 2d));

                if (4 < z)
                {
                    if (isMouseDown)
                    {
                        PlayerDragDropData dragObject = new PlayerDragDropData() { SEQ = SEQ, PlayerName = PlayerName };
                        try
                        {
                            DoDragDrop(dragObject, DragDropEffects.Link);
                            isMouseDown = false;
                        }
                        finally
                        {
                            dragObject = null;
                        }
                    }
                }
            }
        }
         
        private void lbPlayer_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
    }

    /// <summary>
    /// 드래그 앤 드랍으로 사용.
    /// </summary>
    public class PlayerDragDropData
    {
        public string SEQ { get; set; }
        public string PlayerName { get; set; }

        // 아래는 장소에 놓인 참여자 컨트롤 위치값들...
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    } 
}
