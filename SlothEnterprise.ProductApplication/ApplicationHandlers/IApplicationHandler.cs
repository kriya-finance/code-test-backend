using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.ApplicationHandlers
{
    public interface IApplicationHandler
    {
        bool CanHandle(ISellerApplication application);

        int Handle(ISellerApplication application);
    }
}
