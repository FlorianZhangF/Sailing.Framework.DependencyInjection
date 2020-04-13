using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Sailing.Framework.DependencyInjection
{
    public class ServiceContainer : IServiceContainer
    {
        private IDictionary<Type, IList<ServiceModel>> container = new Dictionary<Type, IList<ServiceModel>>();

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="name">支持一接口多实现的名称</param>
        public void Register<TInterface, TClass>(string name = "", LifeCycle lifeCycleType = LifeCycle.Transient) where TClass : TInterface
        {
            Type interfaceType = typeof(TInterface);
            Type classType = typeof(TClass);

            if (container.ContainsKey(interfaceType))
            {
                if (container[interfaceType].Any(u => u.Name == name))
                {
                    container[interfaceType].Where(u => u.Name == name).First().InstanceType = classType;
                    container[interfaceType].Where(u => u.Name == name).First().LifeCycleType = lifeCycleType;
                }
                else
                {
                    container[interfaceType].Add(new ServiceModel()
                    {
                        Name = name,
                        InstanceType = classType,
                        LifeCycleType = lifeCycleType
                    });
                }
            }
            else
            {
                container.Add(interfaceType, new List<ServiceModel>()
                {
                    new ServiceModel()
                    {
                        Name = name,
                        InstanceType = classType,
                        LifeCycleType = lifeCycleType
                    }
                });
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="name">支持一接口多实现的名称</param>
        /// <returns></returns>
        public TInterface Resolve<TInterface>(string name = "")
        {
            Type interfaceType = typeof(TInterface);
            return (TInterface)ResolveObject(interfaceType, name);
        }

        /// <summary>
        /// 构造对象
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="name"></param>
        /// <returns>支持一接口多实现的名称</returns>
        private object ResolveObject(Type interfaceType, string name = "")
        {
            ServiceModel serviceModel = container[interfaceType].Where(u => u.Name == name).FirstOrDefault();

            //生命周期
            switch (serviceModel.LifeCycleType)
            {
                case LifeCycle.Transient:
                    break;
                case LifeCycle.Singleton:
                    //如果是单例，并且已经构造过，则直接返回
                    if (serviceModel.Instance != null)
                    {
                        return serviceModel.Instance;
                    }
                    break;
                case LifeCycle.Scoped:
                    break;
            }

            Type classType = serviceModel.InstanceType;

            //支持构造函数注入
            //选择参数列表最长的构造函数
            var cons = classType.GetConstructors().OrderByDescending(u => u.GetParameters().Length).First();

            var paramList = new List<object>();
            foreach (var param in cons.GetParameters())
            {
                //递归构造参数列表
                paramList.Add(ResolveObject(param.ParameterType, GetAliasValue(param)));
            }

            object instance = null;

            if (paramList.Count() > 0)
            {
                //带参数构造函数
                instance = Activator.CreateInstance(classType, paramList.ToArray()); //第二个参数一定要传object数组，不能是object的List
            }
            else
            {
                //只有无参构造函数
                instance = Activator.CreateInstance(classType);
            }

            //支持属性注入
            foreach (var prop in instance.GetType().GetProperties().Where(u => u.IsDefined(typeof(ServiceContainerAttribute), true)))
            {
                //递归构造属性
                prop.SetValue(instance, ResolveObject(prop.PropertyType, GetAliasValue(prop)));//属性的真实类型要用PropertyType
            }

            //支持方法注入
            //实例化的时候执行一下
            var methodParamList = new List<object>();
            foreach (var method in instance.GetType().GetMethods().Where(u => u.IsDefined(typeof(ServiceContainerAttribute), true)))
            {
                foreach (var param in method.GetParameters())
                {
                    methodParamList.Add(ResolveObject(param.ParameterType));
                }
                method.Invoke(instance, methodParamList.ToArray());
            }

            //生命周期
            switch (serviceModel.LifeCycleType)
            {
                case LifeCycle.Transient:
                    break;
                case LifeCycle.Singleton:
                    //如果是单例，构造完后复制给ServiceModel
                    serviceModel.Instance = instance;
                    break;
                case LifeCycle.Scoped:
                    break;
            }

            return instance;
        }

        /// <summary>
        /// 根据特性获取Resolve的别名Name
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private string GetAliasValue(ICustomAttributeProvider provider)
        {
            if (provider.IsDefined(typeof(ServiceContainerAliasAttribute), true))
            {
                return ((ServiceContainerAliasAttribute)provider.GetCustomAttributes(typeof(ServiceContainerAliasAttribute), true).First()).AliasValue;
            }
            else
            {
                return "";
            }
        }

        private class ServiceModel
        {
            public string Name { get; set; }

            public Type InstanceType { get; set; }

            public object Instance { get; set; }

            public LifeCycle LifeCycleType { get; set; }
        }

    }
}
