using DispachTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static DisPachingConfig configuration = new DisPachingConfig() { MyName = "D1", Topic = "Test", IsWorker = false ,DispacherName="D1" };
        private static DispachTools.Dispach_Server DisPacherServer = new DispachTools.Dispach_Server(configuration);
        static void Main(string[] args)
        {
            DisPacherServer.Start();

            Console.ReadLine();
        }
    }
}
