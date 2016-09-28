
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
	internal class QueryValueHandler<T>
	{
		public T Execute(DbCommandData data)
		{
			var value = default(T);

			if (data.Reader.Read())
			{
				if (data.Reader.GetFieldType(0) == typeof(T))
					value = (T) data.Reader.GetValue(0);
				else
					value = (T) Convert.ChangeType(data.Reader.GetValue(0), typeof(T));
			}
			
			return value;
		}
	}
}