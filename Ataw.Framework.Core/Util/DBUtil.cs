using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Ataw.Core;

namespace Ataw.Framework.Core
{
    public static class DBUtil
    {
        public static StringBuilder DbCommandToString(SqlCommand cmd, StringBuilder sb)
        {
            int length = cmd.Parameters.Count;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    sb.AppendFormat("DECLARE {0} {1} ",
                          cmd.Parameters[i],
                          cmd.Parameters[i].DbType.ToString(),
                          cmd.Parameters[i].Size, cmd.Parameters[i].Value.Value<string>());
                    sb.Append(Environment.NewLine);
                    sb.AppendFormat("set {0} =  {1} ", cmd.Parameters[i], cmd.Parameters[i].Value.Value<string>());
                }
            }
            sb.Append("--查询文本：");
            sb.AppendLine();
            sb.AppendLine(cmd.CommandText);
            sb.AppendLine();
          

            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    sb.AppendFormat("--名称:{0} 类型：{1} 长度：{2} 值：{3}",
                          cmd.Parameters[i],
                          cmd.Parameters[i].DbType.ToString(),
                          cmd.Parameters[i].Size, cmd.Parameters[i].Value.Value<string>());
                    sb.Append(Environment.NewLine);
                }
            }
            return sb;


        }

        public static StringBuilder DbCommandToString(IEnumerable<DbParameter> items, StringBuilder sb)
        {
            foreach (var item in items)
            {

                foreach (var par in items)
                {
                    sb.AppendFormat("名称:{0} 类型：{1} 长度：{2} 值：{3}",
                        par.ParameterName,
                        par.DbType.ToString(),
                        par.Size, par.Value.ToString());
                    sb.Append(Environment.NewLine);
                }

            }
            return sb;
        }

        public static StringBuilder CommandToString(this ICollection<CommandItem> items, StringBuilder sb)
        {
            foreach (var item in items)
            {
                sb.Append(Environment.NewLine);
                sb.Append("操作类型：");
                sb.Append(item.CommandType.ToString());
                sb.Append(Environment.NewLine);
                sb.Append(item.CommandText);
                sb.Append(Environment.NewLine);
                if (item.Parameters.Count > 0)
                {
                    foreach (var par in item.Parameters)
                    {
                        sb.AppendFormat("名称:{0} 类型：{1} 长度：{2} 值：{3}",
                            par.ParameterName,
                            par.SqlDbType.ToString(),
                            par.Size, par.Value.ToString());
                        sb.Append(Environment.NewLine);
                    }
                }
            }
            return sb;
        }
        //SqlTrans
        public static StringBuilder SqlTransToString(this ICollection<SqlTrans> items, StringBuilder sb)
        {
            foreach (var item in items)
            {
                sb.Append(Environment.NewLine);
                sb.Append("操作类型：");
                sb.Append(item.CommandType.ToString());
                sb.Append(Environment.NewLine);
                sb.Append(item.sql);
                sb.Append(Environment.NewLine);
                var paramCount = item.param == null ? 0 : item.param.Count();
                if (paramCount > 0)
                {
                    foreach (var par in item.param)
                    {
                        if (par != null && par.ParameterName != null && par.SqlDbType != null && par.Size != null && par.Value != null)
                            sb.AppendFormat("名称:{0} 类型：{1} 长度：{2} 值：{3}",
                                par.ParameterName,
                                par.SqlDbType.ToString(),
                                par.Size, par.Value.ToString());

                        sb.Append(Environment.NewLine);
                    }
                }
            }
            return sb;
        }
    }
}
