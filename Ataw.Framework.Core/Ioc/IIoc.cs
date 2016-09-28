using System;
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    interface IIoc
    {


        T FetchInstanceByParam<T>( string name , object parameterOverrides);
       
        T FetchInstance<T>();
        T FetchInstance<T>(string name);
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
        void RegisterType<TTo, TFrom>(string regName) where TTo : TFrom;
        void RegisterType<T>(T instace, string regName);
        void RegisterTypeByCreator(string name, Type from,Type to, object cObject);

        PlugInModel RegisterPlugIn<TTo, TFrom>(string regName) where TTo : TFrom;
        PlugInModel RegisterPlugIn(Type to, Type from, string regName);

        PlugInModel RegisterPlugIn<T>(T instance, string regName);
        PlugInModel RegisterPlugIn(Type t, object instance, string regName);

        PlugInModel RegisterPlugIn(Type to, CodePlugAttribute codeAtt);
        RegNameList<PlugInModel> PlugInModelList { get; set; }
    }
}
