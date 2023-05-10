using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools.Messages
{
    public class WorkerStateMessage:BaseMessage
    {
        public StateCode WorkerState { get; set; } = StateCode.Null;
        public bool  IsHeartBeat=false;
        
      

    }
}
