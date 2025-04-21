using SmartStore.Application.GenaricRepository.Abstraction;
using SmartStore.Application.GenaricRepsitory.Implementation;
using SmartStore.Application.UnitOfWork.Abstraction;
using SmartStore.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly SmartStoreContext _smartDbContext;
        public UnitOfWork(SmartStoreContext smartDbContext)
        {
            _smartDbContext = smartDbContext;
        }

        public async ValueTask DisposeAsync()
        {
            if (_smartDbContext != null)
            {
                await _smartDbContext.DisposeAsync();
            }
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.TryGetValue(type, out var repository))
            {
                repository = new Repository<T>(_smartDbContext);
                _repositories[type] = repository;
            }

            return (IRepository<T>)repository;
        }
        public async Task<int> SaveChangeAsync() => await _smartDbContext.SaveChangesAsync();
    }
}
