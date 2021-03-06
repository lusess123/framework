
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
	internal class InsertBuilderDynamic : BaseInsertBuilder, IInsertBuilderDynamic, IInsertUpdateBuilderDynamic
	{
		internal InsertBuilderDynamic(IDbProvider provider, IDbCommand command, string name, ExpandoObject item)
			: base(provider, command, name)
		{
			Data.Item = (IDictionary<string, object>) item;
		}

		public IInsertBuilderDynamic Column(string columnName, object value)
		{
			Actions.ColumnValueAction(columnName, value, false);
			return this;
		}

		public IInsertBuilderDynamic Column(string propertyName)
		{
			Actions.ColumnValueDynamic((ExpandoObject) Data.Item, propertyName);
			return this;
		}

		public IInsertBuilderDynamic AutoMap(params string[] ignoreProperties)
		{
			Actions.AutoMapDynamicTypeColumnsAction(false, ignoreProperties);
			return this;
		}

		IInsertUpdateBuilderDynamic IInsertUpdateBuilderDynamic.AutoMap(params string[] ignoreProperties)
		{
			Actions.AutoMapDynamicTypeColumnsAction(false, ignoreProperties);
			return this;
		}

		IInsertUpdateBuilderDynamic IInsertUpdateBuilderDynamic.Column(string columnName, object value)
		{
			Actions.ColumnValueAction(columnName, value, false);
			return this;
		}

		IInsertUpdateBuilderDynamic IInsertUpdateBuilderDynamic.Column(string propertyName)
		{
			Actions.ColumnValueDynamic((ExpandoObject) Data.Item, propertyName);
			return this;
		}
	}
}