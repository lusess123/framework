
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
	internal partial class DbCommand
	{
		public IDbCommand Sql(string sql)
		{
			_data.Sql.Append(sql);
			return this;
		}

		public IDbCommand Sql<T>(string sql, params Expression<Func<T, object>>[] mappingExpressions)
		{
			if (mappingExpressions == null)
				Sql(sql);
			else
			{
				var propertyNames = ReflectionHelper.GetPropertyNamesFromExpressions(mappingExpressions);
				for (int i = 0; i < propertyNames.Count; i++)
				{
					propertyNames[i] = propertyNames[i].Replace('.', '_');
				}

				_data.Sql.AppendFormat(sql, propertyNames.ToArray());
			}
			return this;
		}
	}
}