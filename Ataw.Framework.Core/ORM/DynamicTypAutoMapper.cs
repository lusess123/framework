
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
	internal class DynamicTypAutoMapper
	{
		private readonly DbCommandData _dbCommandData;

		public DynamicTypAutoMapper(DbCommandData dbCommandData)
		{
			_dbCommandData = dbCommandData;
		}

		public ExpandoObject AutoMap()
		{
			var item = new ExpandoObject();

			var fields = DataReaderHelper.GetDataReaderFields(_dbCommandData.Reader);

			var itemDictionary = (IDictionary<string, object>) item;

			foreach (var column in fields)
			{
				var value = DataReaderHelper.GetDataReaderValue(_dbCommandData.Reader, column.Index, true);

				itemDictionary.Add(column.Name, value); 
			}

			return item;
		}
	}
}