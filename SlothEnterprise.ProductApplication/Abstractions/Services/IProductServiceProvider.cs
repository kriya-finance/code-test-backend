using System;

namespace SlothEnterprise.ProductApplication.Abstractions.Services
{
    /// <summary>
    /// Product services container interface
    /// </summary>
    public interface IProductServiceProvider
    {
        /// <summary>
        /// Registers new service provider for specific product type.
        /// </summary>
        /// <param name="service">Product service</param>
        void RegisterService(IProductService service);

        /// <summary>
        /// Resolves processing product service by target product type.
        /// </summary>
        /// <param name="productType">Product type</param>
        /// <returns>Product service</returns>
        IProductService ResolveProductService(Type productType);
    }
}
