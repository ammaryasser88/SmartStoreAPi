using FluentValidation;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Domain.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Validators
{
    public class DamagedItemRequestValidator : AbstractValidator<DamegedItemRequest>
    {
        private readonly IMessageService _messageService;

        public DamagedItemRequestValidator(IMessageService messageService)
        {
            _messageService = messageService;

            RuleFor(x => x.StoreId)
                .GreaterThan(0).WithMessage(_messageService.GetMessage("RequiredStore"));

            RuleFor(x => x.ItemId)
                .GreaterThan(0).WithMessage(_messageService.GetMessage("RequiredProduct"));

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage(_messageService.GetMessage("RequiredQuentity"));

          
        }
    }

}
