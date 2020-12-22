using SlothEnterprise.ProductApplication.Abstractions;

namespace SlothEnterprise.ProductApplication.Models.Applications
{
    /// <inheritdoc cref="ISellerApplication">
    public class SellerApplication : ISellerApplication
    {
        public IProduct Product { get; set; }
        public ISellerCompanyData CompanyData { get; set; }
    }
}
