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
    public class RevenueRequestValidator : AbstractValidator<RevenueRequest>
    {
        private readonly IMessageService _messageService;

        public RevenueRequestValidator(IMessageService messageService)
        {
            _messageService = messageService;

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage(_messageService.GetMessage("RequiredAmount"));

            RuleFor(x => x.Notes)
                .NotEmpty().WithMessage(_messageService.GetMessage("RequiredDescription"));

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(_messageService.GetMessage("RequiredDate"));
        }
    }

}
