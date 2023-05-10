using DispachTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    internal class Program
    {
        static DisPachingConfig configuration = new DisPachingConfig() { MyName = "W1", Topic = "Test",MyType=DispachingEntityType.Worker };
        static ConcreteWorker baseWorker= new ConcreteWorker(configuration);
        static void Main(string[] args)
        {
            
           Console.ReadLine();
        }
    }
}
