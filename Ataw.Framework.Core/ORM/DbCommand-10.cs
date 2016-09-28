
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
		public TList QueryComplex<TEntity, TList>(Action<IDataReader, IList<TEntity>> customMapper)
			where TList : IList<TEntity>
		{
			TList items = default(TList);

			_data.ExecuteQueryHandler.ExecuteQuery(true, () =>
			{
				items = new QueryComplexHandler<TEntity, TList>().Execute(_data, customMapper);
			});

			return items;
		}

		public List<TEntity> QueryComplex<TEntity>(Action<IDataReader, IList<TEntity>> customMapper)
		{
			return QueryComplex<TEntity, List<TEntity>>(customMapper);
		}
	}
}