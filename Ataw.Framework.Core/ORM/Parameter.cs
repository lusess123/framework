
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
	public class Parameter
	{
		public string ParameterName { get; set; }
		public DataTypes DataType { get; set; }
		public object Value { get; set; }
		public ParameterDirection Direction { get; set; }
		public bool IsId { get; set; }
		public int Size { get; set; }

		public string GetParameterName(IDbProvider provider)
		{
			return provider.GetParameterName(ParameterName);
		}
	}
}