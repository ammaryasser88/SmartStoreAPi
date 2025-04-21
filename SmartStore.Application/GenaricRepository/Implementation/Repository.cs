using Microsoft.EntityFrameworkCore;
using SmartStore.Application.GenaricRepository.Abstraction;
using SmartStore.Domain;
using SmartStore.Domain.Context;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static SmartStore.Application.GenaricRepository.Abstraction.IRepository;

namespace SmartStore.Application.GenaricRepsitory.Implementation
{

    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _dbContext;
        private DbSet<T> _dbSet;
        private IRepository<T> _repositoryImplementation;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            var result = _dbSet.Add(entity);
            return result.Entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                var result = await _dbSet.AddAsync(entity);
                return result.Entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Delete(T entity)
        {
            var exist = _dbSet.Find(entity);
            if (exist == null)
                throw new Exception("Entity not found");
            _dbSet.Remove(exist);
        }

        public void Delete(object id)
        {
            var exist = _dbSet.Find(id);

            if (exist == null)
                throw new Exception("Entity not found");

            _dbSet.Remove(exist);
        }

        public void Delete(long id)
        {
            var exist = _dbSet.Find(id);

            if (exist == null)
                throw new Exception("Entity not found");

            _dbSet.Remove(exist);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                throw new Exception("Entity not Found");

            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            var exist = await _dbSet.FindAsync(entity);
            if (exist == null)
                throw new Exception("Entity not found");
            _dbSet.Remove(exist);
        }

        public T Update(T entity)
        {
            var result = _dbSet.Update(entity);
            return result.Entity;
        }

        public T UpdateSync(T entity)
        {
            try
            {
                var updatedEntity = _dbContext.Set<T>().Update(entity);
                updatedEntity.State = EntityState.Modified;
                return updatedEntity.Entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            return await Task.FromResult(true);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> AsQueryable(Expression<Func<T, bool>> predict)
        {
            return _dbSet.AsQueryable().Where(predict);
        }

        public IQueryable<T> AsQueryableInclude(params Expression<Func<T, object>>[] including)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in including)
            {
                query = query.Include(include).AsQueryable();
            }
            return query;
        }

        public T GetIncluding(Expression<Func<T, object>> including, Expression<Func<T, bool>> filter)
        {
            return _dbSet.Include(including).Where(filter).AsNoTracking().FirstOrDefault();
        }

        public async Task<T> GetIncludingAsync(Expression<Func<T, object>> including, Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Include(including).Where(filter).AsNoTracking().FirstOrDefaultAsync();
        }

        public T Get(Expression<Func<T, bool>> predict)
        {
            return _dbSet.Where(predict).AsNoTracking().FirstOrDefault();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predict)
        {
            return await _dbSet.AsNoTracking().Where(predict).FirstOrDefaultAsync(predict);
        }

        public long Count(Expression<Func<T, bool>> predict)
        {
            return _dbSet.Where(predict).Count();
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> predict)
        {
            return await _dbSet.Where(predict).CountAsync();
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> predict)
        {
            return _dbSet.Where(predict);
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predict)
        {
            return await _dbSet.Where(predict).ToListAsync();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predict)
        {
            return _dbSet.Where(predict);
        }

        public IQueryable<T> GetListIncluding(Expression<Func<T, object>> including, Expression<Func<T, bool>> filter)
        {
            return _dbSet.Include(including).Where(filter);
        }

        public IQueryable<T> GetListIncludingSync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] including)
        {
            var query = _dbSet.AsQueryable();
            foreach (var include in including)
            {
                query = query.Include(include).AsQueryable();
            }
            query = query.Where(filter);
            return query;
        }

        public IQueryable<T> GetListIncludingAsync(Expression<Func<T, object>> including)
        {
            return _dbSet.Include(including);
        }

        public async Task<T> GetIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }
            var item = await queryable.Where(predicate).AsNoTracking().FirstOrDefaultAsync();
            return item;
        }

        public T GetIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }
            var item = queryable.Where(predicate).AsNoTracking().FirstOrDefault();
            return item;
        }

        public async Task<T> GetIncludeAndThenIncludeAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> include, Expression<Func<T, object>> thenInclude)
        {
            IQueryable<T> queryable = GetAll();

            queryable = queryable.Include<T, object>(include);
            var item = await queryable.Where(predicate).FirstOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<T>> GetListIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }
            var item = await queryable.Where(predicate).ToListAsync();
            return item;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<T>> FromSqlAsync(string storedName, object[] parameters)
        {
            try
            {
                var listFromDb = await _dbContext.Set<T>().FromSqlRaw(storedName, parameters).ToListAsync();
                return listFromDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
        public async Task UpdateWithConcurrencyAsync(T entity)
        {
            if (entity is not IHasRowVersion rowVersionEntity)
                throw new InvalidOperationException("This entity does not support concurrency.");

            // تعيين القيمة الأصلية للـ RowVersion
            _dbContext.Entry(entity).Property("RowVersion").OriginalValue = rowVersionEntity.RowVersion;

            _dbContext.Set<T>().Update(entity);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("تم تعديل العنصر من مستخدم آخر. يرجى تحديث الصفحة والمحاولة مجددًا.");
            }
        }


    }
    
}
