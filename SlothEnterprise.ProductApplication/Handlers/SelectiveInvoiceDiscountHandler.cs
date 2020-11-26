using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using System;

namespace SlothEnterprise.ProductApplication.Handlers
{
    public class SelectiveInvoiceDiscountHandler : IProductApplicationHandler
    {
        private readonly ISelectInvoiceService _selectInvoiceService;

        public SelectiveInvoiceDiscountHandler(ISelectInvoiceService selectInvoiceService)
        {
            _selectInvoiceService = selectInvoiceService ?? throw new ArgumentNullException(nameof(selectInvoiceService));
        }

        public bool CanHandle(IProduct product)
        {
            return product is SelectiveInvoiceDiscount;
        }

        public int Handle(ISellerApplication application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            if (application.Product is SelectiveInvoiceDiscount product)
                return _selectInvoiceService.SubmitApplicationFor(application.CompanyData.Number.ToString(), product.InvoiceAmount, product.AdvancePercentage);

            throw new InvalidOperationException();
        }
    }
}
