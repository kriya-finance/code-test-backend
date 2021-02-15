using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.ApplicationHandlers
{
    public interface IApplicationHandler
    {
        bool CanHandle(IProduct product);

        int Handle(ISellerApplication application);
    }
}
