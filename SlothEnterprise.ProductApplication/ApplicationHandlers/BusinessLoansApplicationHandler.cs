using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Extensions;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.ApplicationHandlers
{
    public class BusinessLoansApplicationHandler : IApplicationHandler
    {
        private readonly IBusinessLoansService _businessLoansService;

        public BusinessLoansApplicationHandler(IBusinessLoansService businessLoansService)
        {
            _businessLoansService = businessLoansService;
        }

        public bool CanHandle(IProduct product)
        {
            return product is BusinessLoans;
        }

        public int Handle(ISellerApplication application)
        {
            BusinessLoans product = (BusinessLoans)application.Product;

            CompanyDataRequest companyDataRequest = application.CompanyData.ToCompanyDataRequest();

            IApplicationResult result = _businessLoansService.SubmitApplicationFor(
                companyDataRequest,
                new LoansRequest
                {
                    InterestRatePerAnnum = product.InterestRatePerAnnum,
                    LoanAmount = product.LoanAmount
                });

            return result.ToApplicationId();
        }
    }
}
