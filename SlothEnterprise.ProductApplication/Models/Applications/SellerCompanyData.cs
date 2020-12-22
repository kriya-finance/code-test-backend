using System;
using SlothEnterprise.ProductApplication.Abstractions;

namespace SlothEnterprise.ProductApplication.Models.Applications
{
    public class SellerCompanyData : ISellerCompanyData
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string DirectorName { get; set; }
        public DateTime Founded { get; set; }
    }
}