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
    public interface IItemTypeService
    {
        Task<ServiceResult> AddItemTypeAsync(ItemTypeRequest request);
        Task<PaginationObject<ItemTypeResponse>> GetItemsTypesAsync(int pageIndex);
        Task<ServiceResult> DeleteItemTypeAsync(int itemTypeId);
        Task<ServiceResult> UpdateItemTypeAsync(int itemTypeId, ItemTypeRequest request);
        Task<PaginationObject<ItemTypeResponse>> SearchItemTypeAsync(string input, int pageIndex);
        Task<ItemTypeResponse> GetItemTypeByIdAsync(int itemTypeId);
    }
}
