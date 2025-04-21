using SmartStore.Application.GenaricRepository.Abstraction;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Repository.Abstraction
{
    public interface ISafeTransactionRepo : IRepository<SafeTransaction>
    {
    }
}
