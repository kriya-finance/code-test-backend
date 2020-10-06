using SlothEnterprise.External;
using System;

namespace SlothEnterprise.ProductApplication.Applications
{
    public interface ISellerCompanyData
    {
        string Name { get; }
        int Number { get; }
        string DirectorName { get; }
        DateTime Founded { get; }
        string WrittenNumber => Number.ToString();
        CompanyDataRequest ToRequest();
    }


    public class SellerCompanyData : ISellerCompanyData
    {
        public string Name { get; }
        public int Number { get; }
        public string DirectorName { get; }
        public DateTime Founded { get; }

        public CompanyDataRequest ToRequest()
        {
            return new CompanyDataRequest
            {
                CompanyFounded = Founded,
                CompanyNumber = Number,
                CompanyName = Name,
                DirectorName = DirectorName
            };
        }
    }
}