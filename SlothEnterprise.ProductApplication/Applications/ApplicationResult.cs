using SlothEnterprise.External;
using System.Collections.Generic;

namespace SlothEnterprise.ProductApplication.Applications
{
    public class ApplicationResult : IApplicationResult
    {
        private readonly int? _applicationId;
        private readonly bool _success;
        private readonly IList<string> _errors = new string[0];

        public ApplicationResult(int? applicationId)
        {
            _applicationId = applicationId;
            _success = applicationId.HasValue && applicationId != -1;
        }

        public int? ApplicationId { get => _applicationId; set { } }
        public bool Success { get => _success; set { } }
        public IList<string> Errors { get => _errors; set { } }
    }
}