
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
		private void ConnectionStringInternal(string connectionString, DbProviderTypes dbProviderType, IDbProvider dbProvider)
		{
			ContextData.ConnectionString = connectionString;
			ContextData.ProviderType = dbProviderType;
			ContextData.Provider = dbProvider;
		}

		public IDbContext ConnectionString(string connectionString, DbProviderTypes dbProviderType)
		{
			ConnectionStringInternal(connectionString, dbProviderType, new DbProviderFactory().GetDbProvider(dbProviderType));
			return this;
		}

		public IDbContext ConnectionString(string connectionString, IDbProvider dbProvider)
		{
			ConnectionStringInternal(connectionString, DbProviderTypes.Custom, dbProvider);
			return this;
		}

		public IDbContext ConnectionStringName(string connectionstringName, DbProviderTypes dbProviderType)
		{
			ConnectionStringInternal(GetConnectionStringFromConfig(connectionstringName), dbProviderType, new DbProviderFactory().GetDbProvider(dbProviderType));
			return this;
		}

		public IDbContext ConnectionStringName(string connectionstringName, IDbProvider dbProvider)
		{
			ConnectionStringInternal(GetConnectionStringFromConfig(connectionstringName), DbProviderTypes.Custom, dbProvider);
			return this;
		}

		private string GetConnectionStringFromConfig(string connectionStringName)
		{
			var settings = ConfigurationManager.ConnectionStrings[connectionStringName];
			if (settings == null)
				throw new FluentDataException("A connectionstring with the specified name was not found in the .config file");
			return settings.ConnectionString;
		}
	}
}