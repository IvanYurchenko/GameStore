namespace GameStore.DAL.Interfaces
{
    public interface IDbSynchronizer
    {
        /// <summary>
        /// Synchronizes the databases.
        /// </summary>
        void SynchronizeDatabases();
    }
}