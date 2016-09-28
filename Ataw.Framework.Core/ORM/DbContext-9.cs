
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
		public IDbContext UseTransaction(bool useTransaction)
		{
			ContextData.UseTransaction = useTransaction;
			return this;
		}

		public IDbContext UseSharedConnection(bool useSharedConnection)
		{
			ContextData.UseSharedConnection = useSharedConnection;
			return this;
		}

		public IDbContext IsolationLevel(IsolationLevel isolationLevel)
		{
			ContextData.IsolationLevel = isolationLevel;
			return this;
		}

		public IDbContext Commit()
		{
			VerifyTransactionSupport();

			if (ContextData.TransactionState == TransactionStates.Rollbacked)
				throw new FluentDataException("The transaction has already been rolledback");

			ContextData.Transaction.Commit();
			ContextData.TransactionState = TransactionStates.Committed;
			return this;
		}

		public IDbContext Rollback()
		{
			if (ContextData.TransactionState == TransactionStates.Rollbacked)
				return this;

			VerifyTransactionSupport();

			if (ContextData.TransactionState == TransactionStates.Committed)
				throw new FluentDataException("The transaction has already been commited");

			if (ContextData.Transaction != null)
				ContextData.Transaction.Rollback();
			ContextData.TransactionState = TransactionStates.Rollbacked;
			return this;
		}

		private void VerifyTransactionSupport()
		{
			if (!ContextData.UseTransaction)
				throw new FluentDataException("Transaction support has not been enabled.");
		}
	}
}