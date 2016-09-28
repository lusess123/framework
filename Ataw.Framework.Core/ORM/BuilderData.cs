
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
	public class BuilderData
	{
		public int PagingCurrentPage { get; set; }
		public int PagingItemsPerPage { get; set; }
		public List<TableColumn> Columns { get; set; }
		public object Item { get; set; }
		public string ObjectName { get; set; }
		public DbCommandData CommandData { get; set; }
		public IDbProvider Provider { get; set; }
		public IDbCommand Command { get; set; }
		public List<TableColumn> Where { get; set; }
		public string Having { get; set; }
		public string GroupBy { get; set; }
		public string OrderBy { get; set; }
		public string From { get; set; }
		public string Select { get; set; }
		public string WhereSql { get; set; }

		public BuilderData(IDbProvider provider, IDbCommand command, string objectName)
		{
			Provider = provider;
			ObjectName = objectName;
			Command = command;
			Columns = new List<TableColumn>();
			Where = new List<TableColumn>();
			Having = "";
			GroupBy = "";
			OrderBy = "";
			From = "";
			Select = "";
			WhereSql = "";
			PagingCurrentPage = 1;
			PagingItemsPerPage = 0;
		}

		internal int GetFromItems()
		{
			return (GetToItems() - PagingItemsPerPage + 1);
		}

		internal int GetToItems()
		{
			return (PagingCurrentPage*PagingItemsPerPage);
		}
	}
}