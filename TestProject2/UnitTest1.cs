using DispachTools.Messages;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections;

namespace TestProject2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var heartbeat = new Command();
            var json= heartbeat.ToJson();

            var b1 = JsonConvert.DeserializeObject<BaseMessage>(json);
            var h1 = JsonConvert.DeserializeObject<Command>(json);
            var h2=b1 as Command;
            int i = 0;
        }

      
    }
}