using AutoMapper;
using Azure.Core;
using SmartStore.Application.Repository.Abstraction;
using SmartStore.Application.Responses;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Application.UnitOfWork.Abstraction;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Services.BusinessServices.Implementation
{
    public class StockAdjustmentService : IStockAdjustmentService
    {
        private readonly IStockAdjustmentRepo _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStoreItemQuantityRepo _storeItemQuantityRepo;
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public StockAdjustmentService(IStockAdjustmentRepo repo, IStoreItemQuantityRepo storeItemQuantityRepo, IMessageService messageService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _storeItemQuantityRepo = storeItemQuantityRepo;
            _messageService = messageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult> AddStockAdjustmentAsync(StockAdjustmentRequest request)
        {
            using var transaction = _repo.BeginTransactionAsync();
            try
            {
                var storeItem = await _storeItemQuantityRepo.GetAsync(s => s.ItemId == request.ItemId && s.StoreId == request.StoreId);
                if (storeItem == null) return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));

                
                var difference = request.QuantityAfter - request.QuantityBefore;

                storeItem.Quantity = request.QuantityAfter;
                await _storeItemQuantityRepo.UpdateWithConcurrencyAsync(storeItem);

                var adjustment = new StockAdjustment
                {
                    ItemId = request.ItemId,
                    StoreId = request.StoreId,
                    QuantityBefore = request.QuantityBefore,
                    QuantityAfter = request.QuantityAfter,
                    Difference = difference,
                    Reason = request.Reason,
                    Date = DateTime.Now
                };

                await _repo.AddAsync(adjustment);
                await _unitOfWork.SaveChangeAsync();
                await _repo.CommitTransactionAsync();

                return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
            }
            catch
            {
                await _repo.RollbackTransactionAsync();
                return ServiceResult.Failure(_messageService.GetMessage("RegisterFailure"));
            }
        }

        public async Task<ServiceResult> DeleteStockAdjustmentAsync(int stockAdjustmentId)
        {
            var entity = await _repo.GetByIdAsync(stockAdjustmentId);
            if (entity == null) return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));

           entity.IsDeleted.Equals(true);
            _repo.Update(entity);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResult.Success(_messageService.GetMessage("UpdateSuccessfully"));
        }

        public async Task<ServiceResult> UpdateStockAdjustmentAsync(int stockAdjustmentId, StockAdjustmentRequest request)
        {
            var entity = await _repo.GetByIdAsync(stockAdjustmentId);
            if (entity == null) return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));

            _mapper.Map(request, entity);
            _repo.Update(entity);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResult.Success(_messageService.GetMessage("UpdateSuccessfully"));
        }
    }
}
