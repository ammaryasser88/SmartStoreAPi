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
    public interface IItemService
    {
        Task<ServiceResult> AddItemAsync(ItemRequest request);
        Task<PaginationObject<ItemResponse>> GetItemsAsync(int pageIndex);
        Task<ServiceResult> DeleteItemAsync(int itemId);
        Task<ServiceResult> UpdateItemAsync(int itemId, ItemRequest request);
        Task<PaginationObject<ItemResponse>> SearchItemAsync(string input, int pageIndex);
        Task<ItemResponse> GetItemByIdAsync(int itemId);
    }
}
