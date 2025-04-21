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
    public interface ISafeService
    {
        Task<ServiceResult> AddSafeAsync(SafeRequest request);
        Task<PaginationObject<SafeResponse>> GetSafesAsync(int pageIndex);
        Task<ServiceResult> DeleteSafeAsync(int safeId);
        Task<ServiceResult> UpdateSafeAsync(int safeId, SafeRequest request);
        Task<PaginationObject<SafeResponse>> SearchSafeAsync(string input, int pageIndex);
        Task<SafeResponse> GetSafeByIdAsync(int safeId);
    }
}
