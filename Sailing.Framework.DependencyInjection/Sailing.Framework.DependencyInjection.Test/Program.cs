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

        #region 测试用类和接口
        public interface ITestServiceA
        {
            public void Show();
        }
        public interface ITestServiceB
        {
            public void Show();

            public void ShowMethodTest(ITestServiceE testServiceE);
        }
        public interface ITestServiceC
        {
            public void Show();
        }
        public interface ITestServiceD
        {
            public void Show();
        }
        public interface ITestServiceE
        {
            public void Show();
        }

        public class TestServiceA : ITestServiceA
        {
            public void Show()
            {
                Console.WriteLine($"Class Type is {this.GetType()}");
            }
        }
        public class TestServiceA1 : ITestServiceA
        {
            public void Show()
            {
                Console.WriteLine($"Class Type is {this.GetType()}");
            }
        }
        public class TestServiceA2 : ITestServiceA
        {
            public void Show()
            {
                Console.WriteLine($"Class Type is {this.GetType()}");
            }
        }
        public class TestServiceB : ITestServiceB
        {
            public TestServiceB(ITestServiceD testServiceD)
            {

            }

            [ServiceContainer]
            public ITestServiceC testServiceC { get; set; }

            public void Show()
            {
                Console.WriteLine($"Class Type is {this.GetType()}");
            }

            [ServiceContainer]
            public void ShowMethodTest(ITestServiceE testServiceE)
            {
                testServiceE.Show();
            }

        }
        public class TestServiceC : ITestServiceC
        {
            public void Show()
            {
                Console.WriteLine($"Class Type is {this.GetType()}");
            }
        }
        public class TestServiceD : ITestServiceD
        {
            public TestServiceD(ITestServiceE testServiceE)
            {

            }

            public void Show()
            {
                Console.WriteLine($"Class Type is {this.GetType()}");
            }
        }

        public class TestServiceE : ITestServiceE
        {
            public void Show()
            {
                Console.WriteLine($"Class Type is {this.GetType()}");
            }
        }
        #endregion
    }
}
