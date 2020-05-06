using System;
using System.Collections.Generic;
using System.Text;

namespace Sailing.Framework.DependencyInjection
{
    public class ServiceModel
    {
        public string Name { get; set; }

        public Type InstanceType { get; set; }

        public object Instance { get; set; }

        public LifeCycle LifeCycleType { get; set; }
    }
}
