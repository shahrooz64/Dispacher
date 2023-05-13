using Confluent.Kafka;
using Confluent.Kafka.Admin;
using DispachTools.Messages;
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
        private static string KafkaServers = "";
        private static string userName="";
        private static string pass = "";


        DisPachingConfig config = null;
        private ProducerConfig ProducerConfig { get; set; }
        private ConsumerConfig ConsumerConfig { get; set; }
        private IConsumer<Ignore,string> consumer { get; set; }
        private Thread ThreadConSumer = null;

        protected CancellationTokenSource cts = new CancellationTokenSource();
        IMyMessageHandler _handler = null;
        private SemaphoreSlim semPublishEvent = new SemaphoreSlim(1);
        public BrokerHandler(DisPachingConfig config,IMyMessageHandler handler)
        {
            this.config = config;         
            _handler = handler;
            
            Init();
            ThreadConSumer = new Thread(new ThreadStart(ConsumeLoop));
            ThreadConSumer.Start(); 

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
                    SaslUsername =userName,
                    SaslPassword = pass,

                };
                IAdminClient admin = new AdminClientBuilder(adminConfig).Build();

                admin.CreateTopicsAsync(new[]
                {
                    new TopicSpecification()
                    {
                        Name = config.Topic,
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
                SaslUsername = userName,
                SaslPassword = pass,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId =config.GetKafkaConsumerGroupId(),
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.RoundRobin,
                AllowAutoCreateTopics = true,
                
            };
            consumer = new ConsumerBuilder<Ignore,string>(consumerConfig).Build();
            consumer.Subscribe(config.Topic);


            ProducerConfig = new ProducerConfig
            {
                BootstrapServers = KafkaServers,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslPlaintext,
                Partitioner = Partitioner.Consistent,
                Acks = Acks.All,
                SaslUsername = userName,
                SaslPassword = pass,
                MessageTimeoutMs = 0,
                RetryBackoffMs = 100,
                EnableIdempotence = true,

            };

            }

        public void Publish(BaseMessage message)
        {
            semPublishEvent.Wait();
            try
            {
               
                using (var producer = new ProducerBuilder<Null, string>(ProducerConfig).SetErrorHandler(ProducerErrorHandler).Build())
                {
                    producer.Produce(config.Topic, new Message<Null, string>()
                    {
                        Value = message.ToJson()
                    }, ProducerDeliveryHandler);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                semPublishEvent.Release();
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
