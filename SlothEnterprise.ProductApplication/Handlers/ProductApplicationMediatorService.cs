using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Exceptions;
using SlothEnterprise.ProductApplication.Products;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SlothEnterprise.ProductApplication.Handlers
{
    public interface IProductApplicationMediatorService
    {
        int SubmitApplicationFor(ISellerApplication application);
    }

    public class ProductApplicationMediatorService : IProductApplicationMediatorService
    {
        private readonly IEnumerable<IProductApplicationHandler> _handlers;

        public ProductApplicationMediatorService(IEnumerable<IProductApplicationHandler> handlers)
        {
            _handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            if (application.Product == null)
                throw new InvalidApplicationException("Application product can't be null");

            var handler = _handlers.SingleOrDefault(x => x.CanHandle(application.Product));

            if (handler == default(IProductApplicationHandler))
                throw new InvalidOperationException();

            return handler.Handle(application);
        }
    }


}
