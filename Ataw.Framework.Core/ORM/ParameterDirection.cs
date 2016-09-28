
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
	public enum ParameterDirection
	{
		// The parameter is an input parameter.
		Input = 1,

		// The parameter is an output parameter.
		Output = 2,

		// The parameter is capable of both input and output.
		InputOutput = 3,

		// The parameter represents a return value from an operation such as a stored
		// procedure, built-in function, or user-defined function.
		ReturnValue = 6,
	}

}