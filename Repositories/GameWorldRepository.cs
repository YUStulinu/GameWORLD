using GameWORLD.Data;
using GameWORLD.Models;
using GameWORLD.Repositories.Interfaces;

namespace GameWORLD.Repositories
{
    public class GameWorldRepository : RepositoryBase<GameWORLDContext>, IGameWorldRepository
    {
        public GameWorldRepository(GameWORLDContext locationContext)
            : base(locationContext)
        {
        }
    }
}
