namespace GameStore.BLL.Filtering
{
    public class PipeLine<T>
    {
        public Filter<T> Filter { get; set; }

        public Filter<T> BeginRegister(Filter<T> filter)
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