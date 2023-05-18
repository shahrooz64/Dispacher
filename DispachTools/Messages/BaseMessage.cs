using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools.Messages
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
  

   
    public class BaseMessage
    {
        public enum eMType { BaseMessage = 0, WorkerStateMessage = 1, Command = 2 ,CommandResp}
        public virtual eMType Type { get; } = eMType.BaseMessage;
        public string From { get; set; }
        //public string To { get; set; }
        public DispachingEntityType SenderType { get; set; }= DispachingEntityType.Null;
        public DateTime MessageDateTime { get; set; } = DateTime.Now;
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }

    public class WorkerStateMessage : BaseMessage
    {
        public override eMType Type { get; } = eMType.WorkerStateMessage;
        public StateCode WorkerState { get; set; } = StateCode.Null;
      

    }

    public class Command : BaseMessage
    {
        public override eMType Type { get; } = eMType.Command;
        public string CommandID { get; set; } = "";

        public string WorkerName = "";


    }
    public class CommandResp : Command
    {
        public override eMType Type { get; } = eMType.CommandResp;
        public string CommandID { get; set; } = "";


    }


}
