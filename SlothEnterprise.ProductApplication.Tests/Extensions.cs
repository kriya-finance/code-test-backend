using Autofac;
using NSubstitute;

namespace SlothEnterprise.ProductApplication.Tests
{
    public static class Extensions
    {
        public static void RegisterSubstitute<T>(this ContainerBuilder builder) where T: class
        {
             builder.RegisterInstance(Substitute.For<T>()).As<T>();
        }
    }
}
