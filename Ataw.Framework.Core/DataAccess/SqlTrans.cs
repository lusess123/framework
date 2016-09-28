using System.Data.SqlClient;
using System.Data;

namespace Ataw.Framework.Core
{
    public class SqlTrans
    {
        public string sql { get; set; }
        public CommandType CommandType { get; set; }
        public SqlParameter[] param { get; set; }
    }
}
