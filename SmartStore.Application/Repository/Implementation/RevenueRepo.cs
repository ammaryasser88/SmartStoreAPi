using SmartStore.Application.GenaricRepsitory.Implementation;
using SmartStore.Application.Repository.Abstraction;
using SmartStore.Domain.Context;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Repository.Implementation
{
    public  class RevenueRepo : Repository<Revenue> ,IRevenueRepo
    {
        public RevenueRepo(SmartStoreContext smartStoreContext ):base(smartStoreContext)
        {
            
        }
    }
}
