using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using System;

namespace SlothEnterprise.ProductApplication.Handlers
{
    public class BusinessLoansHandler : IProductApplicationHandler
    {
        private readonly IBusinessLoansService _businessLoansService;

        public BusinessLoansHandler(IBusinessLoansService businessLoansService)
        {
            _businessLoansService = businessLoansService ?? throw new ArgumentNullException(nameof(businessLoansService));
        }

        public bool CanHandle(IProduct product)
        {
            return product is BusinessLoans;
        }

        public int Handle(ISellerApplication application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            if (application.Product is BusinessLoans product)
            {
                var result = _businessLoansService.SubmitApplicationFor(new CompanyDataRequest
                {
                    CompanyFounded = application.CompanyData.Founded,
                    CompanyNumber = application.CompanyData.Number,
                    CompanyName = application.CompanyData.Name,
                    DirectorName = application.CompanyData.DirectorName
                }, new LoansRequest
                {
                    InterestRatePerAnnum = product.InterestRatePerAnnum,
                    LoanAmount = product.LoanAmount
                });
                return (result.Success) ? result.ApplicationId ?? -1 : -1;
            }

            throw new InvalidOperationException();
        }
    }
}
