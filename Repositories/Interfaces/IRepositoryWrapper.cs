namespace GameWORLD.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IGameWorldRepository GameWorldRepository { get; }

        void Save();
    }
}
