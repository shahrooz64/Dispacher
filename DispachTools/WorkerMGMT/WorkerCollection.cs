using DispachTools.InternalMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DispachTools.WorkerMGMT
{
    internal class WorkerCollection:IMyMessageHandler
    {
        private WorkerCollection() { }

        public static WorkerCollection _instance = null;
        public static WorkerCollection Instance
        {
            get
            {
                if (_instance == null) _instance = new WorkerCollection();
                return _instance;
            }
        }

        Dictionary<string, WorkerProxy> dicworkers = new Dictionary<string, WorkerProxy>();
        SemaphoreSlim _Sem_workers = new SemaphoreSlim(1);

        private void HandelWorkerMessage(WorkerStateMessage message)
        {
            _Sem_workers.Wait();
            try
            {
                if (!dicworkers.ContainsKey(message.GetWorkerId()))
                    dicworkers.Add(message.GetWorkerId(),  new WorkerProxy(message.GetWorkerId(), message.GetDispacherID()));
                   
            }
            finally
            {
                _Sem_workers.Release();
            }

           // dicworkers[message.GetWorkerId()].HanndelMessage(message, null);
        }

        public void HandleMessage(string message)
        {
           Console.WriteLine(message);
        }
    }

}


