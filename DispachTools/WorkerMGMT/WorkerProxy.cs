using DispachTools.Messages;
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
        protected List<WorkerStateMessage> WorkerChangeStateMessage = new List<WorkerStateMessage>();
        public WorkerProxy(DisPachingConfig config) 
        {

           

        }

    

        public void HandleMessage(BaseMessage baseMessage)
        {
          
        }

       
    }
}

  