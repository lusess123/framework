using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ataw.Framework.Core.Model.Log;

namespace Ataw.Framework.Core
{



    public class JSON
    {
        private static readonly JSON instance = new JSON();
        public static JSON Current
        {
            get
            {
                return instance;
            }
        }

        public static JSON Instance
        {
            get
            {
                return instance;
            }
        }

        public T ToObject<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public string ToJSON(object oo)
        {
            Newtonsoft.Json.JsonSerializerSettings setting = new Newtonsoft.Json.JsonSerializerSettings();

            var iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            setting.Converters = new List<JsonConverter> { new StringEnumConverter(), iso };
            setting.Formatting = Formatting.Indented;
            // setting.en
            return JsonConvert.SerializeObject(oo, setting);
        }
    }


    public class AtawAppContext : IAppRegistration
    {
        public const string ATAW_WEB_REGNAME = "ATAW_WEB";
        public const string PFW_UFD = "PFW_UFD";

        public const string ATAW_DATABASE = "ATAW_DATABASE";
        public const string XML_2_DATABASE = "XML_2_DATABASE";

        /// <summary>
        /// 单例模式
        /// </summary>
        private static readonly AtawAppContext instance = new AtawAppContext();
        List<string> fAppRegistrationRegNames = null;

        public static AtawAppContext Current
        {
            get
            {
                return instance;
            }
        }


        private AtawAppContext()
        {
            BinPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            fAppRegistrationRegNames = new List<string>();
            XmlPath = Path.Combine(BinPath, AtawApplicationConfig.REAL_PATH);
        }

        public T TryToObject<T>(string str)
        {
            T obj = default(T);
            try
            {
                obj = FastJson.ToObject<T>(str);
            }
            catch
            {
                return default(T);
            }
            return obj;

        }

        #region  属性

        public bool IsInit
        {
            get;
            private set;
        }

        public bool IsAuthenticated
        {
            get
            {
                if (IsInit)
                {
                    var isAuth = PageFlyweight.PageItems["IsAuthenticated"];
                    return isAuth == null ? false : isAuth.Value<bool>();
                }
                return false;
            }
        }

        public string FControlUnitID
        {
            get
            {
                if (IsInit)
                {
                    var isAuth = PageFlyweight.PageItems["FControlUnitID"];
                    return isAuth == null ? "" : isAuth.Value<string>();
                }
                return "";
            }
        }

        public string UserId
        {
            get
            {
                if (IsInit)
                {
                    var isAuth = PageFlyweight.PageItems["UserId"];
                    return isAuth == null ? "" : isAuth.Value<string>();
                }
                return "";
            }
        }

        public string NickName
        {
            get
            {
                if (IsInit)
                {
                    var isAuth = PageFlyweight.PageItems["NickName"];
                    return isAuth == null ? "" : isAuth.Value<string>();
                }
                return "";
            }
        }

        public string UserName
        {
            get
            {
                if (IsInit)
                {
                    var isAuth = PageFlyweight.PageItems["UserName"];
                    return isAuth == null ? "" : isAuth.Value<string>();
                }
                return "";
            }
        }

        public IUnitOfData UnitOfData
        {
            get
            {
                var pageItems = PageFlyweight.PageItems;
                var _value = pageItems[ATAW_DATABASE] as IUnitOfData;
                if (_value == null)
                {
                    _value = CreateDbContext();
                    PageFlyweight.PageItems[ATAW_DATABASE] = _value;
                    // _value.UnitSign = DateTime.Now.ToString();
                }
                //AtawDebug.AssertArgumentNull(_value, "数据访问 没有被启动 ", this);
                return pageItems[ATAW_DATABASE] as IUnitOfData;
            }
            set
            {
                PageFlyweight.PageItems[ATAW_DATABASE] = value;
            }
        }

        public IFlyweight PageFlyweight
        {
            get;
            private set;
        }

        public Xml2DataBase Xml2Db
        {
            get
            {
                if (GetItem(XML_2_DATABASE) == null)
                {
                    Xml2DataBase xml2Db = new Xml2DataBase();
                    SetItem(XML_2_DATABASE, xml2Db);
                    return xml2Db;
                }
                else
                    return (Xml2DataBase)GetItem(XML_2_DATABASE);
            }
            set
            {
                SetItem(XML_2_DATABASE, value);
            }
        }

        public IAtawCache AtawCache
        {
            get;
            private set;
        }

        public JSON FastJson
        {
            get;
            private set;
        }

        public ILogs Logger
        {
            get;
            private set;
        }

        public AtawApplicationConfig ApplicationXml
        {
            get;
            private set;
        }
        public DocViewerConfig DocViewerXml
        {
            get;
            private set;
        }
        public SiteFunConfig SiteFunXml
        {
            get;
            private set;
        }
        public IEnumerable<MenuRight> MenuRights
        {
            get;
            private set;
        }
        public IEnumerable<AppConfig> AppConfigs
        {
            get;
            private set;
        }

        public UserApplicationConfig UserApplicationXml
        {
            get;
            private set;
        }

        public AtawConfigInfo ProductsXml
        {
            get;
            //  private set;
            set;
        }

        public MvcConfigInfo MvcConfigXml
        {
            get;
            set;
        }

        public FileManagementConfig FileManagementConfigXml
        {
            get;
            set;
        }

        public DBConfig DBConfigXml
        {
            get;
            private set;
        }

        public CustomScriptSet CustomScriptSet
        {
            get;
            private set;
        }

        /// <summary>
        /// 重新加载xml
        /// </summary>
        public void ReLoadXML()
        {
            LoadApplicationXml();
        }


        public string BinPath
        {
            get;
            private set;
        }

        public string WebRootPath
        {
            get;
            set;
        }

        public string MapPath
        {
            get;
            set;
        }

        public string XmlPath
        {
            get;
            private set;
        }

        public Lazy<IGroupBuilder> GroupBuilder
        {
            get;
            private set;
        }


        public Lazy<IGPSBuilder> GPSBuilder
        {
            get;
            private set;
        }
        public Lazy<IGPSSet> GPSSetBuilder
        {
            get;
            private set;
        }



        public Lazy<IAtawRightBuilder> AtawRightBuilder
        {
            get;
            private set;
        }
        public Lazy<IAtawServiceBuilder> AtawServiceBuilder
        {
            get;
            private set;
        }
        public Lazy<IMessagesBuilder> AtawMessagesBuilder
        {
            get;
            private set;
        }
        //public Lazy<IWorkflowDefBuilder> AtawWorkflowDefBuilder
        //{
        //    get;
        //    private set;
        //}
        public string DefaultConnString
        {
            get;
            private set;
        }

        public AtawDbContext CreateDbContext()
        {
            return new AtawDbContext(DefaultConnString);
        }

        #endregion

        private DateTime fBeginTime = default(DateTime);
        private LogInfo fLogInfo = new LogInfo();

        private void SignTime(string sign)
        {

            DateTime now = DateTime.Now;
            if (fBeginTime != default(DateTime))
            {
                TimeSpan span = now - fBeginTime;
                fLogInfo.TimePointList.Add(new TimePoint()
                {
                    CostTime = span.TotalMilliseconds.ToString(),
                    Name = sign,
                    Text = sign,
                    Time = now
                });
                LogBuilder("{0}耗时{1}毫秒".AkFormat(sign, span.TotalMilliseconds));
            }
            else
            {
                fLogInfo.TimePointList.Add(new TimePoint()
                {
                    CostTime = "",
                    Name = sign + "   开始计时",
                    Text = sign + "   开始计时",
                    Time = now
                });
                LogBuilder(sign + "   开始计时");
            }
            fBeginTime = now;
        }

        public void SetItem<T>(string key, T obj)
        {
            PageFlyweight.PageItems[key] = obj;
        }
        public object GetItem(string key)
        {
            return PageFlyweight.PageItems[key];
        }
        public IUnitOfData FetchUnitofData(string name, bool isNew = false)
        {
            //string _regName = DBString.CreateRegName(fControlUnitID, products);
            if (isNew)
            {
                PageFlyweight.PageItems[name] = null;
            }
            if (PageFlyweight.PageItems[name] == null)
            {
                string connStr = DBString.GetAtawDatabaseValue(name);
                var res = new AtawDbContext(connStr);
                PageFlyweight.PageItems[name] = res;
                return res;
            }
            else
            {
                return (IUnitOfData)PageFlyweight.PageItems[name];
            }
        }

        public IUnitOfData FetchUnitofData(string fControlUnitID, string products, bool isNew = false)
        {
            string _regName = DBString.CreateRegName(fControlUnitID, products);
            if (isNew)
            {
                PageFlyweight.PageItems[_regName] = null;
            }
            if (PageFlyweight.PageItems[_regName] == null)
            {
                string connStr = DBString.GetAtawControlUnitsProductValue(fControlUnitID, products.Value<ProductsType>(), "DB");
                var res = new AtawDbContext(connStr);
                PageFlyweight.PageItems[_regName] = res;
                return res;
            }
            else
            {
                return (IUnitOfData)PageFlyweight.PageItems[_regName];
            }
        }


        private void LogBuilder(params string[] strs)
        {
            SysTxtBuilder.Current.NewLine();
            //  Logger.Debug(strs);
            foreach (string str in strs)
            {
                SysTxtBuilder.Current.NewLine();
                SysTxtBuilder.Current.Append(str);
                SysTxtBuilder.Current.NewLine();
            }
            SysTxtBuilder.Current.NewLine();
        }
        //public IServiceBus AtawBus
        //{
        //    get;
        //    set;
        //}

        public DllInfo dllLog(string dll)
        {
            //this.fLogInfo.DllInfoList.Add(
            var _dll = new DllInfo()
              {
                  Name = dll,
                  ClassInfoList = new List<ClassInfo>()
              };
            this.fLogInfo.DllInfoList.Add(_dll);
            return _dll;
            //    );
        }

        public void dllErrorLog(string error) {
            this.fLogInfo.DllInfoList.Add(new DllInfo() { 
              Error = error 
            });
        }

        public void classLog(DllInfo dll, PlugInModel plug)
        {
            dll.ClassInfoList.Add(
                new ClassInfo() { 
                  Author = plug.Author,
                  BaseClass = plug.BaseType.ToString() ,
                  CreateTime = plug.CreateDate,
                  Mesg = plug.Description ,
                  RegName = plug.Key

                }
                );
        }

        public void Initialization()
        {
            IsInit = true;
            SignTime("启动");
            string appGuid = Guid.NewGuid().ToString();

            StringBuilder exSb = new StringBuilder();
            LoadApplicationXml();
            SignTime("全局配置完毕...");
            // Logger = LogManager.GetLogger(typeof(AtawAppContext), ApplicationXml.HasLogger);
            FastJson = JSON.Current;
            AtawCache = DefaultAtawCache.Current;
            //if (ApplicationXml.HasLogger)
            //{
            string fpath = Path.Combine(BinPath, AtawApplicationConfig.REAL_PATH, "AtawLogs\\log\\log.txt");
            LogManager.Configure(fpath, 1000, false);
            //  }
            // Logger.Debug("程序初始化开始： " + appGuid);
            var dconn = DBConfigXml.Databases.First(a => a.IsDefault);
            AtawDebug.AssertNotNull(dconn, "至少有一个默认的数据库连接字符串", this);
            DefaultConnString = dconn.ConnectionString;
            // MemCachedCache mcc = MemCachedCache.Current;
            SignTime("日志初始化完毕....");
            if ("EBS".AppKv<bool>(false))
            {
                //初始化 消息总线
                //AtawBus = ServiceBus.Create().Start();
            }
            SignTime("消息总线初始化完毕....");
            //载入代码插件
            //AssamblyTable at = new AssamblyTable();
            //at.Exe();
            //------------web的程序装载器
            IAppRegistration webApp = AtawIocContext.Current.FetchInstance<IAppRegistration>(ATAW_WEB_REGNAME);
            AtawDebug.AssertNotNull(webApp, "web的程序装载器 加载失败", this);
            //webApp.Initialization();
            //this.
            AppDomainTypeFinder finder = new AppDomainTypeFinder();
            finder.LoadMatchingAssemblies(Path.Combine(BinPath, "bin"));
            var assList = finder.GetAssemblies();
            assList.ForEach(
                a =>
                {
                    try
                    {
                        LogBuilder(System.Environment.NewLine + "开始检查程序集：" + a.FullName);
                        var types = a.GetTypes();
                       var dll = this.dllLog(a.FullName);
                        foreach (var t in types)
                        {
                            try
                            {
                                LogBuilder(System.Environment.NewLine + "开始检查类型：" + t.FullName);
                                SetAssamblyClassType(t,dll);
                                webApp.SetAssamblyClassType(t);
                            }
                            catch (ReflectionTypeLoadException rtlex)
                            {
                                // LogBuilder();

                                string _mesg = string.Join(System.Environment.NewLine, rtlex.LoaderExceptions.Select(b => b.Message));
                                LogBuilder(t.FullName + "插件异常单个记录", _mesg);
                                //exSb.Append(System.Environment.NewLine);
                                //SysTxtBuilder.Current.NewLine();

                                //exSb.Append(rtlex.Message);
                                //SysTxtBuilder.Current.Append(rtlex.Message);

                                //exSb.Append(System.Environment.NewLine);
                                //SysTxtBuilder.Current.NewLine();

                            }
                            catch (Exception ex)
                            {
                                // if(ex.InnerException != null)


                                LogBuilder(t.FullName + "插件异常单个记录", ex.Message, ex.InnerException == null ? "" : ex.InnerException.Message);
                                //exSb.Append(System.Environment.NewLine);
                                //SysTxtBuilder.Current.NewLine();
                                dll.ClassInfoList.Add(
                                    new ClassInfo() {
                                        Error = t.FullName + "插件异常单个记录"+
                                        ex.Message+
                                        (ex.InnerException == null ? "" : ex.InnerException.Message)
                                    }
                                    );
                                //exSb.Append(ex.Message);
                                //SysTxtBuilder.Current.Append(ex.Message);

                                //exSb.Append(System.Environment.NewLine);
                                //SysTxtBuilder.Current.NewLine();

                            }
                        }
                    }

                    catch (ReflectionTypeLoadException rtlex)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (Exception exc in rtlex.LoaderExceptions)
                        {
                            sb.AppendLine("dll载入异常记录 "+exc.Message+ (exc.InnerException == null ? "" : exc.InnerException.Message));
                            
                           // LogBuilder("dll载入异常记录", exc.Message, exc.InnerException == null ? "" : exc.InnerException.Message);
                        }

                        this.dllErrorLog(sb.ToString());
                    }
                    catch (Exception ex2)
                    {
                        LogBuilder(a.FullName + "插件程序集异常单个记录", ex2.Message, ex2.InnerException == null ? "" : ex2.InnerException.Message);
                        //exSb.Append(System.Environment.NewLine);
                        //SysTxtBuilder.Current.NewLine();
                        this.dllErrorLog(a.FullName + "插件程序集异常单个记录 "+  ex2.Message + (ex2.InnerException == null ? "" : ex2.InnerException.Message));
                        //exSb.Append(ex2.Message);
                        //SysTxtBuilder.Current.Append(ex2.Message);

                        //exSb.Append(System.Environment.NewLine);
                        //SysTxtBuilder.Current.NewLine();
                    }

                }
                );
            SignTime("程序集插件注册完毕....");
            //注册享元插件
            PageFlyweight = AtawIocContext.Current.FetchInstance<IFlyweight>("PageFlyweight");
            ////把工作单元做为享元
            //PageFlyweight.Set<IUnitOfData>("PFW_UFD", CreateDbContext());
            //载入xml 插件

            //执行全局配置的宏插件
            //   this.ApplicationXml.ExeMacro();
            SignTime("执行全局配置的宏插件完毕....");

            string rightRegName = ApplicationXml.RightBuilder.RegName;
            AtawRightBuilder = new Lazy<IAtawRightBuilder>(
                () => AtawIocContext.Current.FetchInstance<IAtawRightBuilder>(rightRegName)
                );

            string groupRegName = ApplicationXml.GroupBuilder.RegName;
            GroupBuilder = new Lazy<IGroupBuilder>(
                () => AtawIocContext.Current.FetchInstance<IGroupBuilder>(groupRegName)
                );

            //注册gps
            string gpsRegName = ApplicationXml.GPSBuilder.RegName;
            GPSBuilder = new Lazy<IGPSBuilder>(
                () => AtawIocContext.Current.FetchInstance<IGPSBuilder>(gpsRegName)
                );

            //注册gpsSet
            string gpsSetRegName = ApplicationXml.GPSSetBuilder.RegName;
            GPSSetBuilder = new Lazy<IGPSSet>(
                () => AtawIocContext.Current.FetchInstance<IGPSSet>(gpsSetRegName)
                );

            //注册内部服务
            string ServiceRegName = ApplicationXml.ServiceBuilder.RegName;
            AtawServiceBuilder = new Lazy<IAtawServiceBuilder>(
                () => AtawIocContext.Current.FetchInstance<IAtawServiceBuilder>(ServiceRegName)
                );
            //注册内部信息接口
            string MessagesRegName = ApplicationXml.MessagesBuilder.RegName;
            AtawMessagesBuilder = new Lazy<IMessagesBuilder>(
                () => AtawIocContext.Current.FetchInstance<IMessagesBuilder>(MessagesRegName)
                );
            ////注册工作流模式接口
            //string WorkflowDefRegName = ApplicationXml.WorkflowDefBuilder.RegName;
            //AtawWorkflowDefBuilder = new Lazy<IWorkflowDefBuilder>(
            //    () => AtawIocContext.Current.FetchInstance<IWorkflowDefBuilder>(WorkflowDefRegName)
            //    );
            //载入  codeTable xml
            SignTime("app配置注册完毕....");
            InitAppXml();

            SignTime("固定接口注册完毕....");
            InitCodeTableXml();
            SignTime("XMl代码表注册完毕....");
            InitDataDictXml();
            SignTime("XMl数据字典注册完毕....");
            InitCustomScriptXml();


            SignTime("自定义js注册完毕....");
            //载入控制器
            //AtawIocContext.Current.FetchInstance<IAppRegistration>(AtawAppContext.ATAW_WEB_REGNAME).Initialization();
            webApp.Initialization();

            ExeAppRegistration();
            SignTime("全局插件执行完毕....");
            // Logger.Debug("程序初始化结束： " + appGuid);
            //  Logger.Debug("插件异常总结： " + exSb.ToString());
            // SysTxtBuilder.Current.Flush(LogType.Plugin);
            AtawTrace.WriteFile(LogType.Plugin, this.fLogInfo.ToJson());


            if ("CombineJs".AppKv<bool>(false))
            {
                //ICombineJs combine = "CombineJs".CodePlugIn<ICombineJs>();
                //bool CombinJsResult = combine.init(AtawAppContext.Current.BinPath);
                //if (!CombinJsResult)
                //{
                //    throw new AtawException("压缩JS出错!", this);
                //}
                //SignTime("js压缩执行完毕....");
            }


        }
        //读取xml文件，进行合并，同时加到菜单上去
        private void InitAppXml()
        {
            this.AppConfigs = AppConfig.ReadConfigFromDir();
            List<MenuRight> _newMenuRights = new List<MenuRight>();
            foreach (var appconfig in AppConfigs)
            {
                _newMenuRights = ConbinNode(_newMenuRights, appconfig.RootMenuRights);
            }
            this.MenuRights = _newMenuRights;
        }
        //根节点相同，则合并
        private List<MenuRight> ConbinNode(List<MenuRight> newMenuRights, List<MenuRight> menuRights)
        {
            foreach (var menuRight in menuRights)
            {
                if (!newMenuRights.Contains(menuRight))
                {
                    newMenuRights.Add(menuRight);
                    if (menuRight.Children.Count > 0)
                    {
                        ConbinNode(newMenuRights[newMenuRights.Count - 1].Children, menuRight.Children);
                    }
                }
            }
            return newMenuRights;
        }

        private void InitCustomScriptXml()
        {
            var customScriptXmls = CustomScriptConfig.ReadConfigFromDir();
            CustomScriptSet = new CustomScriptSet();
            CustomScriptSet.CommonScripts = new List<string>();
            CustomScriptSet.SeaScripts = new Dictionary<string, string>();
            foreach (CustomScriptConfig config in customScriptXmls)
            {
                foreach (CustomScript script in config.CustomScripts)
                {
                    if (script.RegName.IsEmpty())
                    {
                        CustomScriptSet.CommonScripts.Add(script.Path);
                    }
                    else
                    {
                        CustomScriptSet.SeaScripts.Add(script.RegName, script.Path);
                    }
                }
            }
        }

        private void InitCodeTableXml()
        {
            bool isCache = "CodeTableCache".AppKv<bool>(false);
            var configs = DbCodeTableXml.ReadConfigFromDir();
            foreach (var config in configs)
            {
                foreach (var ct in config.DbCodeTables)
                {
                    AtawIocContext.Current.RegisterTypeByCreator(ct.RegName,
                         typeof(CodeTable<CodeDataModel>),
                         typeof(InternalDbCodeTable),
                          ct);
                    // <CodeTable<CodeDataModel>>(ct.InternalDbCodeTable, ct.RegName);
                    var gg = AtawIocContext.Current.FetchInstance<CodeTable<CodeDataModel>>(ct.RegName) as InternalDbCodeTable;
                    //gg.Description = "CodeTable.xml定义的xml插件";
                    //gg.Author = ct.Author;
                    //gg.CreateDate = ct.CreateDate;
                    //gg.Description = ct.Description;
                    if (ct.HasCache && isCache)
                    {
                        //载入所有的数据缓存起来
                        //  Logger.Debug("缓存codetable", gg.Key);
                        //var interDt = ct.InternalDbCodeTable;
                        string _sign = gg.Sign.IsEmpty() ? gg.RegName : gg.Sign;
                        gg.FillAllData(null).ToList().ForEach(
                            a =>
                            {

                                string key = string.Format(ObjectUtil.SysCulture, DbCodeTable.CODE_TABLE_KEY_STR,
                                    _sign, a.CODE_VALUE);
                                AtawAppContext.Current.AtawCache.Set(key, a, "");
                            }
                            );
                        //  Logger.Debug("缓存codetable完毕", gg.Key);
                    }

                }
            }
        }

        private void InitDataDictXml()
        {
            var configs = DataDictXml.ReadConfigFromDir();
            foreach (var config in configs)
            {
                foreach (var ct in config.DataDicts)
                {
                    XmlCodeTable xml = new XmlCodeTable(ct);


                    var gg = AtawIocContext.Current.RegisterPlugIn<CodeTable<CodeDataModel>>(xml, ct.RegName);
                    gg.Description = "DataDIct.xml:数据字典定义的xml插件";
                    gg.Author = ct.Author;
                    gg.CreateDate = ct.CreateDate;
                    gg.Description = ct.Description;
                    //if (ct.HasCache)
                    //{
                    //    //载入所有的数据缓存起来
                    //    Logger.Debug("缓存codetable", gg.Key);
                    //    var interDt = ct.InternalDbCodeTable;
                    //    string _sign = interDt.Sign.IsEmpty() ? interDt.RegName : interDt.Sign;
                    //    interDt.FillAllData(null).ToList().ForEach(
                    //        a =>
                    //        {

                    //            string key = string.Format(ObjectUtil.SysCulture, DbCodeTable.CODE_TABLE_KEY_STR,
                    //                _sign, a.CODE_VALUE);
                    //            AtawAppContext.Current.AtawCache.Set(key, a);
                    //        }
                    //        );
                    //    Logger.Debug("缓存codetable完毕", gg.Key);
                    //}

                }
            }
        }


        private void ExeAppRegistration()
        {
            foreach (string regname in fAppRegistrationRegNames)
            {
                var _app = AtawIocContext.Current.FetchInstance<IAppRegistration>(regname);
                _app.Initialization();
            }
        }

        private void LoadApplicationXml()
        {
            ApplicationXml = AtawApplicationConfig.ReadConfig(BinPath);
            //  UserApplicationXml = UserApplicationConfig.ReadConfig(BinPath);
            ProductsXml = AtawConfigInfo.ReadConfig(BinPath);
            MvcConfigXml = MvcConfigInfo.ReadConfig(BinPath);
            FileManagementConfigXml = FileManagementConfig.ReadConfig(BinPath);
            //-------------


            DBConfigXml = DBConfig.ReadConfig(BinPath);
            SiteFunXml = SiteFunConfig.ReadConfig(BinPath);
            DocViewerXml = DocViewerConfig.ReadConfig(BinPath);

        }

        public void SaveToProdutsXml(AtawConfigInfo xmlObj)
        {
            xmlObj.SaveFileByName("ProductConfig.xml");

        }
        public void SetAssamblyClassType(Type type)
        { 
        }


        public void SetAssamblyClassType(Type type ,DllInfo dll)
        {

            //throw new NotImplementedException();
            var code = Attribute.GetCustomAttribute(type, typeof(CodePlugAttribute)) as CodePlugAttribute;
            if (code != null)
            {
                if (type.IsEnum)
                {
                    EnumCodeTable ect = new EnumCodeTable(type);
                    var gg = AtawIocContext.Current.RegisterPlugIn<CodeTable<CodeDataModel>>(ect, code.RegName);
                    gg.Description = "枚举类型的  " + type.Name;
                    this.classLog(dll, gg);
                }
                else
                {
                    try
                    {


                        var _gg = AtawIocContext.Current.RegisterPlugIn(type, code);
                        this.classLog(dll,_gg);

                        if (typeof(IAppRegistration).IsAssignableFrom(type))
                        {
                            _gg.Description = "全局插件  " + type.Name;
                            fAppRegistrationRegNames.Add(code.RegName);
                        }

                    }
                    catch (Exception ex)
                    {
                        LogBuilder("注册插件失败: ", ex.Message, type.Name);
                    }
                    //Logger.Debug("注册插件", 
                }

            }
            // AppRegistrationRegNames = fAppRegistrationRegNames;

        }

        public bool GetAppBoolValue(string key)
        {
            var _kv = ApplicationXml.AppSettings[key];
            if (_kv != null)
            {
                return _kv.Value<bool>();
            }
            return false;
        }

        public bool FetchFun(string name)
        {
            var fun = SiteFunXml.GetFunByName(name);
            if (fun != null)
            {
                var _funObj = fun.RegName.PlugGet<IFunFetch>();
                if (_funObj != null)
                {
                    _funObj.Init(fun.RegName, fun.Name, fun.Param1, fun.Param2, fun.Param3);
                    return _funObj.Fetch().Value<bool>();
                }
            }
            return false;
        }

        public bool IsWinApp
        {
            get;
            set;
        }
    }
}
