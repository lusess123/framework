
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
	internal class UpdateBuilder : BaseUpdateBuilder, IUpdateBuilder, IInsertUpdateBuilder
	{
		internal UpdateBuilder(IDbProvider dbProvider, IDbCommand command, string name)
			: base(dbProvider, command, name)
		{
		}

		public virtual IUpdateBuilder Where(string columnName, object value)
		{
			Actions.WhereAction(columnName, value);
			return this;
		}

		public IUpdateBuilder Column(string columnName, object value)
		{
			Actions.ColumnValueAction(columnName, value, false);
			return this;
		}

		IInsertUpdateBuilder IInsertUpdateBuilder.Column(string columnName, object value)
		{
			Actions.ColumnValueAction(columnName, value, false);
			return this;
		}
	}
	
	internal class UpdateBuilder<T> : BaseUpdateBuilder, IUpdateBuilder<T>, IInsertUpdateBuilder<T>
	{
		internal UpdateBuilder(IDbProvider provider, IDbCommand command, string name, T item)
			: base(provider, command, name)
		{
			Data.Item = item;
		}

		public IUpdateBuilder<T> Column(string columnName, object value)
		{
			Actions.ColumnValueAction(columnName, value, false);
			return this;
		}

		public IUpdateBuilder<T> AutoMap(params Expression<Func<T, object>>[] ignoreProperties)
		{
			Actions.AutoMapColumnsAction(false, ignoreProperties);
			return this;
		}

		public IUpdateBuilder<T> Column(Expression<Func<T, object>> expression)
		{
			Actions.ColumnValueAction(expression, false);
			return this;
		}

		public virtual IUpdateBuilder<T> Where(string columnName, object value)
		{
			Actions.WhereAction(columnName, value);
			return this;
		}

		public IUpdateBuilder<T> Where(Expression<Func<T, object>> expression)
		{
			Actions.WhereAction(expression);
			return this;
		}

		IInsertUpdateBuilder<T> IInsertUpdateBuilder<T>.AutoMap(params Expression<Func<T, object>>[] ignoreProperties)
		{
			Actions.AutoMapColumnsAction(false, ignoreProperties);
			return this;
		}

		IInsertUpdateBuilder<T> IInsertUpdateBuilder<T>.Column(string columnName, object value)
		{
			Actions.ColumnValueAction(columnName, value, false);
			return this;
		}

		IInsertUpdateBuilder<T> IInsertUpdateBuilder<T>.Column(Expression<Func<T, object>> expression)
		{
			Actions.ColumnValueAction(expression, false);
			return this;
		}
	}
}