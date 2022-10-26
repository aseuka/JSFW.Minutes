using JSFW.Minutes.Controls;
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
    public partial class MainMinutesForm : Form
    {
        MinutesManager MM { get; set; }

        public MainMinutesForm()
        {
            InitializeComponent();

            //Windows 10 일때 Full!  
            Screen sc = Screen.FromControl(this); 
            this.MaximumSize = new Size(sc.WorkingArea.Width + 38, sc.WorkingArea.Height + 16);

            this.FormClosing += MainForm_FormClosing;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Activate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SelectedItem = null;

            if (MM != null)
            {
                MM.MinutesList.Clear();
            }
            MM = null;
        }
         
        protected override void OnLoad(EventArgs e)
        {
            MM = MinutesManager.Load() ?? new MinutesManager();
            base.OnLoad(e);

            foreach (var unit in MM.MinutesList)
            {
                MinutesItemControl newItem = new MinutesItemControl();
                newItem.SetUnit(unit);
                flowLayoutPanel1.Controls.Add(newItem);
                newItem.ItemDbClick += NewItem_ItemDbClick;
                newItem.ItemSelected += NewItem_ItemSelected;
            }
        }
         
        public MinutesItemControl SelectedItem { get; private set; }

        private void SetItem(MinutesItemControl item)
        {
            if (SelectedItem != null)
            {
                SelectedItem.BackColor = Color.White;
                SelectedItem.ForeColor = Color.Black;
            }

            SelectedItem = item;

            if (SelectedItem != null)
            {
                SelectedItem.BackColor = Color.OrangeRed;
                SelectedItem.ForeColor = Color.White;
            }
        }
         
        private void NewItem_ItemSelected(object sender, ItemSelectedEventArgs<MinutesItemControl> e)
        {
            SetItem(e.Item);
        }

        private void NewItem_ItemDbClick(object sender, ItemSelectedEventArgs<MinutesItemControl> e)
        {
            // 더블클릭 > 상세가기            
            using (MinutesInfoForm min = new MinutesInfoForm() { IsNew = false })
            {
                min.Shown += (ss, ee) => {
                    this.Hide();
                };

                min.FormClosed += (ss, ee) =>
                {
                    this.Show();
                };
                 
                min.SetUnit(e.Item.Unit);
                min.ShowDialog(this);
                if (min.IsDirty)
                {
                    min.Unit.Subject = min.Info.Subject;
                    min.Unit.Desc = min.Info.ToDescript();

                    e.Item.Unit.Subject = min.Info.Subject;
                    e.Item.Unit.Desc = min.Info.ToDescript();

                    if (!MM.MinutesList.Any(m => m.GUID == min.Unit.GUID))
                    {
                        MM.Add(min.Unit);
                    }
                    e.Item.SetUnit(min.Unit); 
                    min.Info.Save();
                    MM.Save();
                }
            }
        }
  
        private void btnAddProject_Click(object sender, EventArgs e)
        {
            using (MinutesInfoForm min = new MinutesInfoForm() { IsNew = true })
            {
                min.Shown += (ss, ee) => {
                    this.Hide();
                };

                min.FormClosed += (ss, ee) =>
                {
                    this.Show();
                };

                min.CreateUnit();
                min.ShowDialog();
                if (min.IsDirty)
                {
                    min.Unit.Subject = min.Info.Subject;
                    min.Unit.Desc = min.Info.ToDescript();

                    if (!MM.MinutesList.Any(m => m.GUID == min.Unit.GUID))
                    {
                        MM.Add(min.Unit);

                        MinutesItemControl newItem = new MinutesItemControl();
                        newItem.SetUnit(min.Unit);
                        flowLayoutPanel1.Controls.Add(newItem);
                        newItem.ItemDbClick += NewItem_ItemDbClick;
                        newItem.ItemSelected += NewItem_ItemSelected;
                        SetItem(newItem);
                    }
                    min.Info.Save();
                    MM.Save();
                }
                else
                {
                    if (System.IO.Directory.Exists(min.Unit.GetFileFolderName()))
                    {
                        // 이미지샷으로 폴더가 생성이 이미 되어 있다면!!
                        System.IO.Directory.Delete(min.Unit.GetFileFolderName());
                    }
                }
            }
        }

        private void btnDelProject_Click(object sender, EventArgs e)
        {
            foreach (MinutesItemControl item in flowLayoutPanel1.Controls)
            {
                item.ShowDeleteCheckBox();
            }
            btnOK.BringToFront();
            btnCancel.BringToFront();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (MinutesItemControl item in flowLayoutPanel1.Controls)
            {
                item.HideDeleteCheckBox();
            }

            btnAddProject.BringToFront();
            btnDelProject.BringToFront();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 삭제 처리.
            List<MinutesItemControl> dels = new List<MinutesItemControl>();
            foreach (MinutesItemControl item in flowLayoutPanel1.Controls)
            {
                if (item.IsDeleteSelected)
                    dels.Add(item);
            }

            for (int loop = dels.Count - 1; loop >= 0; loop--)
            {
                using (dels[loop])
                {
                    dels[loop].ItemDbClick -= NewItem_ItemDbClick;
                    dels[loop].ItemSelected -= NewItem_ItemSelected;
                    MM.Remove(dels[loop].Unit);
                    flowLayoutPanel1.Controls.Remove(dels[loop]);
                }
            }
            MM.Save();

            foreach (MinutesItemControl item in flowLayoutPanel1.Controls)
            {
                item.HideDeleteCheckBox();
            }

            btnAddProject.BringToFront();
            btnDelProject.BringToFront();
        }
    }
}
