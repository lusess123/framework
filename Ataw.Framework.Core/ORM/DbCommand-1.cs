
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
	internal partial class DbCommand : IDisposable, IDbCommand
	{
		private readonly DbCommandData _data;

		public DbCommand(
			DbContext dbContext,
			System.Data.IDbCommand dbCommand,
			DbContextData dbContextData)
		{
			_data = new DbCommandData();
			_data.Context = dbContext;
			_data.ContextData = dbContextData;
			_data.InnerCommand = dbCommand;
			_data.Command = this;
			_data.ExecuteQueryHandler = new ExecuteQueryHandler(_data, this);
		}

        public DbCommandData Data() {
            return this._data;
        }

		internal IDbCommand UseMultipleResultset
		{
			get
			{
				if (!_data.ContextData.Provider.SupportsMultipleResultset)
					throw new FluentDataException("The selected database does not support multiple resultset");

				_data.UseMultipleResultsets = true;
				return this;
			}
		}

		public IDbCommand CommandType(DbCommandTypes dbCommandType)
		{
			_data.CommandType = dbCommandType;
			_data.InnerCommand.CommandType = (System.Data.CommandType) dbCommandType;
			return this;
		}

		internal void ClosePrivateConnection()
		{
			if (!_data.ContextData.UseTransaction
				&& !_data.ContextData.UseSharedConnection)
			{
				_data.InnerCommand.Connection.Close();

				if (_data.ContextData.OnConnectionClosed != null)
					_data.ContextData.OnConnectionClosed(new OnConnectionClosedEventArgs(_data.InnerCommand.Connection));
			}
		}

		public void Dispose()
		{
			if (_data.Reader != null)
				_data.Reader.Close();

			ClosePrivateConnection();
		}
	}
}