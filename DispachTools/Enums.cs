using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools
{

    
    public enum StateCode
    {
       Null=0,
       IamFree=1,
      
       
    }
    public enum DispachingEntityType
    {
        Null = 0,
        Worker=1,
        Dispacher=2,


    }


    public enum MesseageType
    {
        Null=0,
        WorkerStateMessage = 1,
      
    }


  
   

}
