using System;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Abstractions.Services;
using SlothEnterprise.ProductApplication.Models.Products;

namespace SlothEnterprise.ProductApplication.Services.ProductServices
{
    /// <inheritdoc cref="IProductService">
    public class BusinessLoansService : IProductService
    {
        private readonly IBusinessLoansService _businessLoansService;

        public Type ProductType => typeof(BusinessLoans);

        public BusinessLoansService(IBusinessLoansService businessLoansService)
        {
            _businessLoansService = businessLoansService;
        }

        public int ProcessProduct(IProduct product, ISellerCompanyData sellerCompanyData)
        {
            var loans = product as BusinessLoans;

            var companyDataRequest = new CompanyDataRequest
            {
                CompanyFounded = sellerCompanyData.Founded,
                CompanyNumber = sellerCompanyData.Number,
                CompanyName = sellerCompanyData.Name,
                DirectorName = sellerCompanyData.DirectorName
            };
            var loansRequest = new LoansRequest
            {
                InterestRatePerAnnum = loans.InterestRatePerAnnum,
                LoanAmount = loans.LoanAmount
            };

            var result = _businessLoansService.SubmitApplicationFor(companyDataRequest, loansRequest);

            return result.Success ? result.ApplicationId ?? -1 : -1;
        }
    }
}
