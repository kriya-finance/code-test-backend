using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Workflow.Handlers;
using System.Threading.Tasks;

namespace SlothEnterprise.ProductApplication.Workflow
{
    public interface IServiceHandler
    {
        Task<ServiceResult> RunAsync(ISellerApplication application);
    }
}