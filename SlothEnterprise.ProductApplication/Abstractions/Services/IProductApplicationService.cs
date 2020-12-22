namespace SlothEnterprise.ProductApplication.Abstractions.Services
{
    /// <summary>
    /// Interface for product application service.
    /// </summary>
    public interface IProductApplicationService
    {
        /// <summary>
        /// Submits application request.
        /// </summary>
        /// <param name="application">Seller application data</param>
        /// <returns>Application submission number</returns>
        int SubmitApplicationFor(ISellerApplication application);
    }
}