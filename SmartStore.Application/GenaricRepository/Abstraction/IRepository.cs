using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.GenaricRepository.Abstraction
{
 
        public interface IRepository<T> : IRepository, IDisposable where T : class
        {
            T Add(T entity);
            Task<T> AddAsync(T entity);
            IEnumerable<T> AddRange(IEnumerable<T> entities);
            Task<bool> AddRangeAsync(List<T> entities);
            void Delete(T entity);
            void Delete(object id);
            void Delete(long id);

            void Remove(T entity);
            T Update(T entity);
            IQueryable<T> GetAll();
            IQueryable<T> AsQueryable();
            IQueryable<T> AsQueryableInclude(params Expression<Func<T, object>>[] including);
            IQueryable<T> AsQueryable(Expression<Func<T, bool>> predict);
            Task<T> GetByIdAsync(int id);
            Task DeleteAsync(T entity);

            T GetIncluding(Expression<Func<T, object>> including, Expression<Func<T, bool>> filter);
            Task<T> GetIncludingAsync(Expression<Func<T, object>> including, Expression<Func<T, bool>> filter);
            Task<bool> UpdateRangeAsync(IEnumerable<T> entities);
            Task<List<T>> GetListAsync(Expression<Func<T, bool>> predict);
            IQueryable<T> GetList(Expression<Func<T, bool>> predict);
            IQueryable<T> GetAll(Expression<Func<T, bool>> predict);
            IQueryable<T> GetListIncluding(Expression<Func<T, object>> including, Expression<Func<T, bool>> filter);
            IQueryable<T> GetListIncludingAsync(Expression<Func<T, object>> including);
            void DeleteRange(IEnumerable<T> entities);
            T Get(Expression<Func<T, bool>> predict);
            Task<T> GetAsync(Expression<Func<T, bool>> predict);
            Task<long> CountAsync(Expression<Func<T, bool>> predict);
            long Count(Expression<Func<T, bool>> predict);
            Task<T> GetIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
            T GetIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
            Task<IEnumerable<T>> GetListIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
            Task<IEnumerable<T>> FromSqlAsync(string storedName, object[] parameters);
            Task BeginTransactionAsync();
            Task CommitTransactionAsync();
            Task RollbackTransactionAsync();
            Task UpdateWithConcurrencyAsync(T entity);
    }

        public interface IRepository
        {

        }
    
}
