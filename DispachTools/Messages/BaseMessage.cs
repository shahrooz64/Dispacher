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
    [JsonSubtypes.KnownSubType(typeof(WorkerStateMessage), nameof(WorkerStateMessage))]
    public class BaseMessage
    {
        public virtual string Type { get; } =nameof(BaseMessage);
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
        public override string Type { get; } = nameof(WorkerStateMessage);
        public StateCode WorkerState { get; set; } = StateCode.Null;
        public bool IsHeartBeat = false;



    }


}
