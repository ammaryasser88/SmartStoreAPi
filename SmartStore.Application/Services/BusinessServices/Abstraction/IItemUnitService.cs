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
    public interface IItemUnitService
    {
        Task<ServiceResult> AddItemUnitAsync(ItemUnitRequest request);
        Task<PaginationObject<ItemUnitResponse>> GetItemsUnitsAsync(int pageIndex);
        Task<ServiceResult> DeleteItemUnitAsync(int itemUnitId);
        Task<ServiceResult> UpdateItemUnitAsync(int itemUnitId, ItemUnitRequest request);
        Task<PaginationObject<ItemUnitResponse>> SearchItemUnitAsync(string input, int pageIndex);
        Task<ItemUnitResponse> GetItemUnitByIdAsync(int itemUnitId);
    }
}
