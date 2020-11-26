using System;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Handlers;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly IProductApplicationMediatorService _productApplicationMediatorService;

        public ProductApplicationService(IProductApplicationMediatorService productApplicationMediatorService)
        {
            _productApplicationMediatorService = productApplicationMediatorService ?? throw new ArgumentNullException(nameof(productApplicationMediatorService));
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            return _productApplicationMediatorService.SubmitApplicationFor(application);
        }
    }
}
