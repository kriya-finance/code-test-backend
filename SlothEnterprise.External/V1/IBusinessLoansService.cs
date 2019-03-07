namespace SlothEnterprise.External.V1
{

    /// <summary>
    /// Assume this is an external service. you cannot modify this interface
    /// </summary>
    public interface IBusinessLoansService
    {
        IApplicationResult SubmitApplicationFor(CompanyDataRequest applicantData, LoansRequest businessLoans);
    }
}