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
    public partial class SubjectViewControl : TreeView
    {
        public enum PreFixType {
            NONE = 0,
            ALPHA_BIG = 1,
            ALPHA_SMALL = 2,
            NUMBER = 3,
            ALPHA_NUMBER = 4,
        };

        PreFixType _PreFix = PreFixType.NUMBER;

        public PreFixType PreFix
        {
            get { return _PreFix; }
            set
            {
                _PreFix = value;
                SetSubjectList(SubjectText);
            }
        }

        public string SubjectText { get; set; } = "";

        bool _PlayMode = false;
        public bool PlayMode
        {
            get { return _PlayMode; }
            set
            {
                _PlayMode = value; 
            }
        }

        public SubjectViewControl()
        {
            InitializeComponent(); 
            HideSelection = false;
            DrawMode = TreeViewDrawMode.OwnerDrawText; 
        }

        SolidBrush bgBrush = new SolidBrush(DefaultBackColor);
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            base.OnDrawNode(e); 
            // 포커스가 없어도 선택한것처럼 보이게 처리함.
            if (e.Node.Equals( SelectedNode ))
            {
                Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
                Color fore = e.Node.ForeColor;
                if (fore == Color.Empty) fore = e.Node.TreeView.ForeColor;
                fore = SystemColors.HighlightText;
                Color highlightColor = SystemColors.Highlight;
                e.Graphics.FillRectangle(new SolidBrush(highlightColor), -e.Bounds.X, e.Bounds.Y, Width + (e.Bounds.X), e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, fore, highlightColor, TextFormatFlags.GlyphOverhangPadding);
            }
            else
            {
                if (bgBrush.Color != BackColor)
                {
                    bgBrush = new SolidBrush(BackColor);
                }
                e.Graphics.FillRectangle( bgBrush, -e.Bounds.X, e.Bounds.Y, Width + (e.Bounds.X), e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, Font, e.Bounds, e.Node.ForeColor, BackColor, TextFormatFlags.GlyphOverhangPadding);
            }
        }

      
        // 앞에 들어가는 포맷! 
        internal void SetSubjectList(string text)
        {
            // 모두  
            //            string temp = @"# 안건1
            //##  항목1-1
            //# 안건2
            //## 항목2-1
            //### 항목2-1-1"; 
            SubjectText = text;
            DataClear();             
            InitRender(SubjectText); 
        }

        private void DataClear()
        {
            if (0 < Nodes.Count)
            {
                foreach (TreeNode nd in Nodes)
                {
                    NodeClear(nd);
                }
                Nodes.Clear();
            }
        }

        private void NodeClear(TreeNode nd)
        {
            if ( nd != null && 0 < nd.Nodes.Count)
            {
                foreach (TreeNode cd in nd.Nodes)
                {
                    NodeClear(cd);
                }
                nd.Nodes.Clear();
            }
        }

        int seq = 0;
        List<int> Numbers = new List<int>();

        internal void InitRender(string treeContent)
        { 
            string[] lst = treeContent.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                          .Where(w => string.IsNullOrEmpty(w.Trim()) == false).ToArray();
            
            TreeNode CurrentNode = null;

            OLD_LV = -1;
            seq = 0;

            foreach (var nm in lst)
            {
                int lv = nm.Count(c => c == '#');

                string title = "";
                string fr = "";
                string to = "";

                var txt = nm.Trim('\t').Trim('#').Split(";;".ToArray(), StringSplitOptions.RemoveEmptyEntries);

                string pre = GetPreFix(lv);

                if (string.IsNullOrWhiteSpace(pre) == false) pre += ". ";

                title = nm.Trim().TrimStart('#');
                if (txt.Length > 0)
                {
                    title = pre + txt[0].Trim().TrimStart('\t').TrimStart('#');
                }
                if (txt.Length > 1)
                {
                    fr = txt[1];
                }
                if (txt.Length > 2)
                {
                    to = txt[2];
                }

                if (lv <= 1)
                {
                    CurrentNode = Nodes.Add(title);
                }
                else if (lv > CurrentNode.Level)
                {
                    if (OLD_LV < lv)
                    {
                        for (int loop = (OLD_LV - lv) - 1; loop >= 0; loop--)
                        {
                            CurrentNode = CurrentNode.Parent;
                        }
                        CurrentNode = CurrentNode.Nodes.Add(title);
                    }
                    else
                    {
                        CurrentNode = CurrentNode.Parent.Nodes.Add(title);
                    }
                    
                }
                else if (lv == CurrentNode.Level)
                {
                    if (lv < OLD_LV)
                    {
                        for (int loop = (OLD_LV - lv) - 1; loop >= 0; loop--)
                        {
                            CurrentNode = CurrentNode.Parent;
                        }
                    } 
                    CurrentNode = CurrentNode.Parent.Nodes.Add(title);
                }
                else
                {
                    CurrentNode = CurrentNode.Parent.Nodes.Add(title);
                }
                OLD_LV = lv;
            }
            ExpandAll();
        }

        int OLD_LV = -1;
        private string GetPreFix(int lv)
        { 
            if (lv == 1 && OLD_LV != lv) // 1레벨과 이전레벨과 다르면 seq증가 및 번호채번을 다시하기 위해. 초기값 설정
            {
                seq++;
                Numbers.Clear();
                Numbers.Add(seq);
            }
            else if (OLD_LV == lv) // 이전레벨과 같으면 해당 번호 증가 시켜.
            {
                Numbers[Numbers.Count - 1]++;
            }
            else if (lv != OLD_LV) // 이전레벨과 같으면 해당 번호 증가 시켜.
            {
                if (lv < OLD_LV)
                {
                    for (int loop = (OLD_LV - lv) - 1; loop >= 0; loop--)
                    {
                        Numbers.RemoveAt(Numbers.Count - 1);
                    }
                    if (0 < Numbers.Count)
                    {
                        Numbers[Numbers.Count - 1]++;
                    }
                    else
                    {
                        Numbers.Add(seq);
                    }
                }
                else
                {
                    Numbers.Add(1); 
                }
            }
            else // 2레벨, 3레벨이면...
            {
                Numbers.Add(1); //레벨 채번번호 증가.
            }
              
            string result = "";
             
            if (PreFix == PreFixType.NONE) return result;
            switch (PreFix)
            {
                default:
                case PreFixType.NONE: break;
                case PreFixType.ALPHA_BIG:
                    result = string.Join(".", Numbers.ConvertAll(n => "" + (char)('A' + n - 1)));
                    break;
                case PreFixType.ALPHA_SMALL:
                    result = string.Join(".", Numbers.ConvertAll(n => "" + (char)( 'a' + n -1)));
                    break;
                case PreFixType.NUMBER:
                    result = string.Join(".", Numbers.ConvertAll(n => "" + n));
                    break;
                case PreFixType.ALPHA_NUMBER:
                    List<string> buffers = new List<string>();
                    if (0 < Numbers.Count)
                    {
                        buffers.Add( ""+ (char)( 'A' + Numbers[0] - 1 ));
                        for (int loop = 1; loop < Numbers.Count; loop++)
                        {
                            buffers.Add(""+Numbers[loop]);
                        }
                    }
                    result = string.Join(".", buffers.ToArray());
                    break;
            }
            return result;
        }
    }
}
