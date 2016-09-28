
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
		public TList QueryNoAutoMap<TEntity, TList>(Func<dynamic, TEntity> customMapper) where TList : IList<TEntity>
		{
			var items = default(TList);

			_data.ExecuteQueryHandler.ExecuteQuery(true, () =>
			{
				items = new QueryNoAutoMapHandler<TEntity>().QueryNoAutoMap<TList>(_data, null, customMapper);
			});

			return items;
		}
		
		public List<TEntity> QueryNoAutoMap<TEntity>(Func<dynamic, TEntity> customMapper)
		{
			return QueryNoAutoMap<TEntity, List<TEntity>>(customMapper);
		}

		public TList QueryNoAutoMap<TEntity, TList>(Func<IDataReader, TEntity> customMapper) where TList : IList<TEntity>
		{
			var items = default(TList);

			_data.ExecuteQueryHandler.ExecuteQuery(true, () =>
			{
				items = new QueryNoAutoMapHandler<TEntity>().QueryNoAutoMap<TList>(_data, customMapper, null);
			});

			return items;
		}

		public List<TEntity> QueryNoAutoMap<TEntity>(Func<IDataReader, TEntity> customMapper)
		{
			return QueryNoAutoMap<TEntity, List<TEntity>>(customMapper);
		}
	}
}