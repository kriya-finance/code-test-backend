using System;

namespace SlothEnterprise.ProductApplication.Applications
{
    public interface ISellerCompanyData
    {
        string Name { get; set; }
        int Number { get; set; }
        string DirectorName { get; set; }
        DateTime Founded { get; set; }
    }


    public class SellerCompanyData : ISellerCompanyData
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string DirectorName { get; set; }
        public DateTime Founded { get; set; }
    }
}