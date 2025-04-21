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
    public class ItemCategoryRequestValidator : AbstractValidator<ItemCategoryRequest>
    {
        private readonly IMessageService _messageService;

        public ItemCategoryRequestValidator(IMessageService messageService)
        {
            _messageService = messageService;

            RuleFor(x => x.NameArabic)
                .NotEmpty().WithMessage(_messageService.GetMessage("RequiredNameArabic"));

            RuleFor(x => x.NameArabic)
                .NotEmpty().WithMessage(_messageService.GetMessage("RequiredNameEnglish"));
        }
    }

}
