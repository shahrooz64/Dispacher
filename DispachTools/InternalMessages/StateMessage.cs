using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools.InternalMessages
{
    public class StateMessage:BaseMessage
    {
        public StateCode WorkerState { get; set; } = StateCode.Null;
        public bool  IsHeartBeat=false;
        
      

    }
}
