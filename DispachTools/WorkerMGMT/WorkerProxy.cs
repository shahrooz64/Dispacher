using DispachTools.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace DispachTools.WorkerMGMT
{
    internal class WorkerProxy:BaseWorker,IMyMessageHandler
    {
        internal WorkerProxy(DisPachingConfig config) : base(config)
        {

        }
        

        private SemaphoreSlim WorkerSemaphore = new SemaphoreSlim(1);
        protected ConcurrentStack<BaseMessage> AllRecivedMessage = new ConcurrentStack<BaseMessage>();
        protected ConcurrentStack<WorkerStateMessage> WorkerChangeStateMessage = new ConcurrentStack<WorkerStateMessage>();
         
    
        
        public void HandleMessage(BaseMessage objmessage)
        {
            throw new NotImplementedException();
        }
    }
}

  