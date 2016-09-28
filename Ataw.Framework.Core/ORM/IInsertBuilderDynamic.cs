
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
	public interface IInsertBuilderDynamic
	{
		int Execute();
		int ExecuteReturnLastId();
		T ExecuteReturnLastId<T>();
		int ExecuteReturnLastId(string identityColumnName);
		T ExecuteReturnLastId<T>(string identityColumnName);
		IInsertBuilderDynamic AutoMap(params string[] ignoreProperties);
		IInsertBuilderDynamic Column(string columnName, object value);
		IInsertBuilderDynamic Column(string propertyName);
	}
}