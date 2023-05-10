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

        public DisPachingConfig config = null;

       
        private  DispachTools.InternalMessages.StateCode _WorkerState = DispachTools.InternalMessages.StateCode.Null;
        protected CancellationTokenSource cts = new CancellationTokenSource();

        public  BaseWorker(DisPachingConfig config)
        {
          this.config = config;
          
        }

        public StateMessage ChengeState(StateCode state)
        {
          
            StateMessage result = null;
            try
            {
                _WorkerState = state;
                result = CreateChangeStateEvent();
            }
            finally
            {
               
            }
            return result;

        }
        private StateMessage CreateChangeStateEvent()
        {
            var message = new StateMessage();
            message.From = config.MyName;          
            message.WorkerState = _WorkerState;
            message.To = config.DispacherName;
            message.IsHeartBeat = false;
            return message;
        }


        public StateCode GetState() 
        {
           return _WorkerState;
        }


        public  StateMessage CreateHeartBeat()
        {
            var message = new StateMessage();
            message.From = config.MyName;          
            message.WorkerState = GetState();
            message.To = config.DispacherName;
            message.IsHeartBeat = true;
            return message;
        }


        public void Stop()
        {
            cts.Cancel();
        }

    }
}
