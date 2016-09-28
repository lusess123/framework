
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
	public partial class DbContext : IDbContext
	{
		public ISelectBuilder<TEntity> Select<TEntity>(string sql)
		{
			return new SelectBuilder<TEntity>(ContextData.Provider, CreateCommand).Select(sql);
		}

		public ISelectBuilder<TEntity> Select<TEntity>(string sql, Expression<Func<TEntity, object>> mapToProperty)
		{
			return new SelectBuilder<TEntity>(ContextData.Provider, CreateCommand).Select(sql, mapToProperty);
		}

		public IInsertBuilder Insert(string tableName)
		{
			return new InsertBuilder(ContextData.Provider, CreateCommand, tableName);
		}

		public IInsertBuilder<T> Insert<T>(string tableName, T item)
		{
			return new InsertBuilder<T>(ContextData.Provider, CreateCommand, tableName, item);
		}

		public IInsertBuilderDynamic Insert(string tableName, ExpandoObject item)
		{
			return new InsertBuilderDynamic(ContextData.Provider, CreateCommand, tableName, item);
		}

		public IUpdateBuilder Update(string tableName)
		{
			return new UpdateBuilder(ContextData.Provider, CreateCommand, tableName);
		}

		public IUpdateBuilder<T> Update<T>(string tableName, T item)
		{
			return new UpdateBuilder<T>(ContextData.Provider, CreateCommand, tableName, item);
		}

		public IUpdateBuilderDynamic Update(string tableName, ExpandoObject item)
		{
			return new UpdateBuilderDynamic(ContextData.Provider, CreateCommand, tableName, item);
		}

		public IDeleteBuilder Delete(string tableName)
		{
			return new DeleteBuilder(ContextData.Provider, CreateCommand, tableName);
		}

		public IDeleteBuilder<T> Delete<T>(string tableName, T item)
		{
			return new DeleteBuilder<T>(ContextData.Provider, CreateCommand, tableName, item);
		}

		private void VerifyStoredProcedureSupport()
		{
			if (!ContextData.Provider.SupportsStoredProcedures)
				throw new FluentDataException("The selected database does not support stored procedures.");
		}

		public IStoredProcedureBuilder StoredProcedure(string storedProcedureName)
		{
			VerifyStoredProcedureSupport();
			return new StoredProcedureBuilder(ContextData.Provider, CreateCommand, storedProcedureName);
		}

		public IStoredProcedureBuilder MultiResultStoredProcedure(string storedProcedureName)
		{
			VerifyStoredProcedureSupport();
			return new StoredProcedureBuilder(ContextData.Provider, CreateCommand.UseMultipleResultset, storedProcedureName);
		}

		public IStoredProcedureBuilder<T> StoredProcedure<T>(string storedProcedureName, T item)
		{
			VerifyStoredProcedureSupport();
			return new StoredProcedureBuilder<T>(ContextData.Provider, CreateCommand, storedProcedureName, item);
		}

		public IStoredProcedureBuilder<T> MultiResultStoredProcedure<T>(string storedProcedureName, T item)
		{
			VerifyStoredProcedureSupport();
			return new StoredProcedureBuilder<T>(ContextData.Provider, CreateCommand.UseMultipleResultset, storedProcedureName, item);
		}

		public IStoredProcedureBuilderDynamic StoredProcedure(string storedProcedureName, ExpandoObject item)
		{
			VerifyStoredProcedureSupport();
			return new StoredProcedureBuilderDynamic(ContextData.Provider, CreateCommand, storedProcedureName, item);
		}

		public IStoredProcedureBuilderDynamic MultiResultStoredProcedure(string storedProcedureName, ExpandoObject item)
		{
			VerifyStoredProcedureSupport();
			return new StoredProcedureBuilderDynamic(ContextData.Provider, CreateCommand.UseMultipleResultset, storedProcedureName, item);
		}
	}

}