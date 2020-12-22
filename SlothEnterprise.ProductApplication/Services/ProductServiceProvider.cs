using System;
using System.Collections.Generic;
using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Abstractions.Services;

namespace SlothEnterprise.ProductApplication.Services
{
    /// <inheritdoc cref="IProductServiceProvider">
    public class ProductServiceProvider : IProductServiceProvider
    {
        private Dictionary<Type, IProductService> _servicesDict = new Dictionary<Type, IProductService>();

        public void RegisterService(IProductService service)
        {
            if (!typeof(IProduct).IsAssignableFrom(service.ProductType))
                throw new ArgumentException($"Product type of the service doesn't implement {nameof(IProduct)}");

            _servicesDict.Add(service.ProductType, service);
        }

        public IProductService ResolveProductService(Type productType)
        {
            if (_servicesDict.TryGetValue(productType, out var productService))
                return productService;

            throw new InvalidOperationException($"Product service for {productType.Name} is not registered");
        }
    }
}
