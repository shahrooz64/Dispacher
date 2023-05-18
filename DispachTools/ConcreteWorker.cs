using DispachTools.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DispachTools
{
    public class ConcreteWorker :BaseWorker,IMyMessageHandler
    {
      
       
     
        BrokerHandler brokerHandler = null;

       public ConcreteWorker(DisPachingConfig config):base(config)
       {

            brokerHandler = new BrokerHandler(config, this);
             
        


        


       }

      

        public void HandleMessage(BaseMessage objmessage)
        {
            if (objmessage!=null && objmessage is Command)
            {

            }
        }
    }
}
 