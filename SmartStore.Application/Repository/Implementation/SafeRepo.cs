using Microsoft.EntityFrameworkCore;
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
    public class SafeRepo:Repository<Safe>,ISafeRepo
    {
        public SafeRepo(SmartStoreContext smartStoreContext ):base( smartStoreContext ) 
        {
            
        }

        
    }
}
