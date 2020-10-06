using Autofac;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using System.Threading.Tasks;

namespace SlothEnterprise.ProductApplication.Workflow.Handlers
{
    public class ConfidentialInvoiceDiscountServiceHandler : IServiceHandler
    {
        private readonly IConfidentialInvoiceService service;
        public ConfidentialInvoiceDiscountServiceHandler()
        {
            service = StartUp.Scope.Resolve<IConfidentialInvoiceService>();
        }

        public async Task<ServiceResult> RunAsync(ISellerApplication application)
        {
            var cid = application.Product as ConfidentialInvoiceDiscount;
            var result = /*await*/ service.SubmitApplicationFor(
                   application.CompanyData.ToRequest(),
                   cid.TotalLedgerNetworth,
                   cid.AdvancePercentage,
                   cid.VatRate);

            return new ServiceResult(result.Success ? ProceedStatus.Ready : ProceedStatus.Error, result);
        }
    }
}
