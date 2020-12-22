using SlothEnterprise.ProductApplication.Abstractions;

namespace SlothEnterprise.ProductApplication.Models.Products
{
    /// <inheritdoc cref="IProduct">
    public class BusinessLoans : IProduct
    {
        public int Id { get; set; }

        /// <summary>
        /// Per annum interest rate
        /// </summary>
        public decimal InterestRatePerAnnum { get; set; }

        /// <summary>
        /// Total available amount to withdraw
        /// </summary>
        public decimal LoanAmount { get; set; }
    }
}