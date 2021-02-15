using System;
using System.Linq;
using SlothEnterprise.ProductApplication.ApplicationHandlers;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IApplicationHandler[] _applicationHandlers;

        public ProductApplicationService(IApplicationHandler[] applicationHandlers)
        {
            _applicationHandlers = applicationHandlers;
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            IApplicationHandler appropriateApplicationHandler = _applicationHandlers.FirstOrDefault(x => x.CanHandle(application));

            if (appropriateApplicationHandler == null)
            {
                throw new InvalidOperationException("None of V1 microservices can submit application for this product!");
            }

            return appropriateApplicationHandler.Handle(application);
        }
    }
}
