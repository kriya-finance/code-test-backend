using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using System;

namespace SlothEnterprise.ProductApplication.Handlers
{
    public class PersonalLoansHandler : IProductApplicationHandler
    {
        private readonly IPersonalLoansService _personalLoansService;

        public PersonalLoansHandler(IPersonalLoansService personalLoansService)
        {
            _personalLoansService = personalLoansService ?? throw new ArgumentNullException(nameof(personalLoansService));
        }

        public bool CanHandle(IProduct product)
        {
            return product is PersonalLoans;
        }

        public int Handle(ISellerApplication application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            if (application.Product is PersonalLoans product)
            {
                return _personalLoansService.SubmitApplicationFor(new LoansRequest
                {
                    InterestRatePerAnnum = product.InterestRatePerAnnum,
                    LoanAmount = product.LoanAmount
                });
            }

            throw new InvalidOperationException();
        }
    }

    //Mock external personal loan service interface for demo only.
    public interface IPersonalLoansService
    {
        int SubmitApplicationFor(LoansRequest loansRequest);
    }
}
