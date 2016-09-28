
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
	internal class DataReaderHelper
	{
		internal static List<DataReaderField> GetDataReaderFields(IDataReader reader)
		{
			var columns = new List<DataReaderField>();

			for (var i = 0; i < reader.FieldCount; i++)
			{
				var column = new DataReaderField(i, reader.GetName(i), reader.GetFieldType(i));

				if (columns.SingleOrDefault(x => x.LowerName == column.LowerName) == null)
					columns.Add(column);
			}

			return columns;
		}

		internal static object GetDataReaderValue(IDataReader reader, int index, bool isNullable)
		{
			var value = reader[index];
			var type = value.GetType();

			if (value == DBNull.Value)
			{
				if (isNullable)
					return null;

				if (type == typeof(DateTime))
					return DateTime.MinValue;
			}

			return value;
		}
	}
}