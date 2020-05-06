using System;
using System.Collections.Generic;
using System.Text;

namespace Sailing.Framework.DependencyInjection.Test.ModelAndInterface
{
    public interface ITestServiceA
    {
        void Show();
    }
    public interface ITestServiceB
    {
        //[Log(10)]
        //[Authentication(30)]
        //[Error(20)]
        [Log]
        [Authentication]
        [Error]
        void Show();

        void ShowMethodTest(ITestServiceE testServiceE);
    }
    public interface ITestServiceC
    {
        void Show();
    }
    public interface ITestServiceD
    {
        void Show();
    }
    public interface ITestServiceE
    {
        void Show();
    }
}
