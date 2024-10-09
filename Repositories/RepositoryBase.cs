using GameWORLD.Data;
using GameWORLD.Models;
using GameWORLD.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameWORLD.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        
        protected GameWORLDContext GameWORLDContext { get; set; }

        public Task SaveChangesAsync()
        {
            GameWORLDContext.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public RepositoryBase(GameWORLDContext locationContext)
        {
            this.GameWORLDContext = locationContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.GameWORLDContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.GameWORLDContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.GameWORLDContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.GameWORLDContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.GameWORLDContext.Set<T>().Remove(entity);
        }
    }
}
