using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSFW.Minutes
{ 
    public class MinutesManager
    {
        public List<MinutesUnit> MinutesList { get; set; }

        public MinutesManager()
        {
            MinutesList = new List<MinutesUnit>();
        }

        public void Add(MinutesUnit unit)
        {
            MinutesList.Add(unit);
            unit.NewCreateInfoFile();
            Save();
        }

        public static void AllClear()
        {
            // 모두 지워.
            if (Directory.Exists(StaticConst.JSFW_NPT_DIR))
            {
                RemoveDirectoryAndFiles(StaticConst.JSFW_NPT_DIR);

                Directory.CreateDirectory(StaticConst.JSFW_NPT_DIR);
            }
            else
            {
                Directory.CreateDirectory(StaticConst.JSFW_NPT_DIR);
            }
        }

        private static void RemoveDirectoryAndFiles(string dir)
        {
            string[] files = Directory.GetFiles(dir);

            foreach (var file in files)
            {
                File.Delete(file);
            }

            string[] dirs = Directory.GetDirectories(dir);
            foreach (var item in dirs)
            {
                RemoveDirectoryAndFiles(item);
            }
            Directory.Delete(dir);
        }

        internal static string CreateMinutesID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public void Remove(MinutesUnit unit)
        {
            MinutesList.Remove(unit);
            unit.Remove();
            Save();
        }
         

        internal static readonly string ConfigFileName = @"MinutesManager.xml";
        internal void Save()
        {
            // 프로젝트 목록 저장하기!
            string dir = GetFolderName();
            string fileName = dir + ConfigFileName;
            // 프로젝트 목록 저장.
            string xml = this.Serialize();
            if (System.IO.File.Exists(fileName)) System.IO.File.Delete(fileName);
            System.IO.File.AppendAllText(fileName, xml);
        }

        public static MinutesManager Load()
        {
            string dir = GetFolderName();
            string fileName = dir + ConfigFileName;

            // 프로젝트 목록 저장.
            string xml = "";
            if (System.IO.File.Exists(fileName))
            {
                xml = File.ReadAllText(fileName);
                return xml.DeSerialize<MinutesManager>();
            }
            return null;
        }

        public static string GetFolderName()
        {
            return StaticConst.JSFW_NPT_DIR;
        }
    }


}
