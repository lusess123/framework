
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
	public interface IInsertUpdateBuilder
	{
		IInsertUpdateBuilder Column(string columnName, object value);
	}
	
	public interface IInsertUpdateBuilder<T>
	{
		IInsertUpdateBuilder<T> AutoMap(params Expression<Func<T, object>>[] ignoreProperties);
		IInsertUpdateBuilder<T> Column(string columnName, object value);
		IInsertUpdateBuilder<T> Column(Expression<Func<T, object>> expression);
	}
}