using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DispachTools.Messages;
using DispachTools.WorkerMGMT;

namespace DispachTools
{
    public class Dispach_Server:IMyMessageHandler
    {
        DisPachingConfig config = null;
        BrokerHandler _brokerHandler = null;
        Dictionary<string, WorkerProxy> dicworkers = new Dictionary<string, WorkerProxy>();
        public Dispach_Server(DisPachingConfig config)
        {
           this.config = config;
            _brokerHandler = new BrokerHandler(config,this);
            


        }

        public void HandleMessage(BaseMessage objMessage)
        {

            lock (this)
            {
                HandelWorkerStateMesssage(objMessage);
                




            }
        }

        private void HandelWorkerStateMesssage(BaseMessage objMessage)
        {
            if ( objMessage.SenderType == DispachingEntityType.Worker && (objMessage is WorkerStateMessage)  )
            {
                if (!dicworkers.ContainsKey(objMessage.From))
                {
                    var WorkerConfig = (DisPachingConfig)this.config.Clone();
                    WorkerConfig.MyName = objMessage.From;
                    dicworkers.Add(objMessage.From, new WorkerProxy(WorkerConfig));
                }
            }

        }


      
      
        public void Start()
        {
            
        


        }

      
        



       

    }

   


}
