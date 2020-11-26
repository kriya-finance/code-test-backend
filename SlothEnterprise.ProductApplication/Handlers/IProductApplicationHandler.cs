using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Handlers
{
    public interface IProductApplicationHandler
    {
        bool CanHandle(IProduct product);
        int Handle(ISellerApplication application);
    }
}
