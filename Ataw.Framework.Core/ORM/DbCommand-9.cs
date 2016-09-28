
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
		private TList Query<TEntity, TList>(
								Action<IDataReader, TEntity> customMapperReader,
								Action<dynamic, TEntity> customMapperDynamic)
			where TList : IList<TEntity>
		{
			var items = default(TList);

			_data.ExecuteQueryHandler.ExecuteQuery(true, () =>
			{
				items = new GenericQueryHandler<TEntity>().ExecuteListReader<TList>(_data, customMapperReader, customMapperDynamic);
			});

			return items;
		}

		public TList Query<TEntity, TList>()
			where TList : IList<TEntity>
		{
			return Query<TEntity, TList>(null, null);
		}

		public List<TEntity> Query<TEntity>()
		{
			return Query<TEntity, List<TEntity>>(null, null);
		}

		public TList Query<TEntity, TList>(Action<IDataReader, TEntity> customMapper)
			where TList : IList<TEntity>
		{
			return Query<TEntity, TList>(customMapper, null);
		}

		public TList Query<TEntity, TList>(Action<dynamic, TEntity> customMapper)
			where TList : IList<TEntity>
		{
			return Query<TEntity, TList>(null, customMapper);
		}

		public List<TEntity> Query<TEntity>(Action<IDataReader, TEntity> customMapper)
		{
			return Query<TEntity, List<TEntity>>(customMapper, null);
		}

		public List<TEntity> Query<TEntity>(Action<dynamic, TEntity> customMapper)
		{
			return Query<TEntity, List<TEntity>>(null, customMapper);
		}
	}
}