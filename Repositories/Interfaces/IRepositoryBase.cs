using System.Linq.Expressions;

namespace GameWORLD.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task SaveChangesAsync();
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
