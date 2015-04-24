namespace GameStore.BLL.Filtering
{
    public class Filter<T>
    {
        public Filter<T> NextFilter { get; set; }

        public virtual void Execute(T container)
        {
            if (NextFilter != null)
            {
                NextFilter.Execute(container);
            }
        }

        public Filter<T> Register(Filter<T> filter)
        {
            NextFilter = filter;
            return NextFilter;
        }
    }
}