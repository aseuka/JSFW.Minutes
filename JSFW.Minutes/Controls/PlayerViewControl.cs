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
    /// <summary>
    /// 플레이어 > 장소 배치되어 액터.
    /// </summary>
    public partial class PlayerViewControl : UserControl
    {
        public PlayerViewControl()
        {
            InitializeComponent();

            this.lbPlayerName.MouseDown += Label1_MouseDown;
            this.lbPlayerName.MouseUp += Label1_MouseUp;
            this.lbPlayerName.MouseMove += Label1_MouseMove;

            this.lbPlayerName.MouseDoubleClick += Label1_MouseDoubleClick;

            this.BackColor = Color.FromArgb(80, BackColor);
        }
        
        private void Label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //참가자 더블클릭시 대화 입력창 띄우기 위해 라벨과 이벤트를 연결해줌.
            OnMouseDoubleClick(e);
        }

        private void Label1_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void Label1_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void Label1_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        bool IsDn = false;
        bool IsRS = false;
        Point dnPoint;
        Rectangle RSZ;
        readonly int OFFSET = 8;
          
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                IsDn = true;
                dnPoint = e.Location;

                ReCalcBox();

                if (RSZ.Contains(e.Location))
                {
                    IsRS = true;
                    Cursor = Cursors.SizeNWSE;
                    return;
                }
            }
            Cursor = Cursors.Default; 
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Cursor = Cursors.Default;

            IsDn = false; IsRS = false;
            ReCalcBox();

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e); 

            if (IsDn)
            {
                if (IsRS)
                {
                    int x = (e.X - dnPoint.X);
                    int y = (e.Y - dnPoint.Y);
                    int w = Width + x;
                    int h = Height + y;
                    if (Width + x < 20)
                    {
                        w = 20;
                    }
                    if (Height + y < 20)
                    {
                        h = 20;
                    }
                    Width = w;
                    Height = h;
                    dnPoint = e.Location;
                }
                else
                {
                    // 부모컨트롤 밖으로 나가지 못하게 막음. 
                    if (CheckArea( e.Location ))
                    { 
                        this.Left += e.Location.X - dnPoint.X;
                        this.Top += e.Location.Y - dnPoint.Y;
                        // pt = e.Location; // 있으면 X
                    }
                }

                ReCalcBox();

                Invalidate();
                if (Parent != null) Parent.Invalidate(true);
            }

            if (RSZ.Contains(e.Location))
            {
                Cursor = Cursors.SizeNWSE;
                return;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private bool CheckArea(Point loc)
        {
            bool isIn = false;

            Rectangle rect = Parent.ClientRectangle;
            int x = this.Left + loc.X - dnPoint.X;
            int y = this.Top + loc.Y - dnPoint.Y;

            Rectangle rect2 = new Rectangle(x, y, this.Width, this.Height);

            if (rect.Contains(rect2)) {
                isIn = true;
            }
            return isIn;
        }

        private void ReCalcBox()
        {
            RSZ.X = Width - OFFSET;
            RSZ.Y = Height - OFFSET;
            RSZ.Width = OFFSET;
            RSZ.Height = OFFSET;
        }


        public string SEQ { get; private set; }

        public string PlayerName { get { return lbPlayerName.Text; } }

        internal void SetData(PlayerDragDropData data)
        {
            SEQ = data.SEQ;
            lbPlayerName.Text = data.PlayerName;
        }
    }
}
