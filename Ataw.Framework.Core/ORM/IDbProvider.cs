
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
	public interface IDbProvider
	{
		string ProviderName { get; }
		bool SupportsMultipleResultset { get; }
		bool SupportsMultipleQueries { get; }
		bool SupportsOutputParameters { get; }
		bool SupportsStoredProcedures { get; }
		bool SupportsExecuteReturnLastIdWithNoIdentityColumn { get; }
		IDbConnection CreateConnection(string connectionString);
		string GetParameterName(string parameterName);
		string GetSelectBuilderAlias(string name, string alias);
		string GetSqlForSelectBuilder(BuilderData data);
		string GetSqlForInsertBuilder(BuilderData data);
		string GetSqlForUpdateBuilder(BuilderData data);
		string GetSqlForDeleteBuilder(BuilderData data);
		string GetSqlForStoredProcedureBuilder(BuilderData data);
		DataTypes GetDbTypeForClrType(Type clrType);
		T ExecuteReturnLastId<T>(DbCommandData data, string identityColumnName);
		void OnCommandExecuting(DbCommandData data);
		string EscapeColumnName(string name);
	}
}