using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.Practices.Unity;

namespace Ataw.Framework.Core
{
    public class AssamblyTable
    {

        public string CodePath { get; set; }

        public Dictionary<string, Assembly> AssamblyList { get; private set; }

        public Dictionary<string, InitializationAttribute> AssamblyInitialization { get; private set; }

       // public Dictionary<string, Type> PlugInList { get; private set; }

        public AssamblyTable()
        {
            AssamblyList = new Dictionary<string, Assembly>();
            AssamblyInitialization = new Dictionary<string, InitializationAttribute>();
           // PlugInList = new Dictionary<string, Type>();

            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            CodePath = str;
        }


        public void Exe()
        {
            GetByDomin();
            GetByCodePath();

            SetAssamblyInitialization();
            RegistIoc();
        }

        public void GetByCodePath()
        {
            string[] files = Directory.GetFiles(CodePath, "*.dll", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                AssemblyName assemblyName;
                assemblyName = AssemblyName.GetAssemblyName(file);
                Assembly assembly;
                string name = assemblyName.FullName;
                if (AssamblyList.ContainsKey(name))
                {
                    assembly = AssamblyList[name];
                }
                else
                {
                    assembly = Assembly.Load(assemblyName);
                    AssamblyList.Add(name, assembly);
                }

            }
        }
        private void GetByDomin()
        {
            // Dictionary<string, Assembly> loaded = new Dictionary<string, Assembly>();
            AppDomain domain = AppDomain.CurrentDomain;
            Assembly[] assemblies = domain.GetAssemblies();

            foreach (Assembly ass in assemblies)
            {
                string name = ass.FullName;
                if (!AssamblyList.ContainsKey(name))
                    AssamblyList.Add(name, ass);
            }
        }
        private void SetAssamblyInitialization()
        {
            AssamblyList.ToList().ForEach(
                a =>
                {
                    InitializationAttribute attribute =
                        (InitializationAttribute)Attribute.GetCustomAttribute(a.Value, typeof(InitializationAttribute));
                    if (attribute != null)
                    {
                        AssamblyInitialization.Add(a.Key, attribute);
                    }
                }
                );
        }

        private void RegistIoc()
        {
            AssamblyList.ToList().ForEach(
                a =>
                {
                    Assembly list = AssamblyList[a.Key];
                    var types = list.GetTypes();
                    foreach (var t in types)
                    {
                        // Type baseType = t.BaseType;
                        var code = Attribute.GetCustomAttribute(t, typeof(CodePlugAttribute)) as CodePlugAttribute;
                        if (code != null)
                        {
                           // AtawIocContext.Current.PlugInModelList[];
                            AtawIocContext.Current.RegisterPlugIn(t,  code);

                        }
                    }
                }
                );
        }

    }
}
