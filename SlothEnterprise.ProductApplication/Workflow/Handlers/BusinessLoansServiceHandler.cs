using Autofac;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using System.Threading.Tasks;

namespace SlothEnterprise.ProductApplication.Workflow.Handlers
{
    public class BusinessLoansServiceHandler : IServiceHandler
    {
        private readonly IBusinessLoansService service;
        public BusinessLoansServiceHandler()
        {
            service = StartUp.Scope.Resolve<IBusinessLoansService>();
        }

        public async Task<ServiceResult> RunAsync(ISellerApplication application)
        {
            var loans = application.Product as BusinessLoans;
            var result = /*await*/ service.SubmitApplicationFor(
                    application.CompanyData.ToRequest(),
                    loans.ToRequest());

            return new ServiceResult(result.Success ? ProceedStatus.Ready : ProceedStatus.Error, result);
        }
    }
}
