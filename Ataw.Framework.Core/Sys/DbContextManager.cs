
namespace Ataw.Framework.Core.Sys
{
    public class DbContextManager
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        private static readonly DbContextManager instance = new DbContextManager();

        public IUnitOfData MainUnit
        {
            get;
            set;
        }

        bool isCreate = false;

        public RegNameList<IUnitOfData> UnitList
        {
            get
            {
                var _obj = AtawAppContext.Current.PageFlyweight.PageItems["DbContextManager"];
                if (_obj == null)
                {
                    isCreate = true;
                    _obj = new RegNameList<IUnitOfData>();
                }
                else
                    isCreate = false;
                return _obj as RegNameList<IUnitOfData>;

            }

        }

        private void LoadUnitList()
        {

        }


        public static DbContextManager Current
        {
            get
            {
                return instance;
            }
        }


        private DbContextManager()
        {

        }

        public int Submit()
        {
            int result = 0;
            foreach (IUnitOfData unit in UnitList)
            {
                result += unit.Submit();
            }
            return result;
        }

    }
}
