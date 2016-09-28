
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
	public class DbCommandData
	{
		public IDbCommand Command { get; set; }
		public DbContext Context { get; set; }
		public DbContextData ContextData { get; set; }
		public System.Data.IDbCommand InnerCommand { get; set; }
		public StringBuilder Sql { get; set; }
		public bool UseMultipleResultsets { get; set; }
		public IDataReader Reader { get; set; }
		public System.Data.IDataReader InnerReader { get; set; }
		internal ExecuteQueryHandler ExecuteQueryHandler;
		public DbCommandTypes CommandType { get; set; }

		public DbCommandData()
		{
			CommandType = DbCommandTypes.Text;
			Sql = new StringBuilder();
		}
	}
}