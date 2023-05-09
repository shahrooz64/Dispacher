using Confluent.Kafka;
using Confluent.Kafka.Admin;
using DispachTools.InternalMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DispachTools
{
    public class BrokerHandler
    {
        private static string KafkaServers = "beta-agent-2:9093";
        string _DispacherId = "";
        string _DisPacherName = "";
        private ProducerConfig ProducerConfig { get; set; }
        private ConsumerConfig ConsumerConfig { get; set; }
        private IConsumer<Ignore,string> consumer { get; set; }
        private Thread ThreadConSumer = null;

        protected CancellationTokenSource cts = new CancellationTokenSource();
        IMyMessageHandler _handler = null;

        public BrokerHandler(string DispacherID,IMyMessageHandler handler)
        {
            
            _DispacherId = DispacherID;
            _handler = handler;
            
            Init();
            ThreadConSumer = new Thread(new ThreadStart(ConsumeLoop));
            ThreadConSumer.Start(); 

        }
        public string Get_ManagmentTopic()
        {
            return string.Concat(_DispacherId, "_ManagmentTopic" );
        }


        private void Init()
        {
            try
            {
                var adminConfig = new AdminClientConfig()
                {
                    BootstrapServers = KafkaServers,
                    SaslMechanism = SaslMechanism.Plain,
                    SecurityProtocol = SecurityProtocol.SaslPlaintext,
                    Acks = Acks.All,
                    SaslUsername = "kafka",
                    SaslPassword = "rvm9nYNzrD4MTkKt",

                };
                IAdminClient admin = new AdminClientBuilder(adminConfig).Build();

                admin.CreateTopicsAsync(new[]
                {
                    new TopicSpecification()
                    {
                        Name = Get_ManagmentTopic(),
                        NumPartitions = 1,
                        ReplicationFactor = 2,
                        Configs = new Dictionary<string,string>()
                        {
                            ["retention.ms"] = TimeSpan.FromMinutes(1).TotalMilliseconds.ToString()
                        },
                    }
                });



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            var consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = KafkaServers,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslPlaintext,
                SaslUsername = "kafka",
                SaslPassword = "rvm9nYNzrD4MTkKt",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = Get_ManagmentTopic()+"_Consumer_"+Guid.NewGuid().ToString(),
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.RoundRobin,
                AllowAutoCreateTopics = true,
                
            };
            consumer = new ConsumerBuilder<Ignore,string>(consumerConfig).Build();
            consumer.Subscribe(Get_ManagmentTopic());


            ProducerConfig = new ProducerConfig
            {
                BootstrapServers = KafkaServers,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslPlaintext,
                Partitioner = Partitioner.Consistent,
                Acks = Acks.All,
                SaslUsername = "kafka",
                SaslPassword = "rvm9nYNzrD4MTkKt",
                MessageTimeoutMs = 0,
                RetryBackoffMs = 100,
                EnableIdempotence = true,

            };

            }

        public void Publish(BaseMessage message)
        {

            var jsonMessage = JsonConvert.SerializeObject(message);

            using (var producer = new ProducerBuilder<Null, string>(ProducerConfig).SetErrorHandler(ProducerErrorHandler).Build())
            {
                producer.Produce(Get_ManagmentTopic(), new Message<Null, string>()
                {
                    Value = jsonMessage                         }, ProducerDeliveryHandler);
            }




        }

        void ProducerDeliveryHandler(DeliveryReport<Null, string> report)
        {
            if (report.Status == PersistenceStatus.Persisted)
            {

            }
            else
            {

            }
           
        
        }

        private void ProducerErrorHandler(IProducer<Null, string> producer, Confluent.Kafka.Error error)
        {
            producer.Dispose();
            if (true)
            {

            }
            else
            {

            }
        }


        private void ConsumeLoop()
        {

            while (true)
            {
                try
                {
                    var result = consumer.Consume();
                    if (_handler != null)
                    {
                        _handler.HandleMessage(result.Message.Value);
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    break;
                }
            }



        }




    }
    }
