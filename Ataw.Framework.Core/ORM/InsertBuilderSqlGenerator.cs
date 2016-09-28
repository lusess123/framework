
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
	internal class InsertBuilderSqlGenerator
	{
		public string GenerateSql(IDbProvider provider, string parameterPrefix, BuilderData data)
		{
			var insertSql = "";
			var valuesSql = "";
			foreach (var column in data.Columns)
			{
				if (insertSql.Length > 0)
				{
					insertSql += ",";
					valuesSql += ",";
				}

				insertSql += provider.EscapeColumnName(column.ColumnName);
				valuesSql += parameterPrefix + column.ParameterName;
			}

			var sql = string.Format("insert into {0}({1}) values({2})",
										data.ObjectName,
										insertSql,
										valuesSql);
			return sql;
		}
	}
}