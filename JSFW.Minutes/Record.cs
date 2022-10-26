using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSFW.Minutes
{

    /*  
     *      레코드 대상
     
     *      > 참석자 선택 >
     *      > 참석자 왈 : 내용 입력 완료! > 
     *      > 자료변경 > 
     *      > 안건 선택 변경 >
     */

    public class Record : IAccept
    {
        Record IAccept.Record { get { return this; } }

        public Record()
        {
            Tick = DateTime.Now.Ticks;
        }

        //타임 틱!
        public long Tick { get; set; }

        public string Command { get; set; }

        public string TypeName { get; set; }

        public string TargetID { get; set; }

        public object Data { get; set; }
        public string Player { get; set; }

        public Record SetData(string command, string typeName, string targetID, object data )
        { 
            Command = command;
            TypeName = typeName;
            TargetID = targetID;
            Data = data;
            return this;
        }

        public void Acceptor(IVisit vist)
        {
            vist.Accept(this);
        }
    }

    /// <summary>
    /// 레코드
    /// </summary>
    public interface IAccept
    {
        Record Record { get; }
        void Acceptor(IVisit vist);
    }

    /// <summary>
    /// 레코드 플레이어
    /// </summary>
    public interface IVisit
    {
        void Accept(IAccept acc);
    }
     
}
