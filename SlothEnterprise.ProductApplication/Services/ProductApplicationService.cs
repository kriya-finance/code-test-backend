using System;
using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Abstractions.Services;

namespace SlothEnterprise.ProductApplication.Services
{
    /// <inheritdoc cref="IProductApplicationService">
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductServiceProvider _productServiceProvider;

        public ProductApplicationService(IProductServiceProvider productServiceProvider)
        {
            _productServiceProvider = productServiceProvider;
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            if (application == null)
                throw new ArgumentNullException();
            if (application.Product == null)
                throw new ArgumentNullException($"{nameof(ISellerApplication.Product)} can't be empty");
            if (application.CompanyData == null)
                throw new ArgumentNullException($"{nameof(ISellerApplication.CompanyData)} can't be empty");

            var productService = _productServiceProvider.ResolveProductService(application.Product.GetType());

            return productService.ProcessProduct(application.Product, application.CompanyData);
        }
    }
}
