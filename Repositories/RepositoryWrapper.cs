using GameWORLD.Data;
using GameWORLD.Models;
using GameWORLD.Repositories.Interfaces;

namespace GameWORLD.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private GameWORLDContext _locationContext;
        private IGameWorldRepository? _locationRepository;

        public IGameWorldRepository GameWorldRepository
        {
            get
            {
                if (_locationRepository == null)
                {
                    _locationRepository = new GameWorldRepository(_locationContext);
                }

                return _locationRepository;
            }
        }

        public RepositoryWrapper(GameWORLDContext locationContext)
        {
            _locationContext = locationContext;
        }

        public void Save()
        {
            _locationContext.SaveChanges();
        }
    }
}
