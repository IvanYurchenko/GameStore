using GameStore.BLL.Interfaces;

namespace GameStore.BLL.Filtering
{
    public abstract class BaseFilter<T> : IFilter<T>
    {
        public IFilter<T> NextFilter { get; set; }

        public virtual void Execute(T container)
        {
            if (NextFilter != null)
            {
                NextFilter.Execute(container);
            }
        }

        public IFilter<T> Register(IFilter<T> filter)
        {
            NextFilter = filter;
            return NextFilter;
        }
    }
}