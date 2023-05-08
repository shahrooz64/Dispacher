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

        public MessageDirection Direction { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public string GetDispacherID(){
            if (Direction == MessageDirection.D2W)
            {
                return From;
            }
            else { return To; }
        
        }
        public string GetWorkerId()
        {
            if (Direction == MessageDirection.W2D)
            {
                return From;
            }
            else { return To; }

        }


        DateTime MessageDateTime { get; set; } = DateTime.Now;

        public byte[] ToJsonByteArray()
        {
            return Encoding.UTF8.GetBytes(ToJson());
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
