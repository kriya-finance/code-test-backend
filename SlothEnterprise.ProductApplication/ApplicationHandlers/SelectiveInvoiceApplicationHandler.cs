using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.ApplicationHandlers
{
    public class SelectiveInvoiceApplicationHandler : IApplicationHandler
    {
        private readonly ISelectInvoiceService _selectInvoiceService;

        public SelectiveInvoiceApplicationHandler(ISelectInvoiceService selectInvoiceService)
        {
            _selectInvoiceService = selectInvoiceService;
        }

        public bool CanHandle(ISellerApplication application)
        {
            return application?.Product is SelectiveInvoiceDiscount;
        }

        public int Handle(ISellerApplication application)
        {
            string companyNumber = application.CompanyData?.Number.ToString();
            SelectiveInvoiceDiscount product = (SelectiveInvoiceDiscount)application.Product;

            //TODO: What about case when companyNumber is null, empty or 0?

            return _selectInvoiceService.SubmitApplicationFor(companyNumber, product.InvoiceAmount, product.AdvancePercentage);
        }
    }
}
