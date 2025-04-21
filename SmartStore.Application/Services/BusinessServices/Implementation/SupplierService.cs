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
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISupplierRepo _repo;
        private readonly IMessageService _messageService;
        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper, ISupplierRepo repo, IMessageService messageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = repo;
            _messageService = messageService;
        }
        public async Task<ServiceResult> AddSupplierAsync(SupplierRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));

            var isExists = await _repo
                .GetAsync(c => c.NameArabic == request.NameArabic && c.IsDeleted == false);

            if (isExists != null)
                return ServiceResult.Failure(_messageService.GetMessage("ItemExists"));

            var entity = _mapper.Map<Supplier>(request);

            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
        }

        public async Task<PaginationObject<SupplierResponse>> GetSuppliersAsync(int pageIndex)
        {
            var query = (_repo.AsQueryable(i => i.IsDeleted == false))
            .OrderBy(i => i.NameArabic)
            .ProjectTo<SupplierResponse>(_mapper.ConfigurationProvider);
            return await PaginationHelper.CreateAsync(query, pageIndex);
        }

        public async Task<ServiceResult> UpdateSupplierAsync(int supplierId, SupplierRequest request)
        {

            if (supplierId == 0)
            {
                return ServiceResult.Failure(_messageService.GetMessage("InvalidId"));
            }

            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
            {
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));
            }

            var supplier = await _repo
                .GetAsync(ic => ic.SupplierId == supplierId && ic.IsDeleted == false);

            if (supplier == null)
            {
                return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
            }

            _mapper.Map(request, supplier);
            _repo.Update(supplier);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("UpdateSuccessfully"));
        }

        public async Task<ServiceResult> DeleteSupplierAsync(int supplierId)
        {
            if (supplierId != 0)
            {
                var supplier = await _repo
                 .GetAsync(ic => ic.SupplierId == supplierId && ic.IsDeleted == false);

                if (supplier != null)
                {
                    supplier.IsDeleted = true;
                    _repo.Update(supplier);
                    await _unitOfWork.SaveChangeAsync();

                    return ServiceResult.Success(_messageService.GetMessage("DeleteSuccessfully"));
                }
            }
            return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
        }

        public async Task<PaginationObject<SupplierResponse>> SearchSupplierAsync(string input, int pageIndex)
        {
            if (!string.IsNullOrEmpty(input))
            {
                int.TryParse(input, out int id);

                var suppliers = _repo.AsQueryable(ic =>
                    (ic.SupplierId == id || ic.NameArabic.Contains(input) || ic.NameEnglish.Contains(input)) && ic.IsDeleted == false);

                if (suppliers.Any())
                {
                    var res = suppliers.OrderBy(i => i.SupplierId)
                     .ProjectTo<SupplierResponse>(_mapper.ConfigurationProvider);

                    return await PaginationHelper.CreateAsync(res, pageIndex);
                }
            }
            return null;
        }

        public async Task<SupplierResponse> GetSupplierByIdAsync(int supplierId)
        {
            var item = await _repo
                .GetAsync(ic => ic.SupplierId == supplierId && ic.IsDeleted == false);

            if (item != null)
            {
                var itemResp = _mapper.Map<SupplierResponse>(item);
                return itemResp;
            }
            return null;
        }

    }
}
