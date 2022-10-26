using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace JSFW.Minutes
{
    public static class Ux
    {
        /// <summary>
        /// 컨트롤 비동기 호출! 
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="ctrl"></param>
        /// <param name="action"></param>
        public static void DoAsync<TControl>(this TControl ctrl, Action<TControl> action) where TControl : Control
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(action, ctrl);
            }
            else
            {
                action(ctrl);
            }
        }

        /// <summary>
        /// Object To XML
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="value">object Instance</param>
        /// <returns></returns>
        public static string Serialize<T>(this T value)
        {
            if (value == null) return string.Empty;
            string xml = "";
            try
            {
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (var stringWriter = new System.IO.StringWriter())
                {
                    using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                    {
                        xmlSerializer.Serialize(xmlWriter, value);
                        xml = stringWriter.ToString();
                    }
                }
            }
            catch (Exception exc)
            {
                // 변환 중 Error!
            }
            return xml;
        }

        /// <summary>
        /// Xml String !
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(this string xml) where T : class, new()
        {
            T obj = default(T);
            try
            {
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (var stringReader = new System.IO.StringReader(xml))
                {
                    using (var reader = XmlReader.Create(stringReader, new XmlReaderSettings()))
                    {
                        obj = xmlSerializer.Deserialize(reader) as T;
                    }
                }
            }
            catch
            {
            }
            return obj;
        }



        public static int ThumbnailWith = 160;
        public static int ThumbnailHeight = 90;

        /// <summary>
        /// The WM_PRINT drawing options
        /// </summary>
        [Flags]
        enum DrawingOptions
        {
            /// <summary>
            /// Draws the window only if it is visible.
            /// </summary>
            PRF_CHECKVISIBLE = 1,

            /// <summary>
            /// Draws the nonclient area of the window.
            /// </summary>
            PRF_NONCLIENT = 2,
            /// <summary>

            /// Draws the client area of the window.
            /// </summary>
            PRF_CLIENT = 4,

            /// <summary>
            /// Erases the background before drawing the window.
            /// </summary>
            PRF_ERASEBKGND = 8,

            /// <summary>
            /// Draws all visible children windows.
            /// </summary>
            PRF_CHILDREN = 16,

            /// <summary>
            /// Draws all owned windows.
            /// </summary>
            PRF_OWNED = 32
        }


        /// <summary>
        /// 스크롤 생긴 컨트롤을 지정해야 그안에 컨트롤이 가득 찍혀짐. 
        /// </summary>
        /// <param name="ctrl"></param>
        public static System.Drawing.Image ControlShot(this Control ctrl, int offsetWH, bool copyClipboard = true)
        {
            //http://stackoverflow.com/questions/1881317/c-sharp-windows-form-control-to-image
            //todo : 컨트롤을 스크린을 찍어서 클립보드에 넣어준다.
            if (ctrl is ScrollableControl)
            {
                // 스크롤을 0으로 바꿔주어야 스크린 찍을때 안짤림. 
                ((ScrollableControl)ctrl).HorizontalScroll.Value = 0;
                ((ScrollableControl)ctrl).VerticalScroll.Value = 0;
                ((ScrollableControl)ctrl).AutoScrollPosition = new System.Drawing.Point(0, 0);
            }

            const int WM_PRINT = 791;

            using (System.Drawing.Bitmap screenshot = new System.Drawing.Bitmap(
                                                            ctrl.DisplayRectangle.Width + offsetWH,
                                                            ctrl.DisplayRectangle.Height + offsetWH))
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(screenshot))
            {
                try
                {
                    SendMessage(ctrl.Handle,
                                  WM_PRINT,
                                  g.GetHdc().ToInt32(),
                                  (int)(DrawingOptions.PRF_CHILDREN |
                                            DrawingOptions.PRF_CLIENT |
                                            DrawingOptions.PRF_NONCLIENT |
                                            DrawingOptions.PRF_OWNED));
                }
                finally
                {
                    g.ReleaseHdc();
                }
                //screenshot.Save("temp.bmp");  

                if (copyClipboard)
                    Clipboard.SetImage(screenshot);
                return screenshot.GetThumbnailImage(ThumbnailWith, ThumbnailHeight, delegate { return false; }, IntPtr.Zero);
            }
        }

        /// <summary>
        /// 스크롤 생긴 컨트롤을 지정해야 그안에 컨트롤이 가득 찍혀짐. 
        /// </summary>
        /// <param name="ctrl"></param>
        public static System.Drawing.Image OutputShot(this Control ctrl)
        {
            //http://stackoverflow.com/questions/1881317/c-sharp-windows-form-control-to-image
            //todo : 컨트롤을 스크린을 찍어서 클립보드에 넣어준다.
            if (ctrl is ScrollableControl)
            {
                // 스크롤을 0으로 바꿔주어야 스크린 찍을때 안짤림. 
                ((ScrollableControl)ctrl).HorizontalScroll.Value = 0;
                ((ScrollableControl)ctrl).VerticalScroll.Value = 0;
                ((ScrollableControl)ctrl).AutoScrollPosition = new System.Drawing.Point(0, 0);
            }

            const int WM_PRINT = 791;

            System.Drawing.Bitmap screenshot = new System.Drawing.Bitmap(
                                                            ctrl.DisplayRectangle.Width - 1,
                                                            ctrl.DisplayRectangle.Height - 1);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(screenshot))
            {
                try
                {
                    SendMessage(ctrl.Handle,
                                  WM_PRINT,
                                  g.GetHdc().ToInt32(),
                                  (int)(DrawingOptions.PRF_CHILDREN |
                                            DrawingOptions.PRF_CLIENT |
                                            DrawingOptions.PRF_NONCLIENT |
                                            DrawingOptions.PRF_OWNED));
                }
                finally
                {
                    g.ReleaseHdc();
                }
                return screenshot;
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public static DateTime? ToDateTime(this object obj, string Fmt)
        {
            DateTime _datetime;
            //try
            //{ 
            //    _datetime = DateTime.ParseExact("" + obj, "yyyy-MM-dd tt H:mm:ss", null, System.Globalization.DateTimeStyles.AssumeLocal);
            //    return _datetime;
            //}
            //catch (Exception)
            //{ } 

            if (string.IsNullOrEmpty(Fmt)) Fmt = "yyyy-MM-dd tt hh:mm:ss";

            //if (obj.IsNull() || string.IsNullOrEmpty(("" + obj))) return null;
            //else
            //{
            //    return DateTime.ParseExact("" + obj, Fmt, null, System.Globalization.DateTimeStyles.AssumeLocal);
            //}
            string dt = "" + obj;
            if (dt.Contains("오전") || dt.Contains("오후"))
            {
                if (dt.Length == "yyyy-MM-dd 오후 hh:mm:ss".Length)
                {
                    if (DateTime.TryParseExact("" + obj, "yyyy-MM-dd tt hh:mm:ss", null, System.Globalization.DateTimeStyles.AssumeLocal, out _datetime))
                    {
                        return _datetime;
                    }
                }
                else if (dt.Length == "yyyy-MM-dd 오후 H:mm:ss".Length)
                {
                    if (DateTime.TryParseExact("" + obj, "yyyy-MM-dd tt H:mm:ss", null, System.Globalization.DateTimeStyles.AssumeLocal, out _datetime))
                    {
                        return _datetime;
                    }
                }
            }
            else
            {
                if (dt.Length == "yyyy-MM-dd hh:mm:ss".Length)
                {
                    if (DateTime.TryParseExact("" + obj, "yyyy-MM-dd hh:mm:ss", null, System.Globalization.DateTimeStyles.AssumeLocal, out _datetime))
                    {
                        return _datetime;
                    }
                }
                else if (dt.Length == "yyyy-MM-dd h:mm:ss".Length)
                {
                    if (DateTime.TryParseExact("" + obj, "yyyy-MM-dd h:mm:ss", null, System.Globalization.DateTimeStyles.AssumeLocal, out _datetime))
                    {
                        return _datetime;
                    }
                }
                else if (dt.Length == "yyyy-MM-dd hh:mm:ss.fff".Length)
                {
                    if (DateTime.TryParseExact("" + obj, "yyyy-MM-dd hh:mm:ss.fff", null, System.Globalization.DateTimeStyles.AssumeLocal, out _datetime))
                    {
                        return _datetime;
                    }
                }
                else if (dt.Length == "yyyy-MM-dd h:mm:ss.fff".Length)
                {
                    if (DateTime.TryParseExact("" + obj, "yyyy-MM-dd h:mm:ss.fff", null, System.Globalization.DateTimeStyles.AssumeLocal, out _datetime))
                    {
                        return _datetime;
                    }
                }
                else if (dt.Length == "yyyy-MM-dd".Length)
                {
                    if (DateTime.TryParseExact("" + obj, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.AssumeLocal, out _datetime))
                    {
                        return _datetime;
                    }
                }
                else if (dt.Length == "yyyyMMdd".Length)
                {
                    if (DateTime.TryParseExact("" + obj, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AssumeLocal, out _datetime))
                    {
                        return _datetime;
                    }
                }
            }
            return null;
        }

        public static string Toyyyy_MM_dd(this DateTime datetime, string Fmt)
        {
            if (string.IsNullOrEmpty(Fmt)) Fmt = "yyyy-MM-dd";
            return datetime.ToString(Fmt);
        }

        public static T To<T>(this object obj, object DefaultValue) where T : IConvertible
        {
            if (typeof(T).BaseType == typeof(Enum))
            {
                if (Enum.IsDefined(typeof(T), obj))
                {
                    return (T)Enum.Parse(typeof(T), "" + obj);
                }
                else
                {
                    return (T)DefaultValue;
                }
            }

            TypeCode typecode = (TypeCode)Enum.Parse(typeof(TypeCode), typeof(T).Name);

            if (string.IsNullOrEmpty("" + obj))
            {
                switch (typecode)
                {
                    case TypeCode.Boolean:
                        break;
                    case TypeCode.Byte:
                        break;
                    case TypeCode.Char:
                        break;
                    case TypeCode.DBNull:
                        break;
                    case TypeCode.Empty:
                        break;
                    case TypeCode.Object:
                        break;
                    case TypeCode.SByte:
                        break;
                    case TypeCode.String:
                        break;

                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Single:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        obj = DefaultValue ?? "0";
                        break;
                    default:
                        obj = "";
                        break;
                    case TypeCode.DateTime:
                        return (T)obj;
                }
            }
            return (T)Convert.ChangeType(obj, typecode);
        }


        public static DialogResult Alert(this object msg)
        {
            return MessageBox.Show(string.Format("{0}", msg));
        }
        public static DialogResult AlertWarning(this string msg)
        {
            return MessageBox.Show(string.Format("{0}", msg, "경고"));
        }

        public static void DebugWarning(this string msg)
        {
            string title = "";
            try
            {
                System.Diagnostics.StackFrame sf = new System.Diagnostics.StackFrame(1);
                title = sf.GetMethod().Name + ":";
            }
            catch (Exception)
            {
            }

            System.Diagnostics.Debug.WriteLine(msg, title);
        }
        public static void DebugWarning(this string msg, string title)
        {
            System.Diagnostics.Debug.WriteLine(msg, title);
        }
        /// <summary>
        /// Yes or No [ Question ]
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DialogResult Confirm(this object msg)
        {
            return MessageBox.Show(string.Format("{0}", msg), "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

    }

    public class JSFW_BoxInCircle
    {
        internal IBoxInCircle Box { get; set; }
        /// <summary>
        /// 그려질 꼭지점. 기본값 4
        /// </summary>
        public static readonly double __PointCount = 4d;

        /// <summary>
        /// 그려질 꼭지점. 기본값 4 
        /// </summary>
        public double PointCount = __PointCount;

        double Radius { get { return GetRadus(); } }

        protected internal SortedList<char, PointF> Points = new SortedList<char, PointF>();

        private double GetRadus()
        {
            int bw = Box.HostControl.Width;
            int hw = Box.HostControl.Height;
            return (bw > hw ? hw / 2d : bw / 2d) - 8;
        }

        protected PointF Origin
        {
            get { return GetOrigin(); }
        }

        private PointF GetOrigin()
        {
            return new PointF(this.Box.HostControl.DisplayRectangle.Left + this.Box.HostControl.Width / 2f, this.Box.HostControl.DisplayRectangle.Top + this.Box.HostControl.Height / 2f);
        }

        public JSFW_BoxInCircle(IBoxInCircle box)
        {
            this.Box = box;
            this.Box.HostControl.Paint += new PaintEventHandler(box_Paint);
            this.Box.HostControl.Resize += new EventHandler(box_Resize);
            this.Box.HostControl.Move += new EventHandler(box_Move);
        }

        void box_Move(object sender, EventArgs e)
        {
            if (Box.HostControl.Parent != null) Box.HostControl.Parent.Invalidate();
        }

        void box_Resize(object sender, EventArgs e)
        {
            if (Box.HostControl.Parent != null) Box.HostControl.Parent.Invalidate();
            Box.HostControl.Invalidate();
        }

        void box_Paint(object sender, PaintEventArgs e)
        {
            PaintCircle(e.Graphics);
            //CalcDotPoints();
        }

        private void PaintCircle(Graphics g)
        {
            PointF[] points = CalcDotPoints();
            // 컨트롤내에 점 찍기 
            //PointF temp = points[0];
            //for (int loop = 1; loop < points.Length; loop++)
            //{
            //    g.DrawLine(Pens.Black, temp, points[loop]);
            //    g.FillEllipse(Brushes.Red, temp.X - 5, temp.Y - 5, 10, 10);
            //    g.DrawString("" + Points.Keys[loop], Box.HostControl.Font, Brushes.Blue, points[loop]);
            //    temp = points[loop];
            //}
            //g.DrawLine(Pens.Black, temp, points[0]);
            //g.FillEllipse(Brushes.Red, temp.X - 5, temp.Y - 5, 10, 10);
            //g.DrawString("" + Points.Keys[0], Box.HostControl.Font, Brushes.Blue, points[0]);
        }

        internal PointF[] CalcDotPoints()
        {
            Points.Clear();
            //double theta = ((360d / PointCount) / 180d) * Math.PI;
            //PointF origin = Origin;
            //double x = 0f;
            //double y = 0f;
            //double r = GetRadus();
            //for (int loop = 0; loop < PointCount; loop++)
            //{
            //    x = r * Math.Sin(theta * loop);
            //    y = r * Math.Cos(theta * loop);
            //    Points.Add((char)('a' + loop), new PointF(origin.X + (float)x, origin.Y + (float)y - 2));
            //}
            PointF origin = Origin;
            float offset = 0;
            try
            {
                Type t = Box.HostControl.GetType();
                if (t.GetProperty("BorderStyle") != null)
                {
                    BorderStyle border = (BorderStyle)t.GetProperty("BorderStyle").GetValue(Box.HostControl, null);
                    if (border != BorderStyle.None)
                        offset = 1;
                }
            }
            catch { }

            // left
            Points.Add((char)('a'), new PointF(origin.X - Box.HostControl.Width / 2f - offset - Box.HostControl.Padding.Left, origin.Y - offset));
            // top
            Points.Add((char)('b'), new PointF(origin.X - offset, origin.Y - Box.HostControl.Height / 2f - offset - Box.HostControl.Padding.Top));
            // right
            Points.Add((char)('c'), new PointF(origin.X + Box.HostControl.Width / 2f - offset - Box.HostControl.Padding.Right, origin.Y - offset));
            // bottom
            Points.Add((char)('d'), new PointF(origin.X - offset, origin.Y + Box.HostControl.Height / 2f - offset - Box.HostControl.Padding.Bottom));
            return Points.Values.ToArray();
        }

        public KeyValuePair<char, PointF>[] NearPointSearch(IBoxInCircle target)
        {
            Func<float, float, float, float, double> CalcRadius = (x, y, tx, ty) =>
            {
                return Math.Sqrt(Math.Pow(x - tx, 2) + Math.Pow(y - ty, 2));
            };

            //this.Points
            //target.C.Points
            KeyValuePair<char, PointF>[] v = new KeyValuePair<char, PointF>[2];
            double Min = double.MaxValue;
            char k1 = '-';
            char k2 = '-';
            foreach (var p1 in this.Points)
            {
                Point p1Location = this.Box.HostControl.PointToScreen(Point.Round(p1.Value));
                foreach (var p2 in target.BoxInCircle.Points)
                {
                    Point p2Location = target.HostControl.PointToScreen(Point.Round(p2.Value));
                    double r = CalcRadius(p1Location.X, p1Location.Y, p2Location.X, p2Location.Y);
                    if (r < Min)
                    {
                        Min = r;
                        k1 = p1.Key;
                        k2 = p2.Key;
                    }
                }
            }
            if (k1 == '-' || k2 == '-') return null;
            v[0] = new KeyValuePair<char, PointF>(k1, this.Box.HostControl.PointToScreen(Point.Round(this.Points[k1])));
            v[1] = new KeyValuePair<char, PointF>(k2, target.HostControl.PointToScreen(Point.Round(target.BoxInCircle.Points[k2])));

            return v;
        }
    }

    public interface IBoxInCircle
    {
        /// <summary>
        /// 컨트롤 내에 Circle 박스
        /// </summary>
        JSFW_BoxInCircle BoxInCircle { get; }
        /// <summary>
        /// 대상 컨트롤
        /// </summary>
        Control HostControl { get; }
    }

    internal class LineClass
    {
        public enum LinePosition { None, Left, Top, Right, Bottom }

        public LinePosition Type { get; internal set; }

        public LineClass(float x1, float y1, float x2, float y2)
            : this(new PointF(x1, y1), new PointF(x2, y2))
        { }

        public LineClass(PointF p1, PointF p2)
        {
            StartPointF = p1;
            EndPointF = p2;
        }

        public PointF StartPointF { get; private set; }
        public PointF EndPointF { get; private set; }

        private PointF GetOrigin(Rectangle rct)
        {
            return new PointF(rct.X + rct.Width / 2f, rct.Y + rct.Height / 2f);
        }

        internal static bool GetAcrossPointF(LineClass l1, LineClass l2, ref PointF across)
        {
            return GetIntersectPointF(l1.StartPointF, l1.EndPointF, l2.StartPointF, l2.EndPointF, ref across);
        }

        internal static bool GetIntersectPointF(PointF AP1, PointF AP2, PointF BP1, PointF BP2, ref PointF CrossPointF)
        {
            /*출처 : http://www.gisdeveloper.co.kr/15 */
            double t;
            double s;
            double under = (BP2.Y - BP1.Y) * (AP2.X - AP1.X) - (BP2.X - BP1.X) * (AP2.Y - AP1.Y);
            if (under == 0) return false;

            double _t = (BP2.X - BP1.X) * (AP1.Y - BP1.Y) - (BP2.Y - BP1.Y) * (AP1.X - BP1.X);
            double _s = (AP2.X - AP1.X) * (AP1.Y - BP1.Y) - (AP2.Y - AP1.Y) * (AP1.X - BP1.X);

            t = _t / under;
            s = _s / under;

            if (t < 0.0 || t > 1.0 || s < 0.0 || s > 1.0) return false;
            if (_t == 0 && _s == 0) return false;

            CrossPointF.X = AP1.X + (float)(t * Convert.ToDouble(AP2.X - AP1.X));
            CrossPointF.Y = AP1.Y + (float)(t * Convert.ToDouble(AP2.Y - AP1.Y));

            return true;
        }

        internal static PointF CalcAcrossPointF(LineClass BetweenControlCenterLine, Rectangle rectangle, out LineClass.LinePosition type)
        {
            type = LineClass.LinePosition.None;
            PointF cross = PointF.Empty;
            foreach (LineClass item in CalcRectangleAcrossLine(rectangle))
            {
                if (LineClass.GetAcrossPointF(BetweenControlCenterLine, item, ref cross))
                {
                    type = item.Type;
                    break;
                }
            }
            return cross;
        }

        internal static LineClass[] CalcRectangleAcrossLine(Rectangle rct)
        {
            LineClass top = new LineClass(rct.Left, rct.Top, rct.Right, rct.Top) { Type = LineClass.LinePosition.Top };
            LineClass left = new LineClass(rct.Left, rct.Top, rct.Left, rct.Bottom) { Type = LineClass.LinePosition.Left };
            LineClass bottom = new LineClass(rct.Left, rct.Bottom, rct.Right, rct.Bottom) { Type = LineClass.LinePosition.Bottom };
            LineClass right = new LineClass(rct.Right, rct.Top, rct.Right, rct.Bottom) { Type = LineClass.LinePosition.Right };
            return new LineClass[] { left, top, bottom, right };
        }
    }

    public static class DrawUtil
    {
        public static Pen CreatePan(this Color c, float width, System.Drawing.Drawing2D.LineCap startcap = System.Drawing.Drawing2D.LineCap.Square, System.Drawing.Drawing2D.LineCap endcap = System.Drawing.Drawing2D.LineCap.Square)
        {
            Pen p = new Pen(c, width);
            p.StartCap = startcap;
            p.EndCap = endcap;
            return p;
        }
    }

    public static class JSFW_BoxInCircleEx
    {
        static PointF startPoint;
        static PointF endPoint;
        static LineClass.LinePosition startType = LineClass.LinePosition.None;
        static LineClass.LinePosition endType = LineClass.LinePosition.None;
        static LineClass line;
        static PointF startPointF;
        static PointF endPointF;

        static PointF GetOrigin(Rectangle rct)
        {
            return new PointF(rct.X + rct.Width / 2f, rct.Y + rct.Height / 2f);
        }

        static float getTextAngle(PointF p1, PointF p2)
        {
            double angle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * 180d / Math.PI;
            if (angle < 0) angle += 360d;
            return (float)angle;
        }

        /// <summary>
        /// 사각형 중심위치구하기
        /// </summary>
        /// <param name="rct"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        static void CalcRectangleCenterPoint(RectangleF rct, out float cx, out float cy)
        {
            cx = rct.Width / 2f + rct.Left;
            cy = rct.Height / 2f + rct.Top;
        }

        /// <summary>
        /// 컨트롤들 가상 박스 구하기.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        static RectangleF GetObjectOutLineRectangle(RectangleF[] rcts, float cx, float cy)
        {
            float x, y, r, b;
            x = 100000; y = 10000; r = -10000f; b = -10000f;
            foreach (RectangleF rct in rcts)
            {
                if (x > rct.Left) x = rct.Left;
                if (y > rct.Top) y = rct.Top;
                if (r < rct.Right) r = rct.Right;
                if (b < rct.Bottom) b = rct.Bottom;
            }
            return RectangleF.FromLTRB(x, y, r, b);
        }

        const float crossRate = 10f;
        public static void DrawLine(this JSFW_BoxInCircle box, string LineText, Graphics g, Pen linePan, JSFW_BoxInCircle target, RectangleF area, float DrawCmdIndex = 0)
        {
            //그려지는 컨트롤에 Paint event 이어야 정상동작함! 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            KeyValuePair<char, PointF>[] v = box.NearPointSearch(target.Box);
            if (v != null)
            {
                startPoint = box.Box.HostControl.Parent.PointToClient(Point.Round(v[0].Value));
                endPoint = target.Box.HostControl.Parent.PointToClient(Point.Round(v[1].Value));

                line = new LineClass(endPoint, startPoint);
                startPointF = LineClass.CalcAcrossPointF(line, box.Box.HostControl.Bounds, out startType);
                endPointF = LineClass.CalcAcrossPointF(line, target.Box.HostControl.Bounds, out endType);

                float x = 0f, y = 0f, xyoff = 4f;
                CalcRectangleCenterPoint(RectangleF.FromLTRB(startPointF.X - xyoff, startPointF.Y - xyoff, endPointF.X - xyoff, endPointF.Y - xyoff), out x, out y);
                RectangleF rect = GetObjectOutLineRectangle(new RectangleF[] {
                                                                                                    new RectangleF(startPointF.X - xyoff, startPointF.Y - xyoff, xyoff, xyoff),
                                                                                                    new RectangleF(endPointF.X - xyoff, endPointF.Y - xyoff, xyoff, xyoff) }, x, y);
                //g.DrawRectangle(Pens.Black, rect.Left, rect.Top, rect.Width, rect.Height);
                if (area.Contains(rect))
                {
                    if ((startType == LineClass.LinePosition.Left && endType == LineClass.LinePosition.Right) ||
                        (startType == LineClass.LinePosition.Right && endType == LineClass.LinePosition.Left))
                    {
                        startPointF.Y = startPointF.Y + (DrawCmdIndex * crossRate); endPointF.Y = endPointF.Y - (DrawCmdIndex * crossRate);
                    }
                    else
                    {
                        startPointF.X = startPointF.X + (DrawCmdIndex * crossRate); endPointF.X = endPointF.X - (DrawCmdIndex * crossRate);
                    }

                    g.DrawLine(linePan, startPointF.X, startPointF.Y, endPointF.X, endPointF.Y);

                    DrawString(box, LineText, g, linePan);
                }
            }
        }

        /// <summary>
        /// 점 추적 없이. 박스 사각형 경계선까지만 체크 
        /// </summary>
        /// <param name="box"></param>
        /// <param name="LineText"></param>
        /// <param name="g"></param>
        /// <param name="linePan"></param>
        /// <param name="target"></param>
        /// <param name="area"></param>
        /// <param name="DrawCmdIndex"></param>
        public static void DrawCrossLine(this JSFW_BoxInCircle box, string LineText, Graphics g, Pen linePan, JSFW_BoxInCircle target, RectangleF area, float DrawCmdIndex = 0)
        {
            //그려지는 컨트롤에 Paint event 이어야 정상동작함! 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            // 점간 계산 안하는 로직.
            startPoint = GetOrigin(box.Box.HostControl.Bounds);
            endPoint = GetOrigin(target.Box.HostControl.Bounds);


            line = new LineClass(endPoint, startPoint);
            startPointF = LineClass.CalcAcrossPointF(line, box.Box.HostControl.Bounds, out startType);
            endPointF = LineClass.CalcAcrossPointF(line, target.Box.HostControl.Bounds, out endType);

            float x = 0f, y = 0f, xyoff = 4f;
            CalcRectangleCenterPoint(RectangleF.FromLTRB(startPointF.X - xyoff, startPointF.Y - xyoff, endPointF.X - xyoff, endPointF.Y - xyoff), out x, out y);
            RectangleF rect = GetObjectOutLineRectangle(new RectangleF[] {
                                                                            new RectangleF(startPointF.X - xyoff, startPointF.Y - xyoff, xyoff, xyoff),
                                                                            new RectangleF(endPointF.X - xyoff, endPointF.Y - xyoff, xyoff, xyoff)
                                                        }, x, y);

            //g.DrawRectangle(Pens.Black, rect.Left, rect.Top, rect.Width, rect.Height);
            if (area.Contains(rect))
            {
                if ((startType == LineClass.LinePosition.Left && endType == LineClass.LinePosition.Right) ||
                    (startType == LineClass.LinePosition.Right && endType == LineClass.LinePosition.Left))
                {
                    startPointF.Y = startPointF.Y + (DrawCmdIndex * crossRate); endPointF.Y = endPointF.Y - (DrawCmdIndex * crossRate);
                }
                else
                {
                    startPointF.X = startPointF.X + (DrawCmdIndex * crossRate); endPointF.X = endPointF.X - (DrawCmdIndex * crossRate);
                }
                g.DrawLine(linePan, startPointF.X, startPointF.Y, endPointF.X, endPointF.Y);
                DrawString(box, LineText, g, linePan);
            }
        }

        /// <summary>
        /// 컨트롤 별 점간 >>> 추적
        /// </summary>
        /// <param name="box"></param>
        /// <param name="DrawControl"></param>
        /// <param name="LineText"></param>
        /// <param name="g"></param>
        /// <param name="linePan"></param>
        /// <param name="target"></param>
        /// <param name="area"></param>
        /// <param name="DrawCmdIndex"></param>
        public static void DrawCrossLine(this JSFW_BoxInCircle box, Control DrawControl, string LineText, Graphics g, Pen linePan, JSFW_BoxInCircle target, RectangleF area, float DrawCmdIndex = 0)
        {
            //그려지는 컨트롤에 Paint event 이어야 정상동작함! 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            // 점간 사이계산 
            KeyValuePair<char, PointF>[] v = box.Box.BoxInCircle.NearPointSearch(target.Box);
            if (v != null)
            {
                startPoint = DrawControl.PointToClient(Point.Round(v[0].Value));
                endPoint = DrawControl.PointToClient(Point.Round(v[1].Value));

                line = new LineClass(endPoint, startPoint);
                startPointF = LineClass.CalcAcrossPointF(line, box.Box.HostControl.Bounds, out startType);
                endPointF = LineClass.CalcAcrossPointF(line, target.Box.HostControl.Bounds, out endType);

                float x = 0f, y = 0f, xyoff = 4f;
                CalcRectangleCenterPoint(RectangleF.FromLTRB(startPointF.X - xyoff, startPointF.Y - xyoff, endPointF.X - xyoff, endPointF.Y - xyoff), out x, out y);
                RectangleF rect = GetObjectOutLineRectangle(new RectangleF[] {
                                                                            new RectangleF(startPointF.X - xyoff, startPointF.Y - xyoff, xyoff, xyoff),
                                                                            new RectangleF(endPointF.X - xyoff, endPointF.Y - xyoff, xyoff, xyoff)
                                                        }, x, y);

                //g.DrawRectangle(Pens.Black, rect.Left, rect.Top, rect.Width, rect.Height);
                if (area.Contains(rect))
                {
                    if ((startType == LineClass.LinePosition.Left && endType == LineClass.LinePosition.Right) ||
                        (startType == LineClass.LinePosition.Right && endType == LineClass.LinePosition.Left))
                    {
                        startPointF.Y = startPointF.Y + (DrawCmdIndex * crossRate); endPointF.Y = endPointF.Y - (DrawCmdIndex * crossRate);
                    }
                    else
                    {
                        startPointF.X = startPointF.X + (DrawCmdIndex * crossRate); endPointF.X = endPointF.X - (DrawCmdIndex * crossRate);
                    }
                    g.DrawLine(linePan, startPointF.X, startPointF.Y, endPointF.X, endPointF.Y);
                    DrawString(box, LineText, g, linePan);
                }
            }
            else
            {
                // 점간 계산 안하는 로직.
                startPoint = GetOrigin(box.Box.HostControl.Bounds);
                endPoint = GetOrigin(target.Box.HostControl.Bounds);


                line = new LineClass(endPoint, startPoint);
                startPointF = LineClass.CalcAcrossPointF(line, box.Box.HostControl.Bounds, out startType);
                endPointF = LineClass.CalcAcrossPointF(line, target.Box.HostControl.Bounds, out endType);

                float x = 0f, y = 0f, xyoff = 4f;
                CalcRectangleCenterPoint(RectangleF.FromLTRB(startPointF.X - xyoff, startPointF.Y - xyoff, endPointF.X - xyoff, endPointF.Y - xyoff), out x, out y);
                RectangleF rect = GetObjectOutLineRectangle(new RectangleF[] {
                                                                            new RectangleF(startPointF.X - xyoff, startPointF.Y - xyoff, xyoff, xyoff),
                                                                            new RectangleF(endPointF.X - xyoff, endPointF.Y - xyoff, xyoff, xyoff)
                                                        }, x, y);

                //g.DrawRectangle(Pens.Black, rect.Left, rect.Top, rect.Width, rect.Height);
                if (area.Contains(rect))
                {
                    if ((startType == LineClass.LinePosition.Left && endType == LineClass.LinePosition.Right) ||
                        (startType == LineClass.LinePosition.Right && endType == LineClass.LinePosition.Left))
                    {
                        startPointF.Y = startPointF.Y + (DrawCmdIndex * crossRate); endPointF.Y = endPointF.Y - (DrawCmdIndex * crossRate);
                    }
                    else
                    {
                        startPointF.X = startPointF.X + (DrawCmdIndex * crossRate); endPointF.X = endPointF.X - (DrawCmdIndex * crossRate);
                    }
                    g.DrawLine(linePan, startPointF.X, startPointF.Y, endPointF.X, endPointF.Y);
                    DrawString(box, LineText, g, linePan);
                }

            }
        }


        private static void DrawString(JSFW_BoxInCircle box, string LineText, Graphics g, Pen linePan)
        {
            float angle = getTextAngle(startPointF, endPointF);

            SizeF TextSizeF = g.MeasureString(LineText, box.Box.HostControl.Font);
            int xOffset = 10;
            int yOffset = 10;

            if (startType == LineClass.LinePosition.Left && endType == LineClass.LinePosition.Right)
            {
                xOffset *= -1;
                xOffset -= 0;
                yOffset -= (120f <= angle && angle <= 270f) ? (int)(angle / 8f) : 0;
                yOffset *= -1;
            }
            else if (startType == LineClass.LinePosition.Right && endType == LineClass.LinePosition.Left)
            {
                xOffset *= -1;
                xOffset += 20;
                yOffset += (0f <= angle && angle <= 90f) ? (int)(angle / 4f) : 0;
                yOffset *= -1;
            }
            else if (startType == LineClass.LinePosition.Bottom && (endType == LineClass.LinePosition.Top || endType == LineClass.LinePosition.Right || endType == LineClass.LinePosition.Left))
            {
                xOffset *= -1;
                xOffset += (40f <= angle && angle <= 165f) ? -(int)(angle / (166f - angle)) : 0;
                yOffset -= 0;
                yOffset *= -1;
            }
            else if (startType == LineClass.LinePosition.Right && (endType == LineClass.LinePosition.Top || endType == LineClass.LinePosition.Bottom))
            {
                xOffset *= -1;
                xOffset += 20;
                yOffset += 5;
                yOffset *= -1;
            }
            else if (startType == LineClass.LinePosition.Top && (endType == LineClass.LinePosition.Bottom || endType == LineClass.LinePosition.Right || endType == LineClass.LinePosition.Left))
            {
                xOffset *= -1;
                int xOffet2 = 0;
                if (270f <= angle && angle < 290) xOffet2 += 3;
                else if (290f <= angle && angle < 300) xOffet2 += 6;
                else if (300f <= angle && angle < 310) xOffet2 += 9;
                else if (310f <= angle && angle < 320) xOffet2 += 12;
                else if (320f <= angle && angle < 330) xOffet2 += 15;
                else if (330f <= angle && angle < 340) xOffet2 += 18;
                else if (340f <= angle && angle < 350) xOffet2 += 21;

                xOffset += 20 + xOffet2;

                int yOffet2 = 0;
                if (270f <= angle && angle < 290) yOffet2 += 1;
                else if (290f <= angle && angle < 300) yOffet2 += 2;
                else if (300f <= angle && angle < 310) yOffet2 += 3;
                else if (310f <= angle && angle < 320) yOffet2 += 4;
                else if (320f <= angle && angle < 330) yOffet2 += 5;
                else if (330f <= angle && angle < 340) yOffet2 += 6;
                else if (340f <= angle && angle < 350) yOffet2 += 7;

                yOffset -= 20 - yOffet2;
                yOffset *= -1;
            }
            g.RotateTransform(angle, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.TranslateTransform(startPointF.X + xOffset, startPointF.Y - yOffset, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.DrawString(LineText, box.Box.HostControl.Font, linePan.Brush, new Point());
            g.ResetTransform();
        }
    }

    public class MockupControlReSizeAndMoving
    {
        public Control HostControl { get; set; }

        bool IsMDown = false;
        Point local = new Point();

        Rectangle TopRect = new Rectangle();
        Rectangle LeftRect = new Rectangle();
        Rectangle RightRect = new Rectangle();
        Rectangle BottomRect = new Rectangle();
        Rectangle BodyRect = new Rectangle();

        bool IsTop = false;
        bool IsLeft = false;
        bool IsRight = false;
        bool IsBottom = false;
        bool IsBody = false;

        public bool UseTop = false;
        public bool UseLeft = false;
        public bool UseRight = false;
        public bool UseBottom = false;
        public bool UseMove = false;

        readonly int OFFSET = 4;

        public int MinimumWidth = 20;
        public int MinimumHeight = 20;

        public MockupControlReSizeAndMoving(bool useAll)
        {
            UseLeft = UseTop = UseRight = UseBottom = UseMove = useAll;
        }

        public MockupControlReSizeAndMoving()
        {

        }

        public event Action<string, int> Changed = null;

        public void AttachedEvents(Control ctrl)
        {
            HostControl = ctrl;
            if (HostControl != null)
            {
                HostControl.MouseDown += HostControl_MouseDown;
                HostControl.MouseUp += HostControl_MouseUp;
                HostControl.MouseMove += HostControl_MouseMove;
                HostControl.SizeChanged += HostControl_SizeChanged;
                HostControl.MouseHover += HostControl_MouseHover;

                if (HostControl.HasChildren)
                {
                    for (int loop = 0; loop < HostControl.Controls.Count; loop++)
                    {
                        HostControl.Controls[loop].MouseDown += HostControl_MouseDown;
                        HostControl.Controls[loop].MouseUp += HostControl_MouseUp;
                        //HostControl.Controls[loop].MouseMove += HostControl_MouseMove;
                        //HostControl.Controls[loop].SizeChanged += HostControl_SizeChanged;
                    }
                }

                reCalcRectangle();
            }
        }

        void HostControl_MouseHover(object sender, EventArgs e)
        {
            HostControl.Cursor = Cursors.Default;
            if (HostControl != null)
            {
                if (UseLeft && LeftRect.Contains(Control.MousePosition) &&
                  !(
                        HostControl.Dock == DockStyle.Left ||
                        HostControl.Dock == DockStyle.Top ||
                        HostControl.Dock == DockStyle.Bottom ||
                        HostControl.Dock == DockStyle.Fill
                   ))
                {
                    IsLeft = true; HostControl.Cursor = Cursors.VSplit;
                }
                else if (UseRight && RightRect.Contains(Control.MousePosition) &&
                  !(
                        HostControl.Dock == DockStyle.Right ||
                        HostControl.Dock == DockStyle.Top ||
                        HostControl.Dock == DockStyle.Bottom ||
                        HostControl.Dock == DockStyle.Fill
                   ))
                {
                    IsRight = true; HostControl.Cursor = Cursors.VSplit;
                }
                else if (UseTop && TopRect.Contains(Control.MousePosition) &&
                  !(
                        HostControl.Dock == DockStyle.Top ||
                        HostControl.Dock == DockStyle.Left ||
                        HostControl.Dock == DockStyle.Right ||
                        HostControl.Dock == DockStyle.Fill
                   ))
                {
                    IsTop = true; HostControl.Cursor = Cursors.HSplit;
                }
                else if (UseMove && UseBottom && BottomRect.Contains(Control.MousePosition) &&
                    !(
                        HostControl.Dock == DockStyle.Bottom ||
                        HostControl.Dock == DockStyle.Left ||
                        HostControl.Dock == DockStyle.Right ||
                        HostControl.Dock == DockStyle.Fill
                     ))
                {
                    IsBottom = true; HostControl.Cursor = Cursors.HSplit;
                }
                else if (BodyRect.Contains(Control.MousePosition) &&
                          HostControl.Dock == DockStyle.None)
                {
                    IsBody = true; HostControl.Cursor = Cursors.NoMove2D;
                }
            }
        }

        void HostControl_SizeChanged(object sender, EventArgs e)
        {
            if (IsMDown == false) reCalcRectangle();
        }

        private void reCalcRectangle()
        {
            //HostControl 이 null인경우 : 삭제될 어느 시점인가에 불특정하게 ... 
            if (HostControl == null) return;

            if (UseTop)
            {
                TopRect.X = 0;
                TopRect.Y = 0;
                TopRect.Width = HostControl.Width;
                TopRect.Height = OFFSET;
            }

            if (UseLeft)
            {
                LeftRect.X = 0;
                LeftRect.Y = 0;
                LeftRect.Width = OFFSET;
                LeftRect.Height = HostControl.Height;
            }

            if (UseRight)
            {
                RightRect.X = HostControl.Width - OFFSET;
                RightRect.Y = 0;
                RightRect.Width = OFFSET;
                RightRect.Height = HostControl.Height;
            }

            if (UseBottom)
            {
                BottomRect.X = 0;
                BottomRect.Y = HostControl.Height - OFFSET;
                BottomRect.Width = HostControl.Width;
                BottomRect.Height = OFFSET;
            }

            if (UseMove)
            {
                BodyRect.X = UseLeft ? OFFSET : 0;
                BodyRect.Y = UseTop ? OFFSET : 0;
                BodyRect.Width = UseRight ? HostControl.Width - OFFSET : HostControl.Width;
                BodyRect.Height = UseBottom ? HostControl.Height - OFFSET : HostControl.Height;
            }
        }

        public void DetachedEvents()
        {
            if (HostControl != null)
            {
                HostControl.MouseDown -= HostControl_MouseDown;
                HostControl.MouseUp -= HostControl_MouseUp;
                HostControl.MouseMove -= HostControl_MouseMove;
                HostControl.SizeChanged -= HostControl_SizeChanged;
                HostControl.MouseHover -= HostControl_MouseHover;

                if (HostControl.HasChildren)
                {
                    for (int loop = 0; loop < HostControl.Controls.Count; loop++)
                    {
                        HostControl.Controls[loop].MouseDown -= HostControl_MouseDown;
                        HostControl.Controls[loop].MouseUp -= HostControl_MouseUp;
                        //HostControl.Controls[loop].MouseMove -= HostControl_MouseMove;
                        //HostControl.Controls[loop].SizeChanged -= HostControl_SizeChanged;
                    }
                }

            }
            HostControl = null;
        }

        void HostControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMDown)
            {
                if (IsLeft)
                {
                    int w = e.Location.X - local.X;
                    HostControl.Left += w;
                    HostControl.Width -= w;
                    if (HostControl.Width < MinimumWidth)
                    {
                        HostControl.Width = MinimumWidth;
                    }
                }
                else if (IsRight)
                {
                    int w = e.Location.X - local.X;
                    HostControl.Width += (w);
                    local = e.Location;
                    if (HostControl.Width < MinimumWidth)
                    {
                        HostControl.Width = MinimumWidth;
                    }
                }
                else if (IsTop)
                {
                    int h = e.Location.Y - local.Y;
                    HostControl.Top += h;
                    HostControl.Height -= h;
                    if (HostControl.Height < MinimumHeight)
                    {
                        HostControl.Height = MinimumHeight;
                    }
                }
                else if (IsBottom)
                {
                    int h = e.Location.Y - local.Y;
                    HostControl.Height += (h);
                    local = e.Location;
                    if (HostControl.Height < MinimumHeight)
                    {
                        HostControl.Height = MinimumHeight;
                    }
                }
                else if (IsBody)
                {
                    HostControl.Left += e.Location.X - local.X;
                    HostControl.Top += e.Location.Y - local.Y;
                }
                reCalcRectangle();
            }

            //HostControl.Text = string.Format("l:{0},t:{1},r:{2},b:{3}", IsLeft, IsTop, IsRight, IsBottom);

            IsTop = false;
            IsLeft = false;
            IsRight = false;
            IsBottom = false;
            IsBody = false;

            HostControl.Cursor = Cursors.Default;
            if (HostControl != null)
            {
                if (UseLeft && LeftRect.Contains(e.Location) &&
                  !(
                        HostControl.Dock == DockStyle.Left ||
                        HostControl.Dock == DockStyle.Top ||
                        HostControl.Dock == DockStyle.Bottom ||
                        HostControl.Dock == DockStyle.Fill
                   ))
                {
                    IsLeft = true; HostControl.Cursor = Cursors.VSplit;
                }
                else if (UseRight && RightRect.Contains(e.Location) &&
                  !(
                        HostControl.Dock == DockStyle.Right ||
                        HostControl.Dock == DockStyle.Top ||
                        HostControl.Dock == DockStyle.Bottom ||
                        HostControl.Dock == DockStyle.Fill
                   ))
                {
                    IsRight = true; HostControl.Cursor = Cursors.VSplit;
                }
                else if (UseTop && TopRect.Contains(e.Location) &&
                  !(
                        HostControl.Dock == DockStyle.Top ||
                        HostControl.Dock == DockStyle.Left ||
                        HostControl.Dock == DockStyle.Right ||
                        HostControl.Dock == DockStyle.Fill
                   ))
                {
                    IsTop = true; HostControl.Cursor = Cursors.HSplit;
                }
                else if (UseBottom && BottomRect.Contains(e.Location) &&
                    !(
                        HostControl.Dock == DockStyle.Bottom ||
                        HostControl.Dock == DockStyle.Left ||
                        HostControl.Dock == DockStyle.Right ||
                        HostControl.Dock == DockStyle.Fill
                     ))
                {
                    IsBottom = true; HostControl.Cursor = Cursors.HSplit;
                }
                else if (UseMove && BodyRect.Contains(e.Location) &&
                          HostControl.Dock == DockStyle.None)
                {
                    IsBody = true; HostControl.Cursor = Cursors.NoMove2D;
                }
            }
        }

        void HostControl_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseMouseDownFlag();
            reCalcRectangle();
            if (HostControl != null) HostControl.Cursor = Cursors.Default;
        }

        public void ReleaseMouseDownFlag()
        {
            IsMDown = false;
            if (HostControl == null) return;
            if (Changed != null) Changed("Right", HostControl.Width);
            if (Changed != null) Changed("Bottom", HostControl.Height);
        }

        void HostControl_MouseDown(object sender, MouseEventArgs e)
        {
            IsMDown = e.Button == MouseButtons.Left && HostControl != null;
            local = e.Location;
            if (HostControl != null) HostControl.Cursor = Cursors.Default;
        }
    }

    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue>
        : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
