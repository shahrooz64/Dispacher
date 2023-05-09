using DispachTools.InternalMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DispachTools.WorkerMGMT
{
    internal class WorkerProxy:BaseWorker,IMyMessageHandler
    {
        private SemaphoreSlim WorkerSemaphore = new SemaphoreSlim(1);
        protected List<BaseMessage> AllRecivedMessage = new List<BaseMessage>();
        protected List<WorkerStateMessage> WorkerChangeStateMessage = new List<WorkerStateMessage>();
        public WorkerProxy(string workerId,string dispacherId) : base(workerId, dispacherId)
        {

           

        }

    

        public void HandleMessage(string Message)
        {
            Console.WriteLine(Message); 
        }
    }
}

  