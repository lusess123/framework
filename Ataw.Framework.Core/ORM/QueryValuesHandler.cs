
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
	internal class QueryValuesHandler<T>
	{
		public List<T> Execute(DbCommandData data)
		{
			var items = new List<T>();

			while (data.Reader.Read())
			{
				T value;

				if (data.Reader.GetFieldType(0) == typeof(T))
					value = (T) data.Reader.GetValue(0);
				else
					value = (T) Convert.ChangeType(data.Reader.GetValue(0), typeof(T));

				items.Add(value);
			}

			return items;
		}
	}
}