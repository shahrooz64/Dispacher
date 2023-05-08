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
        static ConcreteWorker baseWorker= new ConcreteWorker(Guid.NewGuid().ToString(), "Test");
        static void Main(string[] args)
        {
            
           Console.ReadLine();
        }
    }
}
