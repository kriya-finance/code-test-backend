using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using SlothEnterprise.ProductApplication.Workflow.Handlers;

namespace SlothEnterprise.ProductApplication.Workflow
{
    public interface IProjectClerk
    {
        public void Take(ISellerApplication application);
        public IApplicationResult Issue();
        public ProceedStatus Status { get; }
        public void Reset();
    }

    public class ProjectClerk: IProjectClerk
    {
        private readonly Dictionary<IServiceHandler, bool> serviceHandlers;
        private ApplicationResult applicationResult;
        private readonly List<Task> tasks = new List<Task>();

        public ProjectClerk(IEnumerable<IServiceHandler> serviceHandlers)
        {
            this.serviceHandlers = serviceHandlers.ToDictionary(x => x, x=> false);
            applicationResult = new ApplicationResult(-1);
            Status = ProceedStatus.Free;
        }

        public ProceedStatus Status { get; private set; }

        public IApplicationResult Issue()
        {
            if (!tasks.Any())
            {
                return default;
            }

            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            return applicationResult;            
        }

        private void Collect(IServiceHandler serviceHandler, ServiceResult serviceResult)
        {
            serviceHandlers[serviceHandler] = true;
            if (serviceResult.Status != ProceedStatus.Ready)
            {
                applicationResult.Errors.Add(
                    $"{serviceHandler.GetType().Name} interrupted job on with status {serviceResult.Status}");
            }
            Status = serviceHandlers.All(x => x.Value) 
                ? applicationResult.Errors.Any() 
                    ? ProceedStatus.Error 
                    : ProceedStatus.Ready 
                : ProceedStatus.Proceeding;
            if (Status == ProceedStatus.Error)
            {
                throw new WorkflowException(string.Join(Environment.NewLine, 
                    new[] { "Service errors:" }.Concat(applicationResult.Errors)));
            }
            if (Status == ProceedStatus.Ready)
            {
                applicationResult = new ApplicationResult(serviceResult.ApplicationResult.ApplicationId);
            }
        }

        public void Reset()
        {
            Status = ProceedStatus.Free;
        }

        public void Take(ISellerApplication application)
        {
            Status = ProceedStatus.Taken;

            foreach (var serviceHandler in serviceHandlers.Keys)
            {
                tasks.Add(Task.Factory.StartNew(async () =>
                {
                    Collect(serviceHandler, await serviceHandler.RunAsync(application));
                }));
            }

            
        }
    }
}