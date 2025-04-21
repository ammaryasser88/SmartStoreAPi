using SmartStore.Application.Responses;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Services.BusinessServices.Abstraction
{
    public interface IStockAdjustmentService
    {
        Task<ServiceResult> AddStockAdjustmentAsync(StockAdjustmentRequest request);
       // Task<List<StockAdjustmentResponse>> GetAllAsync();
        Task<ServiceResult> UpdateStockAdjustmentAsync(int stockAdjustmentId, StockAdjustmentRequest request);
        Task<ServiceResult> DeleteStockAdjustmentAsync(int stockAdjustmentId);
    }
}
