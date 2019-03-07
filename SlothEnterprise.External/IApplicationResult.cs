using System.Collections.Generic;

namespace SlothEnterprise.External
{
    public interface IApplicationResult
    {
        int? ApplicationId { get; set; }
        bool Success { get; set; }
        IList<string> Errors { get; set; }
    }
}
