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
    public interface ICustomerService
    {
        Task<ServiceResult> AddCustomerAsync(CustomerRequest request);
        Task<PaginationObject<CustomerResponse>> GetCustomersAsync(int pageIndex);
        Task<ServiceResult> DeleteCustomerAsync(int customerId);
        Task<ServiceResult> UpdateCustomerAsync(int customerId, CustomerRequest request);
        Task<PaginationObject<CustomerResponse>> SearchCustomerAsync(string input, int pageIndex);
        Task<CustomerResponse> GetCustomerByIdAsync(int customerId);
    }
}
