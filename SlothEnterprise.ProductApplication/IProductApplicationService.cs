using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication
{
    public interface IProductApplicationService
    {
        IApplicationResult SubmitApplicationFor(ISellerApplication application);
    }
}
