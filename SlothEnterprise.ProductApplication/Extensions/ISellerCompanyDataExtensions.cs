using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using System;

namespace SlothEnterprise.ProductApplication.Extensions
{
    public static class ISellerCompanyDataExtensions
    {
        public static CompanyDataRequest ToCompanyDataRequest(this ISellerCompanyData sellerCompanyData)
        {
            if (sellerCompanyData == null)
            {
                var parameterName = nameof(sellerCompanyData);
                throw new ArgumentNullException(parameterName, $"Can not create CompanyDataRequest - {parameterName} is null");
            }

            return new CompanyDataRequest
            {
                CompanyFounded = sellerCompanyData.Founded,
                CompanyNumber = sellerCompanyData.Number,
                CompanyName = sellerCompanyData.Name,
                DirectorName = sellerCompanyData.DirectorName
            };
        }
    }
}
