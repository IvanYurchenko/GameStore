using GameStore.BLL.Interfaces;

namespace GameStore.BLL.Filtering
{
    /// <summary>
    /// Pipeline class. 
    /// </summary>
    /// <typeparam name="T">Container</typeparam>
    public class Pipeline<T> : IPipeline<T>
    {
        public IFilter<T> Filter { get; set; }

        public IFilter<T> BeginRegister(IFilter<T> filter)
        {
            Filter = filter;
            return Filter;
        }

        public void ExecuteAll(T container)
        {
            Filter.Execute(container);
        }
    }
}