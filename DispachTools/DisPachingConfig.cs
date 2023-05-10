using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools
{
    public  class DisPachingConfig
    {
        public string MyName = "";
        public string DispacherName = "";
        public string Topic = "";
        public bool IsWorker=true;

        public string GetKafkaConsumerGroupId()
        {
            return Topic + "_Consumer_" + MyName;
        }
        


    }
}
