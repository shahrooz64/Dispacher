﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools
{
    public  class DisPachingConfig
    {
        public string MyName = "";
        public string Topic = "";
        public  DispachingEntityType      MyType=DispachingEntityType.Null;
        public  DateTime StartTime=DateTime.Now;
        public string GetKafkaConsumerGroupId()
        {
            return Topic + "_Consumer_" + MyName;
        }
        


    }
}
