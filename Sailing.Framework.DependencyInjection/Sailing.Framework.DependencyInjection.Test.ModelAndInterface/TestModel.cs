using System;
using System.Collections.Generic;
using System.Text;

namespace Sailing.Framework.DependencyInjection.Test.ModelAndInterface
{
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
}
