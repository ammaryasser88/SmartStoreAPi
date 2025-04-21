using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartStore.Application.Repository.Abstraction;
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
    public class InvoiceService:IInvoiceService
    {
        private readonly IInvoiceRepo _repo;
        private readonly ISafeRepo _safeRepo;
        private readonly IStoreItemQuantityRepo _storeItemQuantityRepo;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly ICustomerRepo _customer;
        private readonly ISupplierRepo _supplier;
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceService(IInvoiceRepo repo, IStoreItemQuantityRepo storeItemQuantityRepo, IMessageService messageService, ISafeRepo safeRepo, IMapper mapper, IUnitOfWork unitOfWork, ISupplierRepo supplier)
        {
            _repo = repo;
            _storeItemQuantityRepo = storeItemQuantityRepo;
            _messageService = messageService;
            _safeRepo = safeRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _supplier = supplier;
        }

        public async Task<InvoiceResponse> AddSaleInvoiceAsync(InvoiceRequest request)
        {
            using var transaction =  _repo.BeginTransactionAsync();

            try
            {
                
                foreach (var detail in request.InvoiceDetails)
                {
                    var storeItem = await _storeItemQuantityRepo
                        .GetAsync(s => s.ItemId == detail.ItemId && s.StoreId == request.StoreId);

                    if (storeItem == null || storeItem.Quantity < detail.Quantity)
                        throw new Exception(_messageService.GetMessage("QuantityNotFound"));

                    storeItem.Quantity -= detail.Quantity;
                }

                
                var safe = await _safeRepo.GetAsync(i=>i.IsDeleted==false);
                await _safeRepo.UpdateWithConcurrencyAsync(safe);

                safe.Balance += request.PaidAmount;

                var safeTransaction = new SafeTransaction
                {
                    SafeId = safe.SafeId,
                    Amount = request.PaidAmount,
                    TransactionTypeId = 1,
                    Date = DateTime.Now,
                    Description = "فاتورة بيع",
                };

                request.SafeTransaction = safeTransaction;
                var invoice = _mapper.Map<Invoice>(request);
                await _repo.AddAsync(invoice);
                await _unitOfWork.SaveChangeAsync();

                 await _repo.CommitTransactionAsync();
                 var customer = await _customer.GetAsync(i => i.CustomerId == invoice.CustomerId);
                
                var invoiceDto = new InvoiceResponse
                {
                    InvoiceId = invoice.InvoiceId,
                    StoreId = invoice.StoreId,
                    Date = invoice.Date,
                    CustomerName = customer.NameArabic,
                    TotalAmount = invoice.TotalAmount,
                    PaidAmount = invoice.PaidAmount,
                    RemainingAmount = invoice.RemainingAmount,
                    Details = invoice.InvoiceDetails.Select(d => new InvoiceDetailResponse
                    {
                        ItemId = d.ItemId,
                        ItemName = d.Item.NameArabic,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        Total = d.Total
                    }).ToList()
                };

                return invoiceDto;
            }
            catch
            {
                await _repo.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<InvoiceResponse> AddPurchaseInvoiceAsync(InvoiceRequest request)
        {
            using var transaction = _repo.BeginTransactionAsync();

            try
            {

                foreach (var detail in request.InvoiceDetails)
                {
                    var storeItem = await _storeItemQuantityRepo
                        .GetAsync(s => s.ItemId == detail.ItemId && s.StoreId == request.StoreId);

                    storeItem.Quantity += detail.Quantity;
                }


                var safe = await _safeRepo.GetAsync(i => i.IsDeleted == false);
                await _safeRepo.UpdateWithConcurrencyAsync(safe);

                safe.Balance -= request.PaidAmount;

                var safeTransaction = new SafeTransaction
                {
                    SafeId = safe.SafeId,
                    Amount = request.PaidAmount,
                    TransactionTypeId = 2,
                    Date = DateTime.Now,
                    Description = "فاتورة شراء",
                };

                request.SafeTransaction = safeTransaction;
                var invoice = _mapper.Map<Invoice>(request);
                await _repo.AddAsync(invoice);
                await _unitOfWork.SaveChangeAsync();

                await _repo.CommitTransactionAsync();
                var supplier = await _supplier.GetAsync(i => i.SupplierId == invoice.SupplierId);

                var invoiceDto = new InvoiceResponse
                {
                    InvoiceId = invoice.InvoiceId,
                    StoreId = invoice.StoreId,
                    Date = invoice.Date,
                    SupplierName = supplier.NameArabic,
                    TotalAmount = invoice.TotalAmount,
                    PaidAmount = invoice.PaidAmount,
                    RemainingAmount = invoice.RemainingAmount,
                    Details = invoice.InvoiceDetails.Select(d => new InvoiceDetailResponse
                    {
                        ItemId = d.ItemId,
                        ItemName = d.Item.NameArabic,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        Total = d.Total
                    }).ToList()
                };

                return invoiceDto;
            }
            catch
            {
                await _repo.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<InvoiceResponse> AddSalesReturnInvoiceAsync(InvoiceRequest request)
        {
            using var transaction = _repo.BeginTransactionAsync();

            try
            {

                foreach (var detail in request.InvoiceDetails)
                {
                    var storeItem = await _storeItemQuantityRepo
                        .GetAsync(s => s.ItemId == detail.ItemId && s.StoreId == request.StoreId);
                    if (storeItem == null || storeItem.Quantity < detail.Quantity)
                        throw new Exception(_messageService.GetMessage("QuantityNotFound"));

                    storeItem.Quantity += detail.Quantity;
                }


                var safe = await _safeRepo.GetAsync(i => i.IsDeleted == false);
                await _safeRepo.UpdateWithConcurrencyAsync(safe);

                safe.Balance -= request.PaidAmount;

                var safeTransaction = new SafeTransaction
                {
                    SafeId = safe.SafeId,
                    Amount = request.PaidAmount,
                    TransactionTypeId = 3,
                    Date = DateTime.Now,
                    Description = "فاتورة مرتجع بيع",
                };

                request.SafeTransaction = safeTransaction;
                var invoice = _mapper.Map<Invoice>(request);
                await _repo.AddAsync(invoice);
                await _unitOfWork.SaveChangeAsync();

                await _repo.CommitTransactionAsync();
                var customer = await _customer.GetAsync(i => i.CustomerId == invoice.CustomerId);

                var invoiceDto = new InvoiceResponse
                {
                    InvoiceId = invoice.InvoiceId,
                    StoreId = invoice.StoreId,
                    Date = invoice.Date,
                    CustomerName = customer.NameArabic,
                    TotalAmount = invoice.TotalAmount,
                    PaidAmount = invoice.PaidAmount,
                    RemainingAmount = invoice.RemainingAmount,
                    Details = invoice.InvoiceDetails.Select(d => new InvoiceDetailResponse
                    {
                        ItemId = d.ItemId,
                        ItemName = d.Item.NameArabic,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        Total = d.Total
                    }).ToList()
                };

                return invoiceDto;
            }
            catch
            {
                await _repo.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<InvoiceResponse> AddPurchaseReturnInvoiceAsync(InvoiceRequest request)
        {
            using var transaction = _repo.BeginTransactionAsync();

            try
            {

                foreach (var detail in request.InvoiceDetails)
                {
                    var storeItem = await _storeItemQuantityRepo
                        .GetAsync(s => s.ItemId == detail.ItemId && s.StoreId == request.StoreId);

                    storeItem.Quantity -= detail.Quantity;
                }


                var safe = await _safeRepo.GetAsync(i => i.IsDeleted == false);
                await _safeRepo.UpdateWithConcurrencyAsync(safe);

                safe.Balance += request.PaidAmount;

                var safeTransaction = new SafeTransaction
                {
                    SafeId = safe.SafeId,
                    Amount = request.PaidAmount,
                    TransactionTypeId = 4,
                    Date = DateTime.Now,
                    Description = "فاتورة مرتجع شراء",
                };

                request.SafeTransaction = safeTransaction;
                var invoice = _mapper.Map<Invoice>(request);
                await _repo.AddAsync(invoice);
                await _unitOfWork.SaveChangeAsync();

                await _repo.CommitTransactionAsync();
                var supplier = await _supplier.GetAsync(i => i.SupplierId == invoice.SupplierId);

                var invoiceDto = new InvoiceResponse
                {
                    InvoiceId = invoice.InvoiceId,
                    StoreId = invoice.StoreId,
                    Date = invoice.Date,
                    SupplierName = supplier.NameArabic,
                    TotalAmount = invoice.TotalAmount,
                    PaidAmount = invoice.PaidAmount,
                    RemainingAmount = invoice.RemainingAmount,
                    Details = invoice.InvoiceDetails.Select(d => new InvoiceDetailResponse
                    {
                        ItemId = d.ItemId,
                        ItemName = d.Item.NameArabic,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        Total = d.Total
                    }).ToList()
                };

                return invoiceDto;
            }
            catch
            {
                await _repo.RollbackTransactionAsync();
                throw;
            }
        }


    }
}
