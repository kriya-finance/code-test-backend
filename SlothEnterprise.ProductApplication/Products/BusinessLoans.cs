using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication.Products
{
    public class BusinessLoans : IProduct
    {
        public int Id { get; }
        /// <summary>
        /// Per annum interest rate
        /// </summary>
        public decimal InterestRatePerAnnum { get; }

        /// <summary>
        /// Total available amount to withdraw
        /// </summary>
        public decimal LoanAmount { get; }

        public LoansRequest ToRequest()
        {
            return new LoansRequest
            {
                InterestRatePerAnnum = InterestRatePerAnnum,
                LoanAmount = LoanAmount
            };
        }
    }
}