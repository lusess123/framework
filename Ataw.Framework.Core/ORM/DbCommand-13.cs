
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
		public T QueryValue<T>()
		{
			T value = default(T);

			_data.ExecuteQueryHandler.ExecuteQuery(true,
				() =>
				{
					value = new QueryValueHandler<T>().Execute(_data);
				});

			return value;
		}

		public List<T> QueryValues<T>()
		{
			List<T> values = null;

			_data.ExecuteQueryHandler.ExecuteQuery(true,
				() =>
				{
					values = new QueryValuesHandler<T>().Execute(_data);
				});

			return values;
		}
	}
}