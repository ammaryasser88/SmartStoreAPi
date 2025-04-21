using SmartStore.Application.Responses;
using SmartStore.Domain.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Services.BusinessServices.Abstraction
{
    public interface IRevenueService
    {
        Task<ServiceResult> AddRevenueAsync(RevenueRequest request);
    }
}
