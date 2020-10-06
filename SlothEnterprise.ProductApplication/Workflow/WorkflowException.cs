using System;

namespace SlothEnterprise.ProductApplication.Workflow
{
    class WorkflowException : Exception
    {
        public WorkflowException(string message) : base(message) { }
    }
}
