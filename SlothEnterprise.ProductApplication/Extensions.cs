using Autofac;
using NSubstitute;

namespace SlothEnterprise.ProductApplication
{
    public static class Extensions
    {
        public static void RegisterSubstitute<T>(this ContainerBuilder builder) where T: class
        {
             builder.RegisterInstance(Substitute.For<T>()).As<T>();
        }
    }
}
