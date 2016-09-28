
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
	internal class QueryNoAutoMapHandler<TEntity>
	{
		internal TList QueryNoAutoMap<TList>(DbCommandData data,
			Func<IDataReader, TEntity> customMapperReader,
			Func<dynamic, TEntity> customMapperDynamic)
			where TList : IList<TEntity>
		{
			var items = (TList) data.ContextData.EntityFactory.Create(typeof(TList));

			DynamicTypAutoMapper dynamicAutoMapper = null;

			while (data.Reader.Read())
			{
				var item = default(TEntity);

				if (customMapperReader != null)
				{
					item = customMapperReader(data.Reader);
				}
				else if (customMapperDynamic != null)
				{
					if (dynamicAutoMapper == null)
						dynamicAutoMapper = new DynamicTypAutoMapper(data);

					var dynamicObject = dynamicAutoMapper.AutoMap();
					item = customMapperDynamic(dynamicObject);
				}

				items.Add(item);
			}

			return items;
		}
	}
}