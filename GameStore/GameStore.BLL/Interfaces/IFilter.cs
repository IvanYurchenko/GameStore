using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Filtering;

namespace GameStore.BLL.Interfaces
{
    public interface IFilter<T>
    {
        IFilter<T> NextFilter { get; set; }
        IFilter<T> Register(IFilter<T> filter);
        void Execute(T container);
    }
}
