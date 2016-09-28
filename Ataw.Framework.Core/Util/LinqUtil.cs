using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public static class LinqUtil
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }
        #region 排序
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }
        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split(',');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
        #endregion
        public static IQueryable<T> QueryData<T>(this IQueryable<T> source, Expression<Func<T, bool>> where, Pagination pagination)
        {
            try
            {
                if (where != null)
                {
                    source = source.Where(where.Compile()).AsQueryable<T>();
                }
                pagination.TotalCount = source.Count();
                if (pagination.IsASC)
                {
                    return source.OrderBy<T>(pagination.SortName).QueryPage<T>(pagination);
                }
                else
                {
                    return source.OrderByDescending<T>(pagination.SortName).QueryPage<T>(pagination);
                }
            }
            catch
            {
                return null;
            }
        }
        public static IQueryable<T> QueryData<T>(this IQueryable<T> source, Expression<Func<T, bool>> where)
        {
            if (where != null)
            {
                return source.Where(where);
            }
            return source;
        }

        public static IQueryable<T> QueryPage<T>(this IQueryable<T> source, Pagination page)
        {
            return source.Skip<T>((page.PageIndex) * page.PageSize).Take<T>(page.PageSize);
        }


        public static ICollection<T>  GetManyPage<T,S>(this IQueryable<T> dbset, Expression<Func<T, bool>> where, Expression<Func<T, S>> orderByExpression, bool IsDESC, int PageIndex, int PageSize, out int TotalRecord)
        {
            TotalRecord = 0;

            var list = IsDESC ? dbset.OrderByDescending(orderByExpression) : dbset.OrderBy(orderByExpression);

            if (TotalRecord >= 0)
            {
                if (where != null)
                {
                    var result = list.Where(where.Compile());
                    TotalRecord = result.Count();
                    return result.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
                }
                else
                {
                    TotalRecord = list.Count();
                    return list.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
                }
            }
            return null;
        }
    }
}
