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
    public class SupplierRequestValidator : AbstractValidator<SupplierRequest>
    {
        private readonly IMessageService _messageService;

        public SupplierRequestValidator(IMessageService messageService)
        {
            _messageService = messageService;

            RuleFor(x => x.NameArabic)
                .NotEmpty().WithMessage(_messageService.GetMessage("RequiredNameArabic"));
            RuleFor(x => x.NameEnglish)
                .NotEmpty().WithMessage(_messageService.GetMessage("RequiredNameEnglish"));

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage(_messageService.GetMessage("RequiredPhone"));
        }
    }

}
