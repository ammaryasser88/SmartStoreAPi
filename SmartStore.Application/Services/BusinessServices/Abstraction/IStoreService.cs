using SmartStore.Application.Responses;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Dtos.Response;
using SmartStore.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Services.BusinessServices.Abstraction
{
    public interface IStoreService
    {
        Task<ServiceResult> AddStoreAsync(StoreRequest request);
        Task<PaginationObject<StoreResponse>> GetStoresAsync(int pageIndex);
        Task<ServiceResult> DeleteStoreAsync(int storeId);
        Task<ServiceResult> UpdateStoreAsync(int storeId, StoreRequest request);
        Task<PaginationObject<StoreResponse>> SearchStoreAsync(string input, int pageIndex);
        Task<StoreResponse> GetStoreByIdAsync(int storeId);
    }
}
