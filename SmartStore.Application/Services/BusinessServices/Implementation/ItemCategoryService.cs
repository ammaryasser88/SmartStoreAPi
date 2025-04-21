using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartStore.Application.Repository.Abstraction;
using SmartStore.Application.Repository.Implementation;
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
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IItemCategoryRepo _repo;
        private readonly IMessageService _messageService;
        public ItemCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IItemCategoryRepo repo, IMessageService messageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = repo;
            _messageService = messageService;
        }

        public async Task<ServiceResult> AddItemCategoryAsync(ItemCategoryRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));

            var isExists = await _repo
                .GetAsync(c => c.NameArabic == request.NameArabic && c.IsDeleted == false);

            if (isExists != null)
                return ServiceResult.Failure(_messageService.GetMessage("ItemCategoryExists"));

            var entity = _mapper.Map<ItemCategory>(request);

            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
        }

        public async Task<PaginationObject<ItemCategoryResponse>> GetItemsCategoriesAsync(int pageIndex)
        {
            var query = (_repo.AsQueryable(i => i.IsDeleted == false))
            .OrderBy(i => i.NameArabic)
            .ProjectTo<ItemCategoryResponse>(_mapper.ConfigurationProvider);
            return await PaginationHelper.CreateAsync(query, pageIndex);
        }

        public async Task<ServiceResult> UpdateItemCategoryAsync(int itemCategoryId, ItemCategoryRequest request)
        {

            if (itemCategoryId == 0)
            {
                return ServiceResult.Failure(_messageService.GetMessage("InvalidId"));
            }

            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
            {
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));
            }

            var itemCategory = await _repo
                .GetAsync(ic => ic.ItemCategoryId == itemCategoryId && ic.IsDeleted == false);

            if (itemCategory == null)
            {
                return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
            }

            _mapper.Map(request, itemCategory);
            _repo.Update(itemCategory);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("UpdateSuccessfully"));
        }

        public async Task<ServiceResult> DeleteItemCategoryAsync(int itemCategoryId)
        {
            if (itemCategoryId != 0)
            {
                var itemCategory = await _repo
                 .GetAsync(ic => ic.ItemCategoryId == itemCategoryId && ic.IsDeleted == false);

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

        public async Task<PaginationObject<ItemCategoryResponse>> SearchItemCategoryAsync(string input, int pageIndex)
        {
            if (!string.IsNullOrEmpty(input))
            {
                int.TryParse(input, out int id);

                var itemsCategories = _repo.AsQueryable(ic =>
                    (ic.ItemCategoryId == id || ic.NameArabic.Contains(input) || ic.NameEnglish.Contains(input)) && ic.IsDeleted == false);

                if (itemsCategories.Any())
                {
                    var res = itemsCategories.OrderBy(i => i.ItemCategoryId)
                     .ProjectTo<ItemCategoryResponse>(_mapper.ConfigurationProvider);

                    return await PaginationHelper.CreateAsync(res, pageIndex);
                }
            }
            return null;
        }

        public async Task<ItemCategoryResponse> GetItemCategoryByIdAsync(int itemCategoryId)
        {
            var itemCategory = await _repo
                .GetAsync(ic => ic.ItemCategoryId == itemCategoryId && ic.IsDeleted == false);

            if (itemCategory != null)
            {
                var itemCategoryResp = _mapper.Map<ItemCategoryResponse>(itemCategory);
                return itemCategoryResp;
            }
            return null;
        }

    }
}
