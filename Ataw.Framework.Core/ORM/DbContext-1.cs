
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
		protected DbContextData ContextData;

		public DbContext()
		{
			ContextData = new DbContextData();
		}

		internal void CloseSharedConnection()
		{
			if (ContextData.Connection == null)
				return;

			if (ContextData.UseTransaction)
			{
				if (ContextData.TransactionState == TransactionStates.None)
					Rollback();
			}

			ContextData.Connection.Close();

			if (ContextData.OnConnectionClosed != null)
				ContextData.OnConnectionClosed(new OnConnectionClosedEventArgs(ContextData.Connection));
		}

		public void Dispose()
		{
			CloseSharedConnection();
		}
	}
}