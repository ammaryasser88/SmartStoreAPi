using AutoMapper;
using Azure.Core;
using SmartStore.Application.Repository.Abstraction;
using SmartStore.Application.Repository.Implementation;
using SmartStore.Application.Responses;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Application.UnitOfWork.Abstraction;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Dtos.Response;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Services.BusinessServices.Implementation
{
    public class DamegedItemService :IDamegedItemService
    {
        private readonly IDamagedItemRepo _repo;
        private readonly IStoreItemQuantityRepo _storeItemQuantityRepo;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DamegedItemService(IDamagedItemRepo repo, IStoreItemQuantityRepo storeItemQuantityRepo, IMessageService messageService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _storeItemQuantityRepo = storeItemQuantityRepo;
            _messageService = messageService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> AddDamagedItemAsync(DamegedItemRequest request)
        {
            using var transaction =  _repo.BeginTransactionAsync();
            try
            {
                var storeItem = await _storeItemQuantityRepo
                    .GetAsync(s => s.ItemId == request.ItemId && s.StoreId == request.StoreId);

                if (storeItem == null || storeItem.Quantity < request.Quantity)
                    throw new Exception(_messageService.GetMessage("QuantityNotFound"));

                storeItem.Quantity -= request.Quantity;
                await _storeItemQuantityRepo.UpdateWithConcurrencyAsync(storeItem);

                var damagedItem = _mapper.Map<DamagedItem>(request);
                damagedItem.Date = DateTime.Now;

                await _repo.AddAsync(damagedItem);
                await _unitOfWork.SaveChangeAsync();
                await _repo.CommitTransactionAsync();

                return ServiceResult.Success(_messageService.GetMessage("RegistDamegedItem"));
            }
            catch
            {
                await _repo.RollbackTransactionAsync();
                return ServiceResult.Failure(_messageService.GetMessage("RegisterFailure"));
            }
        }

        public async Task<ServiceResult> DeleteDamagedItemAsync(int damagedItemId)
        {
            var entity = await _repo.GetAsync(i=>i.DamagedItemId==damagedItemId&&i.IsDeleted==false);
            if (entity == null) return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));

            entity.IsDeleted.Equals(true);
            _repo.Update(entity);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
        }

        public async Task<ServiceResult> UpdateDamagedItemAsync(int damagedItemId, DamegedItemRequest request)
        {
            var entity = await _repo.GetByIdAsync(damagedItemId);
            if (entity == null) return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));

            _mapper.Map(request, entity);
             _repo.Update(entity);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResult.Success(_messageService.GetMessage("UpdateSuccessfully"));
        }
    }
}
