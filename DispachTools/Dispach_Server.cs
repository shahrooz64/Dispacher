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
        public Dispach_Server(DisPachingConfig config)
        {
           this.config = config;
            _brokerHandler = new BrokerHandler(config,this);
            


        }

        public void HandleMessage(string Message)
        {
            var baseMessage = JsonConvert.DeserializeObject<BaseMessage>(Message);
            if (baseMessage.IsValidMessage(config.MyName,null ,Message))
            {
                WorkerCollection.Instance.HandleMessage(Message);
              
            }
        }

      
        public void Start()
        {
            
        


        }

      
        



       

    }

   


}
