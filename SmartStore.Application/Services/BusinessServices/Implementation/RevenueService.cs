using AutoMapper;
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
    public class RevenueService : IRevenueService
    {
        private readonly IRevenueRepo _revenueRepo;
        private readonly ISafeRepo _safeRepo;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RevenueService(IRevenueRepo revenueRepo, ISafeRepo safeRepo, IMessageService messageService, IMapper mapper)
        {
            _revenueRepo = revenueRepo;
            _safeRepo = safeRepo;
            _messageService = messageService;
            _mapper = mapper;
        }

        public async Task<ServiceResult> AddRevenueAsync(RevenueRequest request)
        {
            using var transaction = _revenueRepo.BeginTransactionAsync();
            try
            {
                var safe = await _safeRepo.GetAsync(i => i.IsDeleted == false);
                await _safeRepo.UpdateWithConcurrencyAsync(safe);

                if (safe.Balance < request.Amount)
                    throw new Exception(_messageService.GetMessage("InsufficientBalance"));

                safe.Balance += request.Amount;

                var safeTransaction = new SafeTransaction
                {
                    SafeId = safe.SafeId,
                    Amount = request.Amount,
                    TransactionTypeId = 6, // نوع: وارد
                    Date = request.Date,
                    Description = request.Notes
                };

                request.SafeTransaction = safeTransaction;
                var expense = _mapper.Map<Revenue>(request);
                await _revenueRepo.AddAsync(expense);

                await _unitOfWork.SaveChangeAsync();
                await _revenueRepo.CommitTransactionAsync();

                return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
            }
            catch
            {
                await _revenueRepo.RollbackTransactionAsync();
                return ServiceResult.Failure(_messageService.GetMessage("CanNotExpenseRegist"));
            }
        }
    }
}
