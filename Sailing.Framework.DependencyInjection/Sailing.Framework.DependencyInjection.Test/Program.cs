using Castle.DynamicProxy;
using Sailing.Framework.DependencyInjection.Test.ModelAndInterface;
using System;

namespace Sailing.Framework.DependencyInjection.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IServiceContainer serviceContainer = new ServiceContainer();

            
            {
                serviceContainer.Register<ITestServiceA, TestServiceA>();
                serviceContainer.Register<ITestServiceA, TestServiceA1>("A1");
                serviceContainer.Register<ITestServiceA, TestServiceA2>("A2");

                ITestServiceA testServiceA = serviceContainer.Resolve<ITestServiceA>();
                ITestServiceA testServiceA1 = serviceContainer.Resolve<ITestServiceA>("A1");
                ITestServiceA testServiceA2 = serviceContainer.Resolve<ITestServiceA>("A2");

                testServiceA.Show();
                testServiceA1.Show();
                testServiceA2.Show();
            }

            {
                serviceContainer.Register<ITestServiceB, TestServiceB>();
                serviceContainer.Register<ITestServiceC, TestServiceC>();
                serviceContainer.Register<ITestServiceD, TestServiceD>();
                serviceContainer.Register<ITestServiceE, TestServiceE>();

                ITestServiceB testServiceB = serviceContainer.Resolve<ITestServiceB>();

                testServiceB.Show();
            }

            Console.ReadLine();
        }
    }
}
