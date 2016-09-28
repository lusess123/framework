
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
	internal class QuerySingleNoAutoMapHandler<TEntity>
	{
		internal TEntity ExecuteSingleNoAutoMap(DbCommandData data,
			Func<IDataReader, TEntity> customMapperReader,
			Func<dynamic, TEntity> customMapperDynamic)
		{
			var item = default(TEntity);

			if (data.Reader.Read())
			{
				if (customMapperReader != null)
					item = customMapperReader(data.Reader);
				else if (customMapperDynamic != null)
				{
					var dynamicObject = new DynamicTypAutoMapper(data).AutoMap();
					item = customMapperDynamic(dynamicObject);
				}
			}

			return item;
		}
	}
}