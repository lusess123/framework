using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Reflection;

namespace Ataw.Framework.Core
{
    public class AtawIocContext : IIoc
    {

        private static ResolverOverride[] GetResolverOverride(object parameterOverrides)
        {
            if (parameterOverrides == null) return null;
            var properties = parameterOverrides
             .GetType()
             .GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var overridesArray = properties
             .Select(p => new ParameterOverride(p.Name, p.GetValue(parameterOverrides, null)))
             .Cast<ResolverOverride>()
             .ToArray();
            return overridesArray;

        }




        /// <summary>
        /// 但例模式标准实现
        /// </summary>
        private static readonly AtawIocContext instance = new AtawIocContext();

        private AtawIocContext()
        {
            MainContainer = new UnityContainer();
            PlugInModelList = new RegNameList<PlugInModel>();
        }

        public static AtawIocContext Current
        {
            get
            {
                return instance;
            }
        }

        public IUnityContainer MainContainer { get; set; }

        public void Load()
        {


        }

        private void AtawSetObject(Type type, object obj, string param)
        {
            if (type == typeof(CodeTable<CodeDataModel>))
            {
                CodeTable<CodeDataModel> o = (CodeTable<CodeDataModel>)obj;
                o.Param = param;
            }

        }
        public void RegisterTypeByCreator(string name, Type from, Type to,object cObject)
        {
            MainContainer.RegisterType(from,to, name, new InjectionConstructor(cObject));
        }
        #region 代理注册方法
        private void InternalRegisterType(Type from, Type to, string Regname)
        {
            AtawDebug.Assert(Regname.IndexOf("-") == -1, "注册名{0}不允许包含‘-’字符", this);
            MainContainer.RegisterType(from, to, Regname);
        }
        public void InternalRegisterType<T>(T instace, string Regname)
        {
            AtawDebug.Assert(Regname.IndexOf("-") == -1, "注册名{0}不允许包含‘-’字符", this);
            MainContainer.RegisterInstance<T>(Regname, instace);
        }
        private PlugInModel InternalRegisterPlugIn(Type baseType, object instance, string Regname)
        {
            AtawDebug.Assert(Regname.IndexOf("-") == -1, "注册名{0}不允许包含‘-’字符", this);
            MainContainer.RegisterInstance(baseType, Regname, instance);
            return null;
        }
        private void InternalRegisterType<TTo, TFrom>(string Regname) where TTo : TFrom
        {
            //MainContainer.RegisterType<TTo, TFrom>("");
            AtawDebug.Assert(Regname.IndexOf("-") == -1, "注册名{0}不允许包含‘-’字符", this);
            MainContainer.RegisterType<TFrom, TTo>(Regname);
        }
        #endregion

        public void RegisterType<TTo, TFrom>(string Regname) where TTo : TFrom
        {
            //MainContainer.RegisterType<TTo, TFrom>("");
            // AtawDebug.Assert(Regname.IndexOf("-") == -1, "注册名{0}不允许包含‘-’字符", this);
            InternalRegisterType<TTo, TFrom>(Regname);
        }



        public T FetchInstanceByParam<T>(string name, object parameterOverrides)
        {
            try
            {
                T instance = MainContainer.Resolve<T>(name ,GetResolverOverride(parameterOverrides));
                return instance;
            }
            catch (Exception ex)
            {
                string str = string.Format(ObjectUtil.SysCulture, "基类为{0}的唯一插件获取失败,请检查是否注册成功", typeof(T).ToString());
                throw new AtawException(str, ex, this);
            }
        }


        public T FetchInstance<T>()
        {
            try
            {
                T instance = MainContainer.Resolve<T>();
                return instance;
            }
            catch (Exception ex)
            {
                string str = string.Format(ObjectUtil.SysCulture, "基类为{0}的唯一插件获取失败,请检查是否注册成功", typeof(T).ToString());
                throw new AtawException(str, ex, this);
            }
        }
        public T FetchInstance<T>(string name)
        {
            try
            {
                string[] names = name.Split('-');
                T instance = MainContainer.Resolve<T>(names[0]);
                if (names.Length > 1)
                {
                    AtawSetObject(typeof(T), instance, names[1]);
                }
                return instance;

            }
            catch (ResolutionFailedException ex)
            {
                string str = string.Format(ObjectUtil.SysCulture, "注册名为{0},基类为{1}的插件获取失败,请检查是否注册成功或者构造函数调用失败{2}/r/n{3}", name, typeof(T).ToString(), ex.Message, ex.InnerException.Message);
                throw new AtawException(str, ex, this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetService(Type serviceType)
        {
            return (serviceType.IsClass && !serviceType.IsAbstract) ||
                MainContainer.IsRegistered(serviceType) ?
                MainContainer.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return (serviceType.IsClass && !serviceType.IsAbstract) ||
                MainContainer.IsRegistered(serviceType) ?
                MainContainer.ResolveAll(serviceType) : new object[] { };
        }


        public PlugInModel RegisterPlugIn<TTo, TFrom>(string Regname) where TTo : TFrom
        {
            return RegisterPlugIn(typeof(TTo), typeof(TFrom), Regname);
        }


        public RegNameList<PlugInModel> PlugInModelList
        {
            get;
            set;
        }

        public PlugInModel RegisterPlugIn(Type to, Type from, string Regname)
        {
            string regname = from.AssemblyQualifiedName + Regname;
            var plug = PlugInModelList[regname];
            if (plug == null)
            {
                InternalRegisterType(from, to, Regname);
                // InternalRegisterType(
                var model = new PlugInModel();
                model.BaseType = from;
                model.InstanceType = to;
                model.RegName = regname;
                model.Key = Regname;
                PlugInModelList.Add(model);
                return model;
            }
            return plug;
        }
        public PlugInModel RegisterPlugIn(Type to, CodePlugAttribute codeAtt)
        {
            string regName = codeAtt.RegName;
            var plug = RegisterPlugIn(to, codeAtt.BaseClass, regName);
            // PlugInModelList[codeAtt.BaseClass.AssemblyQualifiedName + regName];
            //MainContainer.RegisterType(codeAtt.BaseClass, to, regName);
            plug.Author = codeAtt.Author;
            plug.CreateDate = codeAtt.CreateDate;
            plug.Description = codeAtt.Description;
            if (codeAtt.Tags != null && codeAtt.Tags.Count() > 0)
                plug.Tags = codeAtt.Tags.ToList();
            Log(plug);
            //AtawTrace.WriteFile(LogType.Plugin, log);
            // plug.
            return plug;
        }

        public void RegisterType<T>(T instace, string Regname)
        {
            // AtawDebug.Assert(Regname.IndexOf("-") == -1, "注册名{0}不允许包含‘-’字符", this);
            InternalRegisterType<T>(instace, Regname);
        }

        private void Log(PlugInModel plug)
        {
            string log = String.Format(ObjectUtil.SysCulture, "\r\n注册名：{0}\r\n基类:{2}\r\n实例类:{1}\r\n" +
                "路径:{3}\r\n作者:{4}\r\n描述:{5}",
            plug.Key,
            plug.InstanceType.ToString(),
            plug.BaseType.ToString(),
            plug.InstanceType.Assembly.Location,
             plug.Author,
             plug.Description
            );
           // AtawAppContext.Current.Logger.Debug("注册插件", log);
            SysTxtBuilder.Current.Append(log);
        }


        public PlugInModel RegisterPlugIn<T>(T instance, string Regname)
        {
            RegisterType<T>(instance, Regname);
            string key = typeof(T).AssemblyQualifiedName + Regname;
            var plug = PlugInModelList[key];
            // PlugInModel model = null;
            if (plug == null)
            {
                plug = new PlugInModel();
                plug.BaseType = typeof(T);
                plug.InstanceType = plug.BaseType;
                plug.RegName = key;
                plug.Key = Regname;
                PlugInModelList.Add(plug);
                //return model;
            }
            Log(plug);
            return plug;
        }

        public PlugInModel RegisterPlugIn(Type baseType, object instance, string Regname)
        {
            //AtawDebug.Assert(Regname.IndexOf("-") == -1, "注册名{0}不允许包含‘-’字符", this);
            // MainContainer.RegisterInstance(baseType, Regname, instance);
            InternalRegisterPlugIn(baseType, instance, Regname);

            return null;
        }
    }
}
