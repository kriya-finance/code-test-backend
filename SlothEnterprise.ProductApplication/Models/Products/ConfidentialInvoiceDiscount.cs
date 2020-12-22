using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Constants;

namespace SlothEnterprise.ProductApplication.Models.Products
{
    /// <inheritdoc cref="IProduct">
    public class ConfidentialInvoiceDiscount : IProduct
    {
        public int Id { get; set; }

        /// <summary>Total ledger networth</summary>
        public decimal TotalLedgerNetworth { get; set; }

        /// <summary>Advance percentage</summary>
        public decimal AdvancePercentage { get; set; }

        /// <summary>Vat rate</summary>
        public decimal VatRate { get; set; } = VatRates.UkVatRate;
    }
}