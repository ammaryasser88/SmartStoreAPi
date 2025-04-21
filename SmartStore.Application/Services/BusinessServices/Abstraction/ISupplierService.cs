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
    public interface ISupplierService
    {
        Task<ServiceResult> AddSupplierAsync(SupplierRequest request);
        Task<PaginationObject<SupplierResponse>> GetSuppliersAsync(int pageIndex);
        Task<ServiceResult> DeleteSupplierAsync(int supplierId);
        Task<ServiceResult> UpdateSupplierAsync(int supplierId, SupplierRequest request);
        Task<PaginationObject<SupplierResponse>> SearchSupplierAsync(string input, int pageIndex);
        Task<SupplierResponse> GetSupplierByIdAsync(int customerId);
    }
}
