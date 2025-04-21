using SmartStore.Application.GenaricRepository.Abstraction;
using SmartStore.Application.GenaricRepsitory.Implementation;
using SmartStore.Application.MapperConfigurations;
using SmartStore.Application.Repository.Abstraction;
using SmartStore.Application.Repository.Implementation;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.ApplicationServices.Implementation;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Implementation;
using SmartStore.Application.UnitOfWork.Abstraction;
using SmartStore.Application.UnitOfWork.Implementation;
using FluentValidation;
using FluentValidation.AspNetCore;
using SmartStore.Application.Validators;

namespace SmartStore.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IItemCategoryRepo, ItemCategoryRepo>();
            services.AddScoped<IItemTypeRepo, ItemTypeRepo>();
            services.AddScoped<IItemUnitRepo, ItemUnitRepo>();
            services.AddScoped<IItemRepo, ItemRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IExpenseRepo, ExpenseRepo>();
            services.AddScoped<IRevenueRepo, RevenueRepo>();
            services.AddScoped<IDamagedItemRepo, DamagedItemRepo>();
            services.AddScoped<IInvoiceRepo, InvoiceRepo>();
            services.AddScoped<ISafeRepo, SafeRepo>();
            services.AddScoped<ISupplierRepo, SupplierRepo>();
            services.AddScoped<IStockAdjustmentRepo, StockAdjustmentRepo>();
            services.AddScoped<IStoreRepo, StoreRepo>();
            services.AddScoped<IStoreItemQuantityRepo, StoreItemQuantityRepo>();

          
            services.AddScoped<IItemCategoryService, ItemCategoryService>();
            services.AddScoped<IItemTypeService, ItemTypeService>();
            services.AddScoped<IItemUnitService, ItemUnitService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IRevenueService, RevenueService>();
            services.AddScoped<IDamegedItemService, DamegedItemService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<ISafeService, SafeService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IStockAdjustmentService, StockAdjustmentService>();
            services.AddScoped<IStoreService, StoreService>();

            services.AddAutoMapper(typeof(ItemCategoryProfile));
            services.AddAutoMapper(typeof(ItemTypeProfile));
            services.AddAutoMapper(typeof(ItemUnitProfile));
            services.AddAutoMapper(typeof(ItemProfile));
            services.AddAutoMapper(typeof(EmployeeProfile));
            services.AddAutoMapper(typeof(CustomerProfile));
            services.AddAutoMapper(typeof(ExpenseProfile));
            services.AddAutoMapper(typeof(RevenueProfile));
            services.AddAutoMapper(typeof(DamagedItemProfile));
            services.AddAutoMapper(typeof(InvoiceProfile));
            services.AddAutoMapper(typeof(SafeProfile));
            services.AddAutoMapper(typeof(SupplierProfile));
            services.AddAutoMapper(typeof(StockAdjustmentProfile));
            services.AddAutoMapper(typeof(StoreProfile));

            services.AddScoped<IMessageService, MessageService>();

           services.AddFluentValidationAutoValidation();
           services.AddValidatorsFromAssemblyContaining<InvoiceRequestValidator>();
           services.AddValidatorsFromAssemblyContaining<CustomerRequestValidator>();
           services.AddValidatorsFromAssemblyContaining<ExpenseRequestValidator>();
           services.AddValidatorsFromAssemblyContaining<ItemCategoryRequestValidator>();
           services.AddValidatorsFromAssemblyContaining<ItemRequestValidator>();
           services.AddValidatorsFromAssemblyContaining<RevenueRequestValidator>();
           services.AddValidatorsFromAssemblyContaining<StockAdjustmentRequestValidator>();
           services.AddValidatorsFromAssemblyContaining<SupplierRequestValidator>();
           services.AddValidatorsFromAssemblyContaining<DamagedItemRequestValidator>();
        }
    }
}
