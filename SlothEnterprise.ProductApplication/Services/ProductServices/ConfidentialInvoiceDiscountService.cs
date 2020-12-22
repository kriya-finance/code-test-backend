using System;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Abstractions.Services;
using SlothEnterprise.ProductApplication.Models.Products;

namespace SlothEnterprise.ProductApplication.Services.ProductServices
{
    /// <inheritdoc cref="IProductService">
    public class ConfidentialInvoiceDiscountService : IProductService
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;

        public Type ProductType => typeof(ConfidentialInvoiceDiscount);

        public ConfidentialInvoiceDiscountService(IConfidentialInvoiceService confidentialInvoiceService)
        {
            _confidentialInvoiceWebService = confidentialInvoiceService;
        }

        public int ProcessProduct(IProduct product, ISellerCompanyData sellerCompanyData)
        {
            var cid = product as ConfidentialInvoiceDiscount;

            var appData = new CompanyDataRequest
            {
                CompanyFounded = sellerCompanyData.Founded,
                CompanyNumber = sellerCompanyData.Number,
                CompanyName = sellerCompanyData.Name,
                DirectorName = sellerCompanyData.DirectorName
            };
            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                appData,
                cid.TotalLedgerNetworth,
                cid.AdvancePercentage,
                cid.VatRate);

            return result.Success ? result.ApplicationId ?? -1 : -1;
        }
    }
}
