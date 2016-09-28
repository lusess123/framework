
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
	internal class DbProviderFactory
	{
		public virtual IDbProvider GetDbProvider(DbProviderTypes dbProvider)
		{
			IDbProvider provider = null;
			switch (dbProvider)
			{
				case DbProviderTypes.SqlServer:
				case DbProviderTypes.SqlAzure:
					provider = new SqlServerProvider();
					break;
				case DbProviderTypes.SqlServerCompact40:
					provider = new SqlServerCompactProvider();
					break;
				case DbProviderTypes.Oracle:
					provider = new OracleProvider();
					break;
				case DbProviderTypes.MySql:
					provider = new MySqlProvider();
					break;
				case DbProviderTypes.Access:
					provider = new AccessProvider();
					break;
				case DbProviderTypes.Sqlite:
					provider = new Sqlite();
					break;
				case DbProviderTypes.PostgreSql:
					provider = new PostgreSqlProvider();
					break;
				case DbProviderTypes.DB2:
					provider = new DB2Provider();
					break;
			}

			return provider;
		}
	}
}