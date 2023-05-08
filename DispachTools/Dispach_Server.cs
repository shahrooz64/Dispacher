using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using DispachTools.InternalMessages;

namespace DispachTools
{
    public class Dispach_Server
    {
        string _DisacherId;
       
        EventingBasicConsumer WorkerMessageConsumer=null;
        Q.QManagment QManagment=null;

        public Dispach_Server(string dispacherId)
        {
            QManagment=new Q.QManagment(dispacherId);
            QManagment.Init();
            _DisacherId = dispacherId;
            WorkerMessageConsumer = QManagment.CreateConsumer();



        }
        public void Start()
        {
            
            WorkerMessageConsumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;
                HandelManagmentMessages(message);


            };
            QManagment.StartConSume(WorkerMessageConsumer);



        }

        private void HandelManagmentMessages(string message)
        {
            var workerMessage = JsonConvert.DeserializeObject<WorkerStateMessage>(message);

            if (workerMessage.GetDispacherID().Equals(_DisacherId))
            {
                WorkerMGMT.WorkerCollection.Instance.HandelWorkerMessage(workerMessage);
            }






        }

      
        public void Stop()
        {

        }



       

    }

   


}
