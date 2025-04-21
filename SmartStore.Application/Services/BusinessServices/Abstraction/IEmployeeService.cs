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
    public interface IEmployeeService
    {
        Task<ServiceResult> AddEmployeeAsync(EmployeeRequest request);
        Task<PaginationObject<EmployeeResponse>> GetEmployeesAsync(int pageIndex);
        Task<ServiceResult> DeleteEmployeeAsync(int employeeId);
        Task<ServiceResult> UpdateEmployeeAsync(int employeeId, EmployeeRequest request);
        Task<PaginationObject<EmployeeResponse>> SearchEmployeeAsync(string input, int pageIndex);
        Task<EmployeeResponse> GetEmployeeByIdAsync(int employeeId);
    }
}
