using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication.Extensions
{
    public static class IApplicationResultExtensions
    {
        public static int ToApplicationId(this IApplicationResult applicationResult)
        {
            if (applicationResult?.Success ?? false)
            {
                return applicationResult.ApplicationId ?? -1;
            }

            return -1;
        }
    }
}
