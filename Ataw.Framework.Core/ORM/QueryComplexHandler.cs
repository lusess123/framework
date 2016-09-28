
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
	internal class QueryComplexHandler<TEntity, TList>
		where TList : IList<TEntity>
	{
		public TList Execute(DbCommandData data, Action<IDataReader, IList<TEntity>> customMapper)
		{
			var items = (TList) data.ContextData.EntityFactory.Create(typeof(TList));

			while (data.Reader.Read())
				customMapper(data.Reader, items);

			return items;
		}
	}

}