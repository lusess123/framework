
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
	public interface IDeleteBuilder
	{
		int Execute();
		IDeleteBuilder Where(string columnName, object value);
	}

	public interface IDeleteBuilder<T>
	{
		int Execute();
		IDeleteBuilder<T> Where(Expression<Func<T, object>> expression);
		IDeleteBuilder<T> Where(string columnName, object value);
	}
}