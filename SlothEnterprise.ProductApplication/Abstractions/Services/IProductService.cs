using System;

namespace SlothEnterprise.ProductApplication.Abstractions.Services
{
    /// <summary>
    /// Product service interface.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Supported product type.
        /// </summary>
        Type ProductType { get; }

        /// <summary>
        /// Processes product request.
        /// </summary>
        /// <param name="product">Target product</param>
        /// <param name="sellerCompanyData">Company data</param>
        /// <returns>App id</returns>
        int ProcessProduct(IProduct product, ISellerCompanyData sellerCompanyData);
    }
}
