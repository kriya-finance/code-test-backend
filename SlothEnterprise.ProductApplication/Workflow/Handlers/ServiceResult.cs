using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication.Workflow.Handlers
{
    public interface IServiceResult
    {
        public ProceedStatus Status { get; }
        public IApplicationResult Result { get; }
    }

    public class ServiceResult
    {
        public ServiceResult(ProceedStatus status, IApplicationResult result)
        {
            Status = status;
            ApplicationResult = result;
        }

        public ProceedStatus Status { get; }
        public IApplicationResult ApplicationResult { get; }
    }
}
