using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Filtering;

namespace GameStore.BLL.Interfaces
{
    public interface IPipeline<T>
    {
        IFilter<T> Filter { get; set; }
        IFilter<T> BeginRegister(IFilter<T> filter);
        void ExecuteAll(T container);
    }
}
