using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Constants;

namespace SlothEnterprise.ProductApplication.Models.Products
{
    public class SelectiveInvoiceDiscount : IProduct
    {
        public int Id { get; set; }
        /// <summary>
        /// Proposed networth of the Invoice
        /// </summary>
        public decimal InvoiceAmount { get; set; }
        /// <summary>
        /// Percentage of the networth agreed and advanced to seller
        /// </summary>
        public decimal AdvancePercentage { get; set; } = AdvancePercentages.SelectiveInvoiceDiscount;
    }
}