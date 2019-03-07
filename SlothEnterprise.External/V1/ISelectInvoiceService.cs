namespace SlothEnterprise.External.V1
{
    /// <summary>
    /// Assume this is an external service. you cannot modify this interface
    /// </summary>
    public interface ISelectInvoiceService
    {
        int SubmitApplicationFor(string companyNumber, decimal invoiceAmount, decimal advancePercentage);
    }
}