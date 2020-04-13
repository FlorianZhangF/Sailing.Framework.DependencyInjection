using System;
using System.Collections.Generic;
using System.Text;

namespace Sailing.Framework.DependencyInjection
{
    public interface IServiceContainer
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="name">支持一接口多实现的名称</param>
        void Register<TInterface, TClass>(string name = "", LifeCycle lifeCycleType = LifeCycle.Transient) where TClass : TInterface;

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="name">支持一接口多实现的名称</param>
        /// <returns></returns>
        TInterface Resolve<TInterface>(string name = "");
    }

    public enum LifeCycle
    {
        Transient,
        Singleton,
        Scoped
    }

}
