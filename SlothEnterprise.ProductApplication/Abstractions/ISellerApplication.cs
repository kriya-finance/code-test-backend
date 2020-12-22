namespace SlothEnterprise.ProductApplication.Abstractions
{
    /// <summary>
    /// Interface for seller application.
    /// </summary>
    public interface ISellerApplication
    {
        /// <summary>
        /// Seller product.
        /// </summary>
        IProduct Product { get; set; }

        /// <summary>
        /// Company data.
        /// </summary>
        ISellerCompanyData CompanyData { get; set; }
    }
}