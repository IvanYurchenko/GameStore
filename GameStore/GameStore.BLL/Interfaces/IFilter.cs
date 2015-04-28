namespace GameStore.BLL.Interfaces
{
    public interface IFilter<T>
    {
        IFilter<T> NextFilter { get; set; }
        IFilter<T> Register(IFilter<T> filter);
        void Execute(T container);
    }
}