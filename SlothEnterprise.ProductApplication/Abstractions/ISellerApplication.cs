namespace SlothEnterprise.ProductApplication.Abstractions
{

    public interface ISellerApplication
    {
        IProduct Product { get; set; }
        ISellerCompanyData CompanyData { get; set; }
    }
}