using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools.InternalMessages
{
    public class InternalMessage:BaseMessage
    {
        public WorkerStateCode WorkerState { get; set; } = WorkerStateCode.Null;
        public bool IsHeartBeat = false;

     

    }
}
