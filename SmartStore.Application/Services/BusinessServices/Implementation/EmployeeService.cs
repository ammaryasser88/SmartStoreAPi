using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartStore.Application.Repository.Abstraction;
using SmartStore.Application.Responses;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Application.UnitOfWork.Abstraction;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Dtos.Response;
using SmartStore.Domain.Entities;
using SmartStore.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Services.BusinessServices.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepo _repo;
        private readonly IMessageService _messageService;
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IEmployeeRepo repo, IMessageService messageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = repo;
            _messageService = messageService;
        }
        public async Task<ServiceResult> AddEmployeeAsync(EmployeeRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));

            var isExists = await _repo
                .GetAsync(c => c.NameArabic == request.NameArabic && c.IsDeleted == false);

            if (isExists != null)
                return ServiceResult.Failure(_messageService.GetMessage("EmployeeExists"));

            var entity = _mapper.Map<Employee>(request);

            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
        }

        public async Task<PaginationObject<EmployeeResponse>> GetEmployeesAsync(int pageIndex)
        {
            var query = (_repo.AsQueryable(i => i.IsDeleted == false))
            .OrderBy(i => i.NameArabic)
            .ProjectTo<EmployeeResponse>(_mapper.ConfigurationProvider);
            return await PaginationHelper.CreateAsync(query, pageIndex);
        }

        public async Task<ServiceResult> UpdateEmployeeAsync(int employeeId, EmployeeRequest request)
        {

            if (employeeId == 0)
            {
                return ServiceResult.Failure(_messageService.GetMessage("InvalidId"));
            }

            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
            {
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));
            }

            var entity = await _repo
                .GetAsync(ic => ic.EmployeeId == employeeId && ic.IsDeleted == false);

            if (entity == null)
            {
                return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
            }

            _mapper.Map(request, entity);
            _repo.Update(entity);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("UpdateSuccessfully"));
        }

        public async Task<ServiceResult> DeleteEmployeeAsync(int employeeId)
        {
            if (employeeId != 0)
            {
                var employee = await _repo
                 .GetAsync(ic => ic.EmployeeId == employeeId && ic.IsDeleted == false);

                if (employee != null)
                {
                    employee.IsDeleted = true;
                    _repo.Update(employee);
                    await _unitOfWork.SaveChangeAsync();

                    return ServiceResult.Success(_messageService.GetMessage("DeleteSuccessfully"));
                }
            }
            return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
        }

        public async Task<PaginationObject<EmployeeResponse>> SearchEmployeeAsync(string input, int pageIndex)
        {
            if (!string.IsNullOrEmpty(input))
            {
                int.TryParse(input, out int id);

                var employees = _repo.AsQueryable(ic =>
                    (ic.EmployeeId == id || ic.NameArabic.Contains(input) || ic.NameEnglish.Contains(input)) && ic.IsDeleted == false);

                if (employees.Any())
                {
                    var res = employees.OrderBy(i => i.EmployeeId)
                     .ProjectTo<EmployeeResponse>(_mapper.ConfigurationProvider);

                    return await PaginationHelper.CreateAsync(res, pageIndex);
                }
            }
            return null;
        }

        public async Task<EmployeeResponse> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _repo
                .GetAsync(ic => ic.EmployeeId == employeeId && ic.IsDeleted == false);

            if (employee != null)
            {
                var employeeResp = _mapper.Map<EmployeeResponse>(employee);
                return employeeResp;
            }
            return null;
        }

    }
    
}
