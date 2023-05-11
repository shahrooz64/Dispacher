using DispachTools.Messages;
using NUnit.Framework;

namespace TestProject1
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
          WorkerStateMessage msg= new WorkerStateMessage();
          string json =msg.ToJson();
            var b = MessageEx.ParsMessage(json,  out var obj);
            Assert.IsTrue(obj!=null);







        }
    }
}