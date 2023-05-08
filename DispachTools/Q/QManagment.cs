using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools.Q
{
    internal class QManagment
    {
        public IModel Channel { get; private set; }
        private IConnection RabitConnection = null;
        string _DispacherId = "";
        public QManagment(string DispacherID)
        {
            _DispacherId = DispacherID;         
        }
        public   string Get_Managment_QName()
        {
            return string.Concat("Q_Managment_", _DispacherId);
        }
        public  string Get_Managment_ExName()
        {
            return string.Concat("Ex_Managment_", _DispacherId);
        }


        public  void Init()
        {
           
            var factory = new ConnectionFactory { HostName = "beta-agent-2", Port = 5672, UserName = "guest", Password = "guest" };
                RabitConnection = factory.CreateConnection();
                Channel = RabitConnection.CreateModel();
                Channel.QueueDeclare(queue: Get_Managment_QName(), durable: false, exclusive:false,autoDelete: false, arguments: null);
                Channel.ExchangeDeclare(Get_Managment_ExName(), ExchangeType.Direct, durable: false, autoDelete: false,arguments:null);
                Channel.QueueBind(queue: Get_Managment_QName(), Get_Managment_ExName(), string.Empty);
            
        }


        public EventingBasicConsumer CreateConsumer()
        {
           return new EventingBasicConsumer(Channel);
        }

        public void StartConSume(EventingBasicConsumer consumer)
        {
            Channel.BasicConsume(queue: Get_Managment_QName(), autoAck: true, consumer: consumer);
        }


        public void publish2DisPacher(byte[]  byteArray)
        {
            Channel.BasicPublish(Get_Managment_ExName(), "",null , byteArray);
        }


    }
}
