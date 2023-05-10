using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools.InternalMessages
{
    public class BaseMessage
    {

        public string From { get; set; }
        public string To { get; set; }
        public MesseageType MesseageType { get; set; } = MesseageType.Null;
        public DateTime MessageDateTime { get; set; } = DateTime.Now;
                
      
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        
        



    }
    public static class BaseMessageExtensions
    {
        public static bool IsValidMessage(this BaseMessage This,string MyName , string message)
        {
            var baseMessage = JsonConvert.DeserializeObject<BaseMessage>(message);
            if(DateTime.Now.Subtract ( baseMessage.MessageDateTime).TotalMinutes>5) return false;
            if (MyName.Equals(baseMessage.From)) return false;
            if (MyName.Equals(baseMessage.To) || baseMessage.To == "*")
            {
                    return true;
            }
            
            return false;


        }
    }

}
