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
    public class InvoiceRequestValidator : AbstractValidator<InvoiceRequest>
    {
        private readonly IMessageService _messageService;

        public InvoiceRequestValidator(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public InvoiceRequestValidator()
        {
            RuleFor(x => x.StoreId)
                .GreaterThan(0).WithMessage(_messageService.GetMessage("RequiredStore"));

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage(_messageService.GetMessage("RequiredCustomer"));

            RuleFor(x => x.InvoiceDetails)
                .NotEmpty().WithMessage(_messageService.GetMessage("RequiredProducts"));

            RuleForEach(x => x.InvoiceDetails).ChildRules(details =>
            {
                details.RuleFor(d => d.ItemId).GreaterThan(0).WithMessage(_messageService.GetMessage("RequiredProduct"));
                details.RuleFor(d => d.Quantity).GreaterThan(0).WithMessage(_messageService.GetMessage("RequiredQuentity"));
            });
        }
    }
}
