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
    public class ItemUnitService : IItemUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IItemUnitRepo _repo;
        private readonly IMessageService _messageService;
        public ItemUnitService(IUnitOfWork unitOfWork, IMapper mapper, IItemUnitRepo repo, IMessageService messageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = repo;
            _messageService = messageService;
        }
        public async Task<ServiceResult> AddItemUnitAsync(ItemUnitRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));

            var isExists = await _repo
                .GetAsync(c => c.NameArabic == request.NameArabic && c.IsDeleted == false);

            if (isExists != null)
                return ServiceResult.Failure(_messageService.GetMessage("ItemUnitExists"));

            var entity = _mapper.Map<ItemUnit>(request);

            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
        }

        public async Task<PaginationObject<ItemUnitResponse>> GetItemsUnitsAsync(int pageIndex)
        {
            var query = (_repo.AsQueryable(i => i.IsDeleted == false))
            .OrderBy(i => i.NameArabic)
            .ProjectTo<ItemUnitResponse>(_mapper.ConfigurationProvider);
            return await PaginationHelper.CreateAsync(query, pageIndex);
        }

        public async Task<ServiceResult> UpdateItemUnitAsync(int itemUnitId, ItemUnitRequest request)
        {

            if (itemUnitId == 0)
            {
                return ServiceResult.Failure(_messageService.GetMessage("InvalidId"));
            }

            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
            {
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));
            }

            var itemUnit = await _repo
                .GetAsync(ic => ic.ItemUnitId == itemUnitId && ic.IsDeleted == false);

            if (itemUnit == null)
            {
                return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
            }

            _mapper.Map(request, itemUnit);
            _repo.Update(itemUnit);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("UpdateSuccessfully"));
        }

        public async Task<ServiceResult> DeleteItemUnitAsync(int itemUnitId)
        {
            if (itemUnitId != 0)
            {
                var itemUnit = await _repo
                 .GetAsync(ic => ic.ItemUnitId == itemUnitId && ic.IsDeleted == false);

                if (itemUnit != null)
                {
                    itemUnit.IsDeleted = true;
                    _repo.Update(itemUnit);
                    await _unitOfWork.SaveChangeAsync();

                    return ServiceResult.Success(_messageService.GetMessage("DeleteSuccessfully"));
                }
            }
            return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
        }

        public async Task<PaginationObject<ItemUnitResponse>> SearchItemUnitAsync(string input, int pageIndex)
        {
            if (!string.IsNullOrEmpty(input))
            {
                int.TryParse(input, out int id);

                var itemsUnits = _repo.AsQueryable(ic =>
                    (ic.ItemUnitId == id || ic.NameArabic.Contains(input) || ic.NameEnglish.Contains(input)) && ic.IsDeleted == false);

                if (itemsUnits.Any())
                {
                    var res = itemsUnits.OrderBy(i => i.ItemUnitId)
                     .ProjectTo<ItemUnitResponse>(_mapper.ConfigurationProvider);

                    return await PaginationHelper.CreateAsync(res, pageIndex);
                }
            }
            return null;
        }

        public async Task<ItemUnitResponse> GetItemUnitByIdAsync(int itemUnitId)
        {
            var itemUnit = await _repo
                .GetAsync(ic => ic.ItemUnitId == itemUnitId && ic.IsDeleted == false);

            if (itemUnit != null)
            {
                var itemUnitResp = _mapper.Map<ItemUnitResponse>(itemUnit);
                return itemUnitResp;
            }
            return null;
        }

    }
}
