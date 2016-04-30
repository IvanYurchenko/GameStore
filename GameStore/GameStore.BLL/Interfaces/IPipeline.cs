namespace GameStore.BLL.Interfaces
{
    public interface IPipeline<T>
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        IFilter<T> Filter { get; set; }

        /// <summary>
        /// Registers the first filter in the pipeline.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        IFilter<T> BeginRegister(IFilter<T> filter);

        /// <summary>
        /// Executes all filters on target container.
        /// </summary>
        /// <param name="container">The container.</param>
        void ExecuteAll(T container);
    }
}