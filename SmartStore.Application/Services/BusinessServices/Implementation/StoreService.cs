using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartStore.Application.Repository.Abstraction;
using SmartStore.Application.Responses;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Application.UnitOfWork.Abstraction;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Dtos.Response;
using SmartStore.Domain.Entities;
using SmartStore.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Services.BusinessServices.Implementation
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStoreRepo _repo;
        private readonly IMessageService _messageService;
        public StoreService(IUnitOfWork unitOfWork, IMapper mapper, IStoreRepo repo, IMessageService messageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = repo;
            _messageService = messageService;
        }
        public async Task<ServiceResult> AddStoreAsync(StoreRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));

            var isExists = await _repo
                .GetAsync(c => c.Name == request.Name && c.IsDeleted == false);

            if (isExists != null)
                return ServiceResult.Failure(_messageService.GetMessage("StoreExists"));

            var entity = _mapper.Map<Store>(request);

            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
        }

        public async Task<PaginationObject<StoreResponse>> GetStoresAsync(int pageIndex)
        {
            var query = (_repo.AsQueryable(i => i.IsDeleted == false))
            .OrderBy(i => i.Name)
            .ProjectTo<StoreResponse>(_mapper.ConfigurationProvider);
            return await PaginationHelper.CreateAsync(query, pageIndex);
        }

        public async Task<ServiceResult> UpdateStoreAsync(int storeId, StoreRequest request)
        {

            if (storeId == 0)
            {
                return ServiceResult.Failure(_messageService.GetMessage("InvalidId"));
            }

            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));
            }

            var store = await _repo
                .GetAsync(ic => ic.StoreId == storeId && ic.IsDeleted == false);

            if (store == null)
            {
                return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
            }

            _mapper.Map(request, store);
            _repo.Update(store);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("UpdateSuccessfully"));
        }

        public async Task<ServiceResult> DeleteStoreAsync(int storeId)
        {
            if (storeId != 0)
            {
                var store = await _repo
                 .GetAsync(ic => ic.StoreId == storeId && ic.IsDeleted == false);

                if (store != null)
                {
                    store.IsDeleted = true;
                    _repo.Update(store);
                    await _unitOfWork.SaveChangeAsync();

                    return ServiceResult.Success(_messageService.GetMessage("DeleteSuccessfully"));
                }
            }
            return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
        }

        public async Task<PaginationObject<StoreResponse>> SearchStoreAsync(string input, int pageIndex)
        {
            if (!string.IsNullOrEmpty(input))
            {
                int.TryParse(input, out int id);

                var stores = _repo.AsQueryable(ic =>
                    (ic.StoreId == id || ic.Name.Contains(input))  && ic.IsDeleted == false);

                if (stores.Any())
                {
                    var res = stores.OrderBy(i => i.StoreId)
                     .ProjectTo<StoreResponse>(_mapper.ConfigurationProvider);

                    return await PaginationHelper.CreateAsync(res, pageIndex);
                }
            }
            return null;
        }

        public async Task<StoreResponse> GetStoreByIdAsync(int storeId)
        {
            var store = await _repo
                .GetAsync(ic => ic.StoreId == storeId && ic.IsDeleted == false);

            if (store != null)
            {
                var storeResp = _mapper.Map<StoreResponse>(store);
                return storeResp;
            }
            return null;
        }

    }
    
}
