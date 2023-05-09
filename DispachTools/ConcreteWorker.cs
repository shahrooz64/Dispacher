using DispachTools.InternalMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DispachTools
{
    public class ConcreteWorker :BaseWorker,IMyMessageHandler
    {
      
       
        private SemaphoreSlim semPublishEvent = new SemaphoreSlim(1);
        private Thread HeartBeatThread = null;
        BrokerHandler brokerHandler = null;

       public ConcreteWorker(string workerId , string dispacherId):base(workerId,dispacherId)
       {

            brokerHandler = new BrokerHandler(dispacherId, this);
            HeartBeatThread = new Thread(new ThreadStart(HeartBeat));
            HeartBeatThread.Start();


        


       }

        public void HandleMessage(string Message)
        {
           Console.WriteLine(Message);
        }

        private void HeartBeat()
        {
            while (!cts.IsCancellationRequested)
            {
                var heartbeatMessage = CreateHeartBeat();
                brokerHandler.Publish(heartbeatMessage);
                Thread.Sleep(500);
            }
        }

       





    }
}
 