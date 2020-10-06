namespace SlothEnterprise.ProductApplication.Products
{
    public class ConfidentialInvoiceDiscount : IProduct
    {
        public int Id { get; }
        public decimal TotalLedgerNetworth { get; }
        public decimal AdvancePercentage { get; }
        public decimal VatRate { get; } = VatRates.UkVatRate;
    }
}