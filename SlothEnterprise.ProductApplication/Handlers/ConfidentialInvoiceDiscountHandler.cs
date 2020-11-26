using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using System;

namespace SlothEnterprise.ProductApplication.Handlers
{
    public class ConfidentialInvoiceDiscountHandler : IProductApplicationHandler
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;

        public ConfidentialInvoiceDiscountHandler(IConfidentialInvoiceService confidentialInvoiceWebService)
        {
            _confidentialInvoiceWebService = confidentialInvoiceWebService ?? throw new ArgumentNullException(nameof(confidentialInvoiceWebService));
        }

        public bool CanHandle(IProduct product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            return product is ConfidentialInvoiceDiscount;
        }

        public int Handle(ISellerApplication application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            if (application.Product is ConfidentialInvoiceDiscount product)
            {
                var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                    new CompanyDataRequest
                    {
                        CompanyFounded = application.CompanyData.Founded,
                        CompanyNumber = application.CompanyData.Number,
                        CompanyName = application.CompanyData.Name,
                        DirectorName = application.CompanyData.DirectorName
                    }, product.TotalLedgerNetworth, product.AdvancePercentage, product.VatRate);
                return (result.Success) ? result.ApplicationId ?? -1 : -1;
            }

            throw new InvalidOperationException();
        }
    }
}
