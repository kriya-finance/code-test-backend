
namespace SlothEnterprise.ProductApplication.Workflow
{
    public enum ProceedStatus
    {
        /// <summary>
        /// is not working at all
        /// </summary>
        Free,
        /// <summary>
        /// just after taking application, no services yet includes in work
        /// </summary>
        Taken,
        /// <summary>
        /// sent application to 1 or more services, waiting them to finish
        /// </summary>
        Proceeding,
        /// <summary>
        /// returned unproceeded from one of services due to application error
        /// </summary>
        Returned,
        /// <summary>
        /// unproceeded due to some service error
        /// </summary>
        Error,
        /// <summary>
        /// finished and ready to issue specific application
        /// </summary>
        Ready
    }
}
