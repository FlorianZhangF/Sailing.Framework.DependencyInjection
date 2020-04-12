using System;
using System.Collections.Generic;
using System.Text;

namespace Sailing.Framework.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class ServiceContainerAliasAttribute : Attribute
    {
        public string AliasValue { get; private set; }

        public ServiceContainerAliasAttribute(string aliasValue)
        {
            this.AliasValue = aliasValue;
        }
    }
}
