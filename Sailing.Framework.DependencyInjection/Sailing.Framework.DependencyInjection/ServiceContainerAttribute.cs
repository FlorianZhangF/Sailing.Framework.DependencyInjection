using System;
using System.Collections.Generic;
using System.Text;

namespace Sailing.Framework.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class ServiceContainerAttribute : Attribute
    {
    }
}
