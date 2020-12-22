using System;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Abstractions.Services;
using SlothEnterprise.ProductApplication.Models.Products;

namespace SlothEnterprise.ProductApplication.Services.ProductServices
{
    /// <inheritdoc cref="IProductService">
    public class SelectiveInvoiceDiscountService : IProductService
    {
        private readonly ISelectInvoiceService _selectInvoiceService;

        public Type ProductType => typeof(SelectiveInvoiceDiscount);

        public SelectiveInvoiceDiscountService(ISelectInvoiceService selectInvoiceService)
        {
            _selectInvoiceService = selectInvoiceService;
        }

        public int ProcessProduct(IProduct product, ISellerCompanyData sellerCompanyData)
        {
            var sid = product as SelectiveInvoiceDiscount;

            return _selectInvoiceService.SubmitApplicationFor(
                sellerCompanyData.Number.ToString(),
                sid.InvoiceAmount,
                sid.AdvancePercentage);
        }
    }
}
