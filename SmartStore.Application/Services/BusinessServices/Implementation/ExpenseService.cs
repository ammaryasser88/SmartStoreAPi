using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepo _expenseRepo;
        private readonly ISafeRepo _safeRepo;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseService(IExpenseRepo expenseRepo, ISafeRepo safeRepo, IMessageService messageService, IMapper mapper)
        {
            _expenseRepo = expenseRepo;
            _safeRepo = safeRepo;
            _messageService = messageService;
            _mapper = mapper;
        }

        public async Task<ServiceResult> AddExpenseAsync(ExpenseRequest request)
        {
            using var transaction =  _expenseRepo.BeginTransactionAsync();
            try
            {
                var safe = await _safeRepo.GetAsync(i=>i.IsDeleted==false);
                await _safeRepo.UpdateWithConcurrencyAsync(safe);

                if (safe.Balance < request.Amount)
                    throw new Exception(_messageService.GetMessage("InsufficientBalance"));

                safe.Balance -= request.Amount;

                var safeTransaction = new SafeTransaction
                {
                    SafeId = safe.SafeId,
                    Amount = request.Amount,
                    TransactionTypeId = 5, // نوع: مصروف
                    Date = request.Date,
                    Description = request.Notes
                };

                request.SafeTransaction = safeTransaction;
                var expense = _mapper.Map<Expense>(request);
                await  _expenseRepo.AddAsync(expense);

                await _unitOfWork.SaveChangeAsync();
                await _expenseRepo.CommitTransactionAsync();

                return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
            }
            catch
            {
                await  _expenseRepo.RollbackTransactionAsync();
                return ServiceResult.Failure(_messageService.GetMessage("CanNotExpenseRegist"));
            }
        }
    }
    
}
