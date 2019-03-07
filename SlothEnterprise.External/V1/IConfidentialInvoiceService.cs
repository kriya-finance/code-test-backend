namespace SlothEnterprise.External.V1
{
    /// <summary>
    /// Assume this is an external service. you cannot modify this interface
    /// </summary>
    public interface IConfidentialInvoiceService
    {
        IApplicationResult SubmitApplicationFor(CompanyDataRequest applicantData, decimal invoiceLedgerTotalValue, decimal advantagePercentage, decimal vatRate);
    }
}