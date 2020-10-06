using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Workflow.Handlers;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SlothEnterprise.ProductApplication.Workflow
{
    public interface IProductResolver
    {
        IProjectClerk Assign(ISellerApplication application);
    }

    public class BasicProductResolver : IProductResolver
    {
        protected IReadOnlyDictionary<Type, IEnumerable<IServiceHandler>> resolverMapping { get; set; }        

        public BasicProductResolver(IReadOnlyDictionary<Type, IEnumerable<IServiceHandler>> resolverMapping)
        {
            this.resolverMapping = resolverMapping;
        }

        public IProjectClerk Assign(ISellerApplication application)
        {
            var resolver = resolverMapping.FirstOrDefault(x => x.Key.IsAssignableFrom(application.Product.GetType())).Value;
            if (resolver != null)
            {
                return new ProjectClerk(resolver);
            }
            throw new WorkflowException("No product-service mapping found");
        }
    }

    public sealed class V1ProductResolver: BasicProductResolver
    {
        public V1ProductResolver(IReadOnlyDictionary<Type, IEnumerable<IServiceHandler>> resolverMapping)
            : base(new Dictionary<Type, IEnumerable<IServiceHandler>>
                   {
                        { typeof(SelectiveInvoiceDiscount), new[] { new SelectiveInvoiceDiscountServiceHandler() }},
                        { typeof(ConfidentialInvoiceDiscount), new[] { new ConfidentialInvoiceDiscountServiceHandler() }},
                        { typeof(BusinessLoans), new[] { new BusinessLoansServiceHandler() }}
                   }
              )
        { 
        }
        
    }
}
