
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
	internal class StoredProcedureBuilder : BaseStoredProcedureBuilder, IStoredProcedureBuilder
	{
		internal StoredProcedureBuilder(IDbProvider dbProvider, IDbCommand command, string name)
			: base(dbProvider, command, name)
		{
		}

		public IStoredProcedureBuilder Parameter(string name, object value)
		{
			Actions.ColumnValueAction(name, value, true);
			return this;
		}

		public IStoredProcedureBuilder ParameterOut(string name, DataTypes parameterType, int size = 0)
		{
			Actions.ParameterOutputAction(name, parameterType, size);
			return this;
		}
	}
	
	internal class StoredProcedureBuilder<T> : BaseStoredProcedureBuilder, IStoredProcedureBuilder<T>
	{
		internal StoredProcedureBuilder(IDbProvider dbProvider, IDbCommand command, string name, T item)
			: base(dbProvider, command, name)
		{
			Data.Item = item;
		}

		public IStoredProcedureBuilder<T> Parameter(string name, object value)
		{
			Actions.ColumnValueAction(name, value, true);
			return this;
		}

		public IStoredProcedureBuilder<T> AutoMap(params Expression<Func<T, object>>[] ignoreProperties)
		{
			Actions.AutoMapColumnsAction(true, ignoreProperties);
			return this;
		}

		public IStoredProcedureBuilder<T> Parameter(Expression<Func<T, object>> expression)
		{
			Actions.ColumnValueAction(expression, true);

			return this;
		}

		public IStoredProcedureBuilder<T> ParameterOut(string name, DataTypes parameterType, int size = 0)
		{
			Actions.ParameterOutputAction(name, parameterType, size);
			return this;
		}
	}
}