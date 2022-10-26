using JSFW.Minutes.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSFW.Minutes
{
    public class MinutesUnit : Unit
    {
        internal static readonly string ConfigFileName = @"MinutesInfo.xml";

        public string Subject { get; set; }

        public MinutesInfo GetMinutesInfo()
        {
            // todo : 프로젝트 목록에서 프로젝트 상세보기를 갈때 인스턴스를 한다! 
            string dir = GetFolderName();
            string infoFileName = dir + ConfigFileName;
            // 파일이 있으면 로딩
            if (File.Exists(infoFileName))
            {
                string xml = File.ReadAllText(infoFileName);
                MinutesInfo prj = xml.DeSerialize<MinutesInfo>();
                prj.Unit = this;

                // 레코드 보정이 필요함?

                foreach (Record rec in  prj.Records)
                {
                    if (rec.TypeName == "CHAT")
                    {
                        rec.Data = ("" + rec.Data).Replace("\n", Environment.NewLine);
                    }
                }

                return prj;
            }
            else
            {
                // 파일이 없으면 
                return new MinutesInfo(this);
            }
        }

        internal void NewCreateInfoFile()
        {
            // 초기 최초 파일 생성 
            string dir = GetFolderName();
            if (!Directory.Exists(dir))
            {

                Directory.CreateDirectory(dir);
            }

            string file_dir = GetFileFolderName();
            if (!Directory.Exists(file_dir))
            {
                Directory.CreateDirectory(file_dir);
            }

            MinutesInfo info = GetMinutesInfo();
            using (info as IDisposable)
            {
                // 저장!
                info.Save();
            }
        }

        internal void Remove()
        {
            string dir = GetFolderName();
            try
            {
                if (Directory.Exists(dir))
                {
                    //?에러가 날때가 있어...
                    Directory.Move(dir, dir.Substring(0, dir.Length - "\\".Length) + "$Del" + DateTime.Now.Ticks);
                }
            }
            catch (Exception ex)
            {                
                throw;
            }
        }

        public override string GetFolderName()
        {
            return StaticConst.JSFW_NPT_DIR + GUID + "\\";
        }
         
        public string GetFileFolderName()
        {
            string dir = GetFolderName() + "Files\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return GetFolderName() + "Files\\";
        }
    }

    public class MinutesInfo : Info<MinutesUnit>
    { 
        /// <summary>
        /// 주제
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 장소
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// 회의 일자
        /// </summary>
        public string Day { get; set; }
         
        /// <summary>
        /// 회의 시각
        /// </summary>
        public string Time { get; set; }
         
        /// <summary>
        /// 안건
        /// </summary>
        public string Topics { get; set; }

        /// <summary>
        /// 참석자들
        /// </summary>
        public string Players { get; set; }

        /// <summary>
        /// 레코드
        /// </summary>
        public List<Record> Records { get; set; }

        /// <summary>
        /// 로그
        /// </summary>
        public string Log { get; set; }

        /// <summary>
        /// 챗로그 rtf
        /// </summary>
        public string ChattRTF { get; set; }

        /// <summary>
        /// 첨부파일 rtf
        /// </summary>
        public string AttatchFilsRTF { get; set; }

        /// <summary>
        /// 챗 링크
        /// </summary>
        public List<Controls.HyperLink> ChattLinks { get; set; }

        /// <summary>
        /// 첨부파일 링크
        /// </summary>
        public List<Controls.HyperLink> AttatchFileLinks { get; set; }

        public string Bigo { get; set; }
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        /// <summary>
        /// 장소에 놓인 참석자
        /// </summary>
        public List<PlayerDragDropData> PlaceInPlayers { get; set; }

        public MinutesInfo()
        {
            Records = new List<Record>();
            ChattLinks = new List<Controls.HyperLink>();
            AttatchFileLinks = new List<Controls.HyperLink>();
            PlaceInPlayers = new List<PlayerDragDropData>();
        }

        public MinutesInfo(MinutesUnit unit) : this()
        {
            Unit = unit;
        }
         
        internal void Save()
        {
            // 프로젝트 목록 저장하기!
            string dir = Unit.GetFolderName();
            string fileName = dir + MinutesUnit.ConfigFileName;
            // 프로젝트 목록 저장.
            string xml = this.Serialize();
            if (System.IO.File.Exists(fileName)) System.IO.File.Delete(fileName);
            System.IO.File.AppendAllText(fileName, xml);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing == false)
            {
                //if (MinutesUnitList != null)
                //{
                //    for (int loop = MinutesUnitList.Count - 1; loop >= 0; loop--)
                //    {
                //        using (MinutesUnitList[loop]) { }
                //    }
                //}
            }
            base.Dispose(disposing);
        }
         
        internal string ToDescript()
        {
            string ending = "";
            if (string.IsNullOrWhiteSpace(EndTime) == false)
            {
                ending = "(완료)";
            } 
            return $"[{Day} {Time}] {ending} {Place}";
        }

    }
}
