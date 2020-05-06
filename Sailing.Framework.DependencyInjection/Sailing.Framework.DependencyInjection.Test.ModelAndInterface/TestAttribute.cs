using Castle.DynamicProxy;
using Sailing.Framework.Interceptor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sailing.Framework.DependencyInjection.Test.ModelAndInterface
{
    public class LogAttribute : MethodInterceptorAttribute
    {
        //public LogAttribute(int order) : base(order)
        //{
        //}

        public override Action Use(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"{nameof(LogAttribute)} Do Before Next");
                action.Invoke();
                Console.WriteLine($"{nameof(LogAttribute)} Do After Next");
            };
        }
    }

    public class AuthenticationAttribute : MethodInterceptorAttribute
    {
        //public AuthenticationAttribute(int order) : base(order)
        //{
        //}
        public override Action Use(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"{nameof(AuthenticationAttribute)} Do Before Next");
                action.Invoke();
                Console.WriteLine($"{nameof(AuthenticationAttribute)} Do After Next");
            };
        }
    }

    public class ErrorAttribute : MethodInterceptorAttribute
    {
        //public ErrorAttribute(int order) : base(order)
        //{
        //}
        public override Action Use(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"{nameof(ErrorAttribute)} Do Before Next");
                action.Invoke();
                Console.WriteLine($"{nameof(ErrorAttribute)} Do After Next");
            };
        }
    }
}
