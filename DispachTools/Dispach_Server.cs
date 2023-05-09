using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DispachTools.InternalMessages;

namespace DispachTools
{
    public class Dispach_Server:IMyMessageHandler
    {
        string _DisacherId;
        BrokerHandler _brokerHandler = null;
        public Dispach_Server(string dispacherId)
        {
           this._DisacherId = dispacherId;
            _brokerHandler = new BrokerHandler(_DisacherId, this);
            


        }

        public void HandleMessage(string Message)
        {
            Console.WriteLine(Message);
        }

        public void Start()
        {
            
        


        }

      
        



       

    }

   


}
