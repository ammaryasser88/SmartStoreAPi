using SmartStore.Application.Repository.Implementation;
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
    public interface IItemCategoryService
    {
        Task<ServiceResult> AddItemCategoryAsync(ItemCategoryRequest request);
        Task<PaginationObject<ItemCategoryResponse>> GetItemsCategoriesAsync(int pageIndex);
        Task<ServiceResult> DeleteItemCategoryAsync(int itemCategoryId);
        Task<ServiceResult> UpdateItemCategoryAsync(int itemCategoryId, ItemCategoryRequest request);
        Task<PaginationObject<ItemCategoryResponse>> SearchItemCategoryAsync(string input, int pageIndex);
        Task<ItemCategoryResponse> GetItemCategoryByIdAsync(int itemCategoryId);
    }
}
