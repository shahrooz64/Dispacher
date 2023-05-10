using DispachTools.Messages;
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

        public DisPachingConfig config = null;

       
        private StateCode _WorkerState =StateCode.Null;
        protected CancellationTokenSource cts = new CancellationTokenSource();

        public  BaseWorker(DisPachingConfig config)
        {
          this.config = config;
          
        }

        public WorkerStateMessage ChengeState(StateCode state)
        {

            WorkerStateMessage result = null;
            lock (this)
            {
                _WorkerState = state;
                result = CreateChangeStateEvent();
            }
           
            return result;

        }
        private WorkerStateMessage CreateChangeStateEvent()
        {
            var message = new WorkerStateMessage();
            message.From = config.MyName;          
            message.WorkerState = _WorkerState;
            message.SenderType = config.MyType;
            message.IsHeartBeat = false;
            return message;
        }


        public StateCode GetState() 
        {
            lock (this)
            {
                return _WorkerState;
            }
        }


        public WorkerStateMessage CreateHeartBeat()
        {
            var message = new WorkerStateMessage();
            message.From = config.MyName;          
            message.WorkerState = GetState();
            message.SenderType = config.MyType;
            message.IsHeartBeat = true;
            return message;
        }


        public void Stop()
        {
            cts.Cancel();
        }

    }
}
