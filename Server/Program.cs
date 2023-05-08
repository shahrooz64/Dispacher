using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {

        private static DispachTools.Dispach_Server DisPacherServer = new DispachTools.Dispach_Server("Test");
        static void Main(string[] args)
        {
            DisPacherServer.Start();

            Console.ReadLine();
        }
    }
}
