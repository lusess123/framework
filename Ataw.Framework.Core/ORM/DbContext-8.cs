
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
		private DbCommand CreateCommand
		{
			get
			{
				IDbConnection connection = null;

				if (ContextData.UseTransaction
					|| ContextData.UseSharedConnection)
				{
					if (ContextData.Connection == null)
						ContextData.Connection = ContextData.Provider.CreateConnection(ContextData.ConnectionString);
					connection = ContextData.Connection;
				}
				else
					connection = ContextData.Provider.CreateConnection(ContextData.ConnectionString);

				var cmd = connection.CreateCommand();
				cmd.Connection = connection;

				return new DbCommand(this, cmd, ContextData);
			}
		}

		public IDbCommand Sql(string sql, params object[] parameters)
		{
			var command = CreateCommand.Sql(sql);
			if (parameters != null)
				command.Parameters(parameters);
			return command;
		}

		public IDbCommand Sql<T>(string sql, params Expression<Func<T, object>>[] mappingExpressions)
		{
			var command = CreateCommand.Sql(sql, mappingExpressions);
			
			return command;
		}

		public IDbCommand MultiResultSql()
		{
			return CreateCommand.UseMultipleResultset;
		}

		public IDbCommand MultiResultSql(string sql, params object[] parameters)
		{
			var command = CreateCommand.UseMultipleResultset.Sql(sql);
			if (parameters != null)
				command.Parameters(parameters);
			return command;
		}

		public IDbCommand MultiResultSql<T>(string sql, params Expression<Func<T, object>>[] mappingExpressions)
		{
			var command = CreateCommand.UseMultipleResultset.Sql(sql, mappingExpressions);
			return command;
		}
	}
}