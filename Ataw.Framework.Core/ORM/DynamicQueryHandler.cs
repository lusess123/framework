
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
	internal class DynamicQueryHandler
	{
		public List<dynamic> ExecuteList(DbCommandData data)
		{
			var items = new List<dynamic>();

			var autoMapper = new DynamicTypAutoMapper(data);

			while (data.Reader.Read())
			{
				var item = autoMapper.AutoMap();

				items.Add(item);
			}

			return items;
		}

		public dynamic ExecuteSingle(DbCommandData data)
		{
			var autoMapper = new DynamicTypAutoMapper(data);

			ExpandoObject item = null;

			if (data.Reader.Read())
				item = autoMapper.AutoMap();

			return item;
		}
	}

}