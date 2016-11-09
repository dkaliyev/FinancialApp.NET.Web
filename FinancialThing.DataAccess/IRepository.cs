using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.DataAccess
{
    public interface IRepository<T, K> where T : class
    {
        Task<T> Add(T entity);
        Task Delete(T entity);
        Task<T> FindBy(Expression<Func<T, bool>> expression);
        Task<T> GetById(K id);
        Task<IQueryable<T>> GetQuery();
        Task SaveOrUpdate(T entity);
        Task Update(T entity);
    }
}
