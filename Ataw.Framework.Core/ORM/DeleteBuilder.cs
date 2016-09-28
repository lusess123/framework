
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
	internal class DeleteBuilder : BaseDeleteBuilder, IDeleteBuilder
	{
		public DeleteBuilder(IDbProvider provider, IDbCommand command, string tableName)
			: base(provider, command, tableName)
		{
		}

		public IDeleteBuilder Where(string columnName, object value)
		{
			Actions.ColumnValueAction(columnName, value, false);
			return this;
		}
	}
	
	internal class DeleteBuilder<T> : BaseDeleteBuilder, IDeleteBuilder<T>
	{
		public DeleteBuilder(IDbProvider provider, IDbCommand command, string tableName, T item)
			: base(provider, command, tableName)
		{
			Data.Item = item;
		}
		public IDeleteBuilder<T> Where(Expression<Func<T, object>> expression)
		{
			Actions.ColumnValueAction(expression, false);
			return this;
		}

		public IDeleteBuilder<T> Where(string columnName, object value)
		{
			Actions.ColumnValueAction(columnName, value, false);
			return this;
		}
	}
}