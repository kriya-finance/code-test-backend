
using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Workflow;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductResolver productResolver;

        public ProductApplicationService(IProductResolver productResolver)
        {
            this.productResolver = productResolver;
        }

        public IApplicationResult SubmitApplicationFor(ISellerApplication application)
        {

            var clerk = productResolver.Assign(application);

            // future asynchrony
            clerk.Take(application);
            return clerk.Issue();
        }       
    }
}
