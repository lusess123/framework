
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
	public class PropertyExpressionParser<T>
	{
		private readonly object _item;
		private PropertyInfo _property;

		public PropertyExpressionParser(object item, Expression<Func<T, object>> propertyExpression)
		{
			_item = item;
			_property = GetProperty(propertyExpression);
		}

		private static PropertyInfo GetProperty(Expression<Func<T, object>> exp)
		{
			PropertyInfo result;
			if (exp.Body.NodeType == ExpressionType.Convert)
				result = ((MemberExpression) ((UnaryExpression) exp.Body).Operand).Member as PropertyInfo;
			else result = ((MemberExpression) exp.Body).Member as PropertyInfo;

			if (result != null)
				return typeof(T).GetProperty(result.Name);

			throw new ArgumentException(string.Format("Expression '{0}' does not refer to a property.", exp.ToString()));
		}
		
		public object Value
		{
			get { return ReflectionHelper.GetPropertyValue(_item, _property); }
		}

		public string Name
		{
			get { return _property.Name; }
		}

		public Type Type
		{
			get { return ReflectionHelper.GetPropertyType(_property); }
		}
	}
}