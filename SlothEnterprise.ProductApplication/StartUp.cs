using Autofac;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Workflow;


namespace SlothEnterprise.ProductApplication
{
    public static class StartUp
    {
        public static ILifetimeScope Scope { get; private set; }
        public static void Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<V1ProductResolver>().As<IProductResolver>();

            builder.RegisterType<ProductApplicationService>()                
                .As<IProductApplicationService>();

            builder.RegisterSubstitute<ISelectInvoiceService>();
            builder.RegisterSubstitute<IConfidentialInvoiceService>();
            builder.RegisterSubstitute<IBusinessLoansService>();

            
            Scope = builder.Build().BeginLifetimeScope();
        }
        public static void Finish()
        {
            Scope.Dispose();
        }
    }
}
