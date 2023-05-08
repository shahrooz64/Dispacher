using DispachTools.InternalMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DispachTools
{
    public abstract class  BaseWorker
    {
        protected BaseWorker() { }

        public string WorkerId  { get; private set; }
        public string DispacherId { get; private set; }
       

        private SemaphoreSlim WorkerSemaphore = new SemaphoreSlim(1);
        private  DispachTools.InternalMessages.WorkerStateCode _WorkerState = WorkerStateCode.Null;
        protected CancellationTokenSource cts = new CancellationTokenSource();

        public  BaseWorker(string workerId,string dispacherId)
        {
          
            DispacherId=dispacherId;
            WorkerId = workerId;
          
        }

        public WorkerStateMessage ChengeState(WorkerStateCode state)
        {
            WorkerSemaphore.Wait();
            WorkerStateMessage result = null;
            try
            {
                _WorkerState = state;
                result = CreateChangeStateEvent();
            }
            finally
            {
                WorkerSemaphore.Release();
            }
            return result;

        }
        private WorkerStateMessage CreateChangeStateEvent()
        {
            var message = new WorkerStateMessage();
            message.From = WorkerId;
            message.To = DispacherId;
            message.WorkerState = _WorkerState;
            message.Direction = MessageDirection.W2D;
            message.IsHeartBeat = false;
            return message;
        }


        public DispachTools.InternalMessages.WorkerStateCode GetState() 
        {
            WorkerSemaphore.Wait();
            try
            {
                return _WorkerState;
            }
            finally
            {
                WorkerSemaphore.Release();
            }
        
        }


        public  WorkerStateMessage CreateHeartBeat()
        {
            var message = new WorkerStateMessage();
            message.From = WorkerId;
            message.To = DispacherId;
            message.WorkerState = GetState();
            message.Direction = MessageDirection.W2D;
            message.IsHeartBeat = true;
            return message;
        }


      
        public abstract void HanndelMessage(BaseMessage message, Action<BaseMessage> action);
     


        public void Stop()
        {
            cts.Cancel();
        }

    }
}
