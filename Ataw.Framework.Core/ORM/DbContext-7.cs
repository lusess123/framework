
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
	public partial class DbContext : IDbContext
	{
		public IDbContext OnConnectionOpening(Action<OnConnectionOpeningEventArgs> action)
		{
			ContextData.OnConnectionOpening = action;
			return this;
		}

		public IDbContext OnConnectionOpened(Action<OnConnectionOpenedEventArgs> action)
		{
			ContextData.OnConnectionOpened = action;
			return this;
		}

		public IDbContext OnConnectionClosed(Action<OnConnectionClosedEventArgs> action)
		{
			ContextData.OnConnectionClosed = action;
			return this;
		}

		public IDbContext OnExecuting(Action<OnExecutingEventArgs> action)
		{
			ContextData.OnExecuting = action;
			return this;
		}

		public IDbContext OnExecuted(Action<OnExecutedEventArgs> action)
		{
			ContextData.OnExecuted = action;
			return this;
		}

		public IDbContext OnError(Action<OnErrorEventArgs> action)
		{
			ContextData.OnError = action;
			return this;
		}
	}

}