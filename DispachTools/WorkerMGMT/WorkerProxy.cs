using DispachTools.InternalMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DispachTools.WorkerMGMT
{
    internal class WorkerProxy:BaseWorker
    {
        private SemaphoreSlim WorkerSemaphore = new SemaphoreSlim(1);
        protected List<BaseMessage> AllRecivedMessage = new List<BaseMessage>();
        protected List<StateMessage> WorkerChangeStateMessage = new List<StateMessage>();
        public WorkerProxy(DisPachingConfig config) : base(config)
        {

           

        }

    

        public void HandleMessage(string Message)
        {
            Console.WriteLine(Message); 
        }

       
    }
}

  