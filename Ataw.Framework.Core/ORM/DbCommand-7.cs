
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
		/// <returns>Numbers of records affected.</returns>
		public int Execute()
		{
			int recordsAffected = 0;

			_data.ExecuteQueryHandler.ExecuteQuery(false, () =>
			{
				recordsAffected = new ExecuteHandler().Execute<int>(_data);
			});
			return recordsAffected;
		}
	}
}