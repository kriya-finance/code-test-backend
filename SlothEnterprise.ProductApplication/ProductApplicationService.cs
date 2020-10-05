using System;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        private readonly IConfidentialInvoiceService _confidentialInvoiceService;
        private readonly IBusinessLoansService _businessLoansService;

        public ProductApplicationService(ISelectInvoiceService selectInvoiceService, IConfidentialInvoiceService confidentialInvoiceService, IBusinessLoansService businessLoansService)
        {
            _selectInvoiceService = selectInvoiceService;
            _confidentialInvoiceService = confidentialInvoiceService;
            _businessLoansService = businessLoansService;
        }

        public IApplicationResult SubmitApplicationFor(ISellerApplication application)
        {

            if (application.Product is SelectiveInvoiceDiscount sid)
            {

                var resultApplicationId = _selectInvoiceService.SubmitApplicationFor(application.CompanyData.WrittenNumber, sid.InvoiceAmount, sid.AdvancePercentage);
                return new ApplicationResult(resultApplicationId);
            }

            if (application.Product is ConfidentialInvoiceDiscount cid)
            {
                var result = _confidentialInvoiceService.SubmitApplicationFor(
                    new CompanyDataRequest
                    {
                        CompanyFounded = application.CompanyData.Founded,
                        CompanyNumber = application.CompanyData.Number,
                        CompanyName = application.CompanyData.Name,
                        DirectorName = application.CompanyData.DirectorName
                    }, cid.TotalLedgerNetworth, cid.AdvancePercentage, cid.VatRate);

                return result;
            }

            if (application.Product is BusinessLoans loans)
            {
                var result = _businessLoansService.SubmitApplicationFor(new CompanyDataRequest
                {
                    CompanyFounded = application.CompanyData.Founded,
                    CompanyNumber = application.CompanyData.Number,
                    CompanyName = application.CompanyData.Name,
                    DirectorName = application.CompanyData.DirectorName
                }, new LoansRequest
                {
                    InterestRatePerAnnum = loans.InterestRatePerAnnum,
                    LoanAmount = loans.LoanAmount
                });
                return result;
            }

            throw new InvalidOperationException();
        }       
    }
}
