
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
	public interface ISelectBuilder<TEntity>
	{
		ISelectBuilder<TEntity> Select(string sql);
		ISelectBuilder<TEntity> Select(string sql, Expression<Func<TEntity, object>> mapToProperty);
		ISelectBuilder<TEntity> From(string sql);
		ISelectBuilder<TEntity> Where(string sql);
		ISelectBuilder<TEntity> GroupBy(string sql);
		ISelectBuilder<TEntity> OrderBy(string sql);
		ISelectBuilder<TEntity> Having(string sql);
		ISelectBuilder<TEntity> Paging(int currentPage, int itemsPerPage);

		ISelectBuilder<TEntity> Parameter(string name, object value);
		ISelectBuilder<TEntity> Parameters(params object[] parameters);

		TList Query<TList>() where TList : IList<TEntity>;
		TList Query<TList>(Action<dynamic, TEntity> customMapper) where TList : IList<TEntity>;
		TList Query<TList>(Action<IDataReader, TEntity> customMapper) where TList : IList<TEntity>;
		List<TEntity> Query();
		List<TEntity> Query(Action<dynamic, TEntity> customMapper);
		List<TEntity> Query(Action<IDataReader, TEntity> customMapper);
		TList QueryComplex<TList>(Action<IDataReader, IList<TEntity>> customMapper) where TList : IList<TEntity>;
		List<TEntity> QueryComplex(Action<IDataReader, IList<TEntity>> customMapper);
		TList QueryNoAutoMap<TList>(Func<dynamic, TEntity> customMapper) where TList : IList<TEntity>;
		TList QueryNoAutoMap<TList>(Func<IDataReader, TEntity> customMapper) where TList : IList<TEntity>;
		List<TEntity> QueryNoAutoMap(Func<dynamic, TEntity> customMapper);
		List<TEntity> QueryNoAutoMap(Func<IDataReader, TEntity> customMapper);
		TEntity QuerySingle();
		TEntity QuerySingle(Action<IDataReader, TEntity> customMapper);
		TEntity QuerySingle(Action<dynamic, TEntity> customMapper);
		TEntity QuerySingleNoAutoMap(Func<IDataReader, TEntity> customMapper);
		TEntity QuerySingleNoAutoMap(Func<dynamic, TEntity> customMapper);
		TValue QueryValue<TValue>();
	}

}