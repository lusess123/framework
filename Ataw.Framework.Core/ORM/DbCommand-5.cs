
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
	internal partial class DbCommand
	{
		public int ExecuteReturnLastId()
		{
			return ExecuteReturnLastId<int>();
		}

		public T ExecuteReturnLastId<T>()
		{
			if (!_data.ContextData.Provider.SupportsExecuteReturnLastIdWithNoIdentityColumn)
				throw new FluentDataException("The selected database does not support this method.");

			var lastId = _data.ContextData.Provider.ExecuteReturnLastId<T>(_data, null);

			return lastId;
		}

		public int ExecuteReturnLastId(string identityColumnName)
		{
			return ExecuteReturnLastId<int>(identityColumnName);
		}

		public T ExecuteReturnLastId<T>(string identityColumnName)
		{
			if (_data.ContextData.Provider.SupportsExecuteReturnLastIdWithNoIdentityColumn)
				throw new FluentDataException("The selected database does not support this method.");

			var lastId = _data.ContextData.Provider.ExecuteReturnLastId<T>(_data, identityColumnName);

			return lastId;
		}

		
	}
}