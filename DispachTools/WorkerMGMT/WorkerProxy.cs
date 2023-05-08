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
        protected List<WorkerStateMessage> WorkerChangeStateMessage = new List<WorkerStateMessage>();
        public WorkerProxy(string workerId,string dispacherId) : base(workerId, dispacherId)
        {

           

        }

        public override void HanndelMessage(BaseMessage message, Action<BaseMessage> action)
        {
            WorkerSemaphore.Wait();
            try
            {
                if (message is WorkerStateMessage  )
                {

                    var workerStateMessage = (WorkerStateMessage)message;
                    AllRecivedMessage.Add(workerStateMessage);
                    WorkerChangeStateMessage.Add(workerStateMessage);
                    this.ChengeState(workerStateMessage.WorkerState);

                }
                if (action != null)
                {
                    action(message);
                }

            }
            catch (Exception ex)
            {

            }
            finally { WorkerSemaphore.Release(); }

        }

    }
}

  