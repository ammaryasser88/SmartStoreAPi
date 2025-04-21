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
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IItemRepo _repo;
        private readonly IMessageService _messageService;
        public ItemService(IUnitOfWork unitOfWork, IMapper mapper, IItemRepo repo, IMessageService messageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = repo;
            _messageService = messageService;
        }
        public async Task<ServiceResult> AddItemAsync(ItemRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));

            var isExists = await _repo
                .GetAsync(c => c.NameArabic == request.NameArabic && c.IsDeleted == false);

            if (isExists != null)
                return ServiceResult.Failure(_messageService.GetMessage("ItemExists"));

            var entity = _mapper.Map<Item>(request);

            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
        }

        public async Task<PaginationObject<ItemResponse>> GetItemsAsync(int pageIndex)
        {
            var query = (_repo.AsQueryable(i => i.IsDeleted == false))
            .OrderBy(i => i.NameArabic)
            .ProjectTo<ItemResponse>(_mapper.ConfigurationProvider);
            return await PaginationHelper.CreateAsync(query, pageIndex);
        }

        public async Task<ServiceResult> UpdateItemAsync(int itemId, ItemRequest request)
        {

            if (itemId == 0)
            {
                return ServiceResult.Failure(_messageService.GetMessage("InvalidId"));
            }

            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
            {
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));
            }

            var itemType = await _repo
                .GetAsync(ic => ic.ItemId == itemId && ic.IsDeleted == false);

            if (itemType == null)
            {
                return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
            }

            _mapper.Map(request, itemType);
            _repo.Update(itemType);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("UpdateSuccessfully"));
        }

        public async Task<ServiceResult> DeleteItemAsync(int itemId)
        {
            if (itemId != 0)
            {
                var itemCategory = await _repo
                 .GetAsync(ic => ic.ItemId == itemId && ic.IsDeleted == false);

                if (itemCategory != null)
                {
                    itemCategory.IsDeleted = true;
                    _repo.Update(itemCategory);
                    await _unitOfWork.SaveChangeAsync();

                    return ServiceResult.Success(_messageService.GetMessage("DeleteSuccessfully"));
                }
            }
            return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
        }

        public async Task<PaginationObject<ItemResponse>> SearchItemAsync(string input, int pageIndex)
        {
            if (!string.IsNullOrEmpty(input))
            {
                int.TryParse(input, out int id);

                var itemsCategories = _repo.AsQueryable(ic =>
                    (ic.ItemId == id || ic.NameArabic.Contains(input) || ic.NameEnglish.Contains(input)) && ic.IsDeleted == false);

                if (itemsCategories.Any())
                {
                    var res = itemsCategories.OrderBy(i => i.ItemId)
                     .ProjectTo<ItemResponse>(_mapper.ConfigurationProvider);

                    return await PaginationHelper.CreateAsync(res, pageIndex);
                }
            }
            return null;
        }

        public async Task<ItemResponse> GetItemByIdAsync(int itemTypeId)
        {
            var item = await _repo
                .GetAsync(ic => ic.ItemId == itemTypeId && ic.IsDeleted == false);

            if (item != null)
            {
                var itemResp = _mapper.Map<ItemResponse>(item);
                return itemResp;
            }
            return null;
        }

    }
    
}
