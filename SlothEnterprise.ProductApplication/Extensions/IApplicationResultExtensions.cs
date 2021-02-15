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

            //FYI: I've modified a little bit original logic - if returned result is null then I return -1 as well
            return -1;
        }
    }
}
