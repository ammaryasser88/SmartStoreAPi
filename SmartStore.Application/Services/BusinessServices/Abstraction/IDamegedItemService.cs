using SmartStore.Application.Responses;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Services.BusinessServices.Abstraction
{
    public interface IDamegedItemService
    {
        Task<ServiceResult> AddDamagedItemAsync(DamegedItemRequest request);
        Task<ServiceResult> UpdateDamagedItemAsync(int damagedItemId, DamegedItemRequest request);
        Task<ServiceResult> DeleteDamagedItemAsync(int damagedItemId);
    }
}
