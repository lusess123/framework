
// FluentData version 2.3.0.0.
// Copyright ©  2012 - Lars-Erik Kindblad (http://www.kindblad.com).
// See http://fluentdata.codeplex.com for more information and licensing terms.

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Dynamic;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace FluentData
{
	internal class DeleteBuilderSqlGenerator
	{
		public string GenerateSql(IDbProvider provider, string parameterPrefix, BuilderData data)
		{
			var whereSql = "";
			foreach (var column in data.Columns)
			{
				if (whereSql.Length > 0)
					whereSql += " and ";

				whereSql += string.Format("{0} = {1}{2}",
												provider.EscapeColumnName(column.ColumnName),
												parameterPrefix,
												column.ParameterName);
			}

			var sql = string.Format("delete from {0} where {1}", data.ObjectName, whereSql);
			return sql;
		}
	}

}