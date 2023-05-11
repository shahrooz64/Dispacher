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
        public static bool IsValidMessage(this BaseMessage This, string MyName, DateTime? ValidFrom, string message)
        {
            var baseMessage = JsonConvert.DeserializeObject<BaseMessage>(message);
            if (ValidFrom != null && This.MessageDateTime < ValidFrom.Value) return false;
            if (DateTime.Now.Subtract(baseMessage.MessageDateTime).TotalMinutes > 5) return false;
            if (MyName.Equals(baseMessage.From)) return false;

            return false;


        }
        public static bool ParsMessage(string strMessage,out BaseMessage message)
        {
            message = null;



            return message != null;
        }

    }
}
