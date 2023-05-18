using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools.Messages
{
    public static class MessageEx
    {
        
        public static BaseMessage ParsMessage(string message)
        {
            if(string.IsNullOrEmpty(message)) return null;
            var tmp=  JsonConvert.DeserializeObject<BaseMessage>(message);

            switch (tmp.Type)
            {
                case BaseMessage.eMType.WorkerStateMessage: return JsonConvert.DeserializeObject<WorkerStateMessage>(message); ;
                case BaseMessage.eMType.Command: return JsonConvert.DeserializeObject<Command>(message);
                case BaseMessage.eMType.CommandResp: return JsonConvert.DeserializeObject<CommandResp>(message);
            }
            return tmp;

        }

    }
}
