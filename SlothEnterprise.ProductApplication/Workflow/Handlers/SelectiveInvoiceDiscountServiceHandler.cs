using Autofac;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using System.Threading.Tasks;

namespace SlothEnterprise.ProductApplication.Workflow.Handlers
{
    public class SelectiveInvoiceDiscountServiceHandler: IServiceHandler
    {
        private readonly ISelectInvoiceService service;
        public SelectiveInvoiceDiscountServiceHandler()
        {
            service = StartUp.Scope.Resolve<ISelectInvoiceService>();
        }

        public async Task<ServiceResult> RunAsync(ISellerApplication application)
        {
            var sid = application.Product as SelectiveInvoiceDiscount;
            var result = /*await*/ service.SubmitApplicationFor(
                        application.CompanyData.WrittenNumber,
                        sid.InvoiceAmount,
                        sid.AdvancePercentage);

            return new ServiceResult(result >= 0 ? ProceedStatus.Ready : ProceedStatus.Error
                , new ApplicationResult(result));
        }
    }
}
