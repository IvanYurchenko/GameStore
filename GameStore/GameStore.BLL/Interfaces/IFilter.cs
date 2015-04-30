namespace GameStore.BLL.Interfaces
{
    public interface IFilter<T>
    {
        /// <summary>
        /// A next filter that will be executed after the current one.
        /// </summary>
        /// <value>
        /// The next filter.
        /// </value>
        IFilter<T> NextFilter { get; set; }

        /// <summary>
        /// Registers the specified filter as next.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        IFilter<T> Register(IFilter<T> filter);

        /// <summary>
        /// Executes current filter on the specified container and calls <see cref="Execute"/> for the <see cref="NextFilter"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        void Execute(T container);
    }
}