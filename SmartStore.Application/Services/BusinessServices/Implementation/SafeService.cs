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
    public class SafeService : ISafeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISafeRepo _repo;
        private readonly IMessageService _messageService;
        public SafeService(IUnitOfWork unitOfWork, IMapper mapper, ISafeRepo repo, IMessageService messageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = repo;
            _messageService = messageService;
        }
        public async Task<ServiceResult> AddSafeAsync(SafeRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));

            var isExists = await _repo
                .GetAsync(c => c.NameArabic == request.NameArabic && c.IsDeleted == false);

            if (isExists != null)
                return ServiceResult.Failure(_messageService.GetMessage("ItemExists"));

            var entity = _mapper.Map<Safe>(request);

            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("RegisterSuccessfully"));
        }

        public async Task<PaginationObject<SafeResponse>> GetSafesAsync(int pageIndex)
        {
            var query = (_repo.AsQueryable(i => i.IsDeleted == false))
            .OrderBy(i => i.NameArabic)
            .ProjectTo<SafeResponse>(_mapper.ConfigurationProvider);
            return await PaginationHelper.CreateAsync(query, pageIndex);
        }

        public async Task<ServiceResult> UpdateSafeAsync(int safeId, SafeRequest request)
        {

            if (safeId == 0)
            {
                return ServiceResult.Failure(_messageService.GetMessage("InvalidId"));
            }

            if (request == null || string.IsNullOrWhiteSpace(request.NameArabic))
            {
                return ServiceResult.Failure(_messageService.GetMessage("EmptyValue"));
            }

            var safe = await _repo
                .GetAsync(ic => ic.SafeId == safeId && ic.IsDeleted == false);

            if (safe == null)
            {
                return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
            }

            _mapper.Map(request, safe);
            _repo.Update(safe);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(_messageService.GetMessage("UpdateSuccessfully"));
        }

        public async Task<ServiceResult> DeleteSafeAsync(int safeId)
        {
            if (safeId != 0)
            {
                var safe = await _repo
                 .GetAsync(ic => ic.SafeId == safeId && ic.IsDeleted == false);

                if (safe != null)
                {
                    safe.IsDeleted.Equals(true);
                    _repo.Update(safe);
                    await _unitOfWork.SaveChangeAsync();

                    return ServiceResult.Success(_messageService.GetMessage("DeleteSuccessfully"));
                }
            }
            return ServiceResult.Failure(_messageService.GetMessage("ValueNotFound"));
        }

        public async Task<PaginationObject<SafeResponse>> SearchSafeAsync(string input, int pageIndex)
        {
            if (!string.IsNullOrEmpty(input))
            {
                int.TryParse(input, out int id);

                var safes = _repo.AsQueryable(ic =>
                    (ic.SafeId == id || ic.NameArabic.Contains(input) || ic.NameEnglish.Contains(input)) && ic.IsDeleted == false);

                if (safes.Any())
                {
                    var res = safes.OrderBy(i => i.SafeId)
                     .ProjectTo<SafeResponse>(_mapper.ConfigurationProvider);

                    return await PaginationHelper.CreateAsync(res, pageIndex);
                }
            }
            return null;
        }

        public async Task<SafeResponse> GetSafeByIdAsync(int safeId)
        {
            var safe = await _repo
                .GetAsync(ic => ic.SafeId == safeId && ic.IsDeleted == false);

            if (safe != null)
            {
                var safeResponse = _mapper.Map<SafeResponse>(safe);
                return safeResponse;
            }
            return null;
        }

    }
}
