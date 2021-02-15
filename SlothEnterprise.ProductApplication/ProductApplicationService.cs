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
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application), "Empty application when submitting for");
            }

            if (application.Product == null)
            {
                throw new ArgumentNullException(nameof(application.Product), "Empty product when submitting application");
            }

            IApplicationHandler appropriateApplicationHandler = _applicationHandlers.FirstOrDefault(x => x.CanHandle(application.Product));
            //TODO: Based on business requirements we can also use SingleOrDefault,
            //if we want to throw exception when more than 1 handler can submit application - it may mean an incorrect request

            if (appropriateApplicationHandler == null)
            {
                throw new InvalidOperationException("None of V1 microservices can submit application for this product!");
            }

            return appropriateApplicationHandler.Handle(application);
        }
    }
}
