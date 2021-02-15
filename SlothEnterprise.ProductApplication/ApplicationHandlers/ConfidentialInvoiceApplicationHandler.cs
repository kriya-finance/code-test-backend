using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Extensions;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.ApplicationHandlers
{
    public class ConfidentialInvoiceApplicationHandler : IApplicationHandler
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceService;

        public ConfidentialInvoiceApplicationHandler(IConfidentialInvoiceService confidentialInvoiceService)
        {
            _confidentialInvoiceService = confidentialInvoiceService;
        }

        public bool CanHandle(IProduct product)
        {
            return product is ConfidentialInvoiceDiscount;
        }

        public int Handle(ISellerApplication application)
        {
            ConfidentialInvoiceDiscount product = (ConfidentialInvoiceDiscount)application.Product;

            CompanyDataRequest companyDataRequest = application.CompanyData.ToCompanyDataRequest();

            IApplicationResult result = _confidentialInvoiceService.SubmitApplicationFor(
                companyDataRequest,
                product.TotalLedgerNetworth,
                product.AdvancePercentage,
                product.VatRate);

            return result.ToApplicationId();
        }
    }
}
