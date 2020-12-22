using System;

namespace SlothEnterprise.ProductApplication.Abstractions
{
    /// <summary>
    /// Seller company data interface.
    /// </summary>
    public interface ISellerCompanyData
    {
        /// <summary>Name</summary>
        string Name { get; set; }

        /// <summary>Number</summary>
        int Number { get; set; }

        /// <summary>Director name</summary>
        string DirectorName { get; set; }

        /// <summary>Company foundation date</summary>
        DateTime Founded { get; set; }
    }
}
