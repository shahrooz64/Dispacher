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
      
       
      
        private Thread HeartBeatThread = null;
        BrokerHandler brokerHandler = null;

       public ConcreteWorker(DisPachingConfig config):base(config)
       {

            brokerHandler = new BrokerHandler(config, this);
             
            HeartBeatThread = new Thread(new ThreadStart(HeartBeat));
            HeartBeatThread.Start();


        


       }

        public virtual void HandleMessage(string Message)
        {
            var baseMessage = JsonConvert.DeserializeObject<BaseMessage>(Message);
            if (baseMessage.IsValidMessage(config.MyName,null, Message))
            {
                Console.WriteLine(Message);
            }


         
        }
        private void HeartBeat()
        {
            while (!cts.IsCancellationRequested)
            {
                var heartbeatMessage = CreateHeartBeat();
                brokerHandler.Publish(heartbeatMessage);
                Thread.Sleep(1000);
            }
        }

       





    }
}
 