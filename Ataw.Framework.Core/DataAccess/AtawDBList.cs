using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Ataw.Framework.Core
{
    public class AtawDBList
    {
        public enum DatabaseType
        {
            Right = 0,
            MRP = 1,
            Clothing = 2,
            Dispatch = 3,
            OA = 4,
            Stor = 5,
            VM = 6,
            Service = 7,
            GPS = 8,
            GPSSet = 9,
        }
        public List<AtawDbContext> LdbCon
        {
            get;
            set;
        }

        public string GetFid()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + "A" + Guid.NewGuid().ToString().Replace("-", "");
        }

        public AtawDbContext CreateAtawDbContext(string nameOrConnectionString)
        {
            AtawDbContext dbCon = null;
            List<AtawDbContext> dbConList = new List<AtawDbContext>();
            if (LdbCon != null)
                dbConList = LdbCon;
            if (LdbCon != null)
            {
                dbCon = LdbCon.Where(i => i.NameOrConnectionString == nameOrConnectionString).ToList().First();
            }

            if (dbCon == null)
            {
                dbCon = new AtawDbContext(nameOrConnectionString);
                dbConList.Add(dbCon);
                LdbCon = dbConList;
            }
            return dbCon;
        }
        /// <summary>
        /// 注册事务
        /// </summary>
        /// <param name="dbcon">连接类</param>
        /// <param name="storedName">存储过程名称</param>
        /// <param name="param">存储过程参数</param>
        /// <returns>注册事务后的连接类</returns>
        public AtawDbContext RegisterStored(AtawDbContext dbcon, string storedName, params SqlParameter[] param)
        {
            dbcon.RegisterStored(storedName, param);
            return dbcon;
        }
        /// <summary>
        /// 全部提交
        /// </summary>
        public void SubmitAll()
        {
            foreach (AtawDbContext dbcon in LdbCon)
            {
                dbcon.Submit();
            }
        }
    }
}
