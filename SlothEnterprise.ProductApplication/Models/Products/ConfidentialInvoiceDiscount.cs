using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Constants;

namespace SlothEnterprise.ProductApplication.Models.Products
{
    public class ConfidentialInvoiceDiscount : IProduct
    {
        public int Id { get; set; }
        public decimal TotalLedgerNetworth { get; set; }
        public decimal AdvancePercentage { get; set; }
        public decimal VatRate { get; set; } = VatRates.UkVatRate;
    }
}