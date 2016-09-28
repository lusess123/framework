
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
		public TEntity QuerySingle<TEntity>()
		{
			return QuerySingle<TEntity>(null);
		}

		public TEntity QuerySingle<TEntity>(Action<IDataReader, TEntity> customMapper)
		{
			var item = default(TEntity);

			_data.ExecuteQueryHandler.ExecuteQuery(true, () =>
			{
				item = new GenericQueryHandler<TEntity>().ExecuteSingle(_data, customMapper, null);
			});

			return item;
		}

		public TEntity QuerySingle<TEntity>(Action<dynamic, TEntity> customMapper)
		{
			var item = default(TEntity);

			_data.ExecuteQueryHandler.ExecuteQuery(true, () =>
			{
				item = new GenericQueryHandler<TEntity>().ExecuteSingle(_data, null, customMapper);
			});

			return item;
		}
	}
}