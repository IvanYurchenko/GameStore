namespace GameStore.BLL.Interfaces
{
    public interface IPipeline<T>
    {
        IFilter<T> Filter { get; set; }
        IFilter<T> BeginRegister(IFilter<T> filter);
        void ExecuteAll(T container);
    }
}