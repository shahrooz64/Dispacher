using DispachTools.InternalMessages;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DispachTools
{
    public class ConcreteWorker :BaseWorker
    {
      
        Q.QManagment qManagment = null;
        private SemaphoreSlim semPublishEvent = new SemaphoreSlim(1);
        private Thread HeartBeatThread = null;
       public ConcreteWorker(string workerId , string dispacherId):base(workerId, dispacherId)
       {
          

            qManagment = new Q.QManagment(dispacherId);
            qManagment.Init();
            var message = this.ChengeState(WorkerStateCode.IamFree);
            PublicEvent2Dispacher(message);
            HeartBeatThread = new Thread(new ThreadStart(HeartBeat));
            HeartBeatThread.Start();





       }

        public override void HanndelMessage(BaseMessage message, Action<BaseMessage> action)
        {
            throw new NotImplementedException();
        }

        public void PublicEvent2Dispacher(WorkerStateMessage m)
       {
            semPublishEvent.Wait();
            try 
            {
                qManagment.publish2DisPacher(m.ToJsonByteArray());

            }
            catch(Exception ex) { }
            finally { semPublishEvent.Release(); }

       }

       private void HeartBeat()
        {
            while (!cts.IsCancellationRequested)
            {
                var heartbeatMessage = CreateHeartBeat();
                PublicEvent2Dispacher(heartbeatMessage);
                Console.WriteLine(heartbeatMessage.ToJson());
                Thread.Sleep(500);
            }
        }

       





    }
}
 