using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispachTools
{
    public interface IData<T>
    {
        string WorkerSelectionMetric(T t);
    }
}
