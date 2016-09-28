
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ataw.Framework.Core.DataAccess
{
    public interface IRepository<T> where T : class
    {
        #region 查询
        T GetByKey(params object[] keyValues);
        T Get(Expression<Func<T, bool>> where);
        ICollection<T> GetMany(Func<T, Boolean> where);
        IQueryable<T> QueryMany(Expression<Func<T, Boolean>> where);
        ICollection<T> GetMany<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderByExpression, bool IsDESC, int PageIndex, int PageSize, out int TotalRecord);
        #endregion

        #region 增
        void Add(T entity);
        void Add(ICollection<T> entitys);
        #endregion

        #region 改

        void Edit(T entity);

        void Update(T entity);
        void Update(T entity, params string[] columns);
        //void Update(ICollection<T> entitys);
        void Edit(T bean, Action<T> editBlock);
        #endregion

        #region 删除
        void DeleteByKey(params object[] keyValues);
        void Delete(T entity);
        void Delete(Func<T, Boolean> predicate);
        #endregion


        //void Add(T entity);
        //void Delete(T entity);
        //void Delete(Func<T, Boolean> predicate);
        //void Update(T entity);
        //T GetById(long Id);
        //T Get(Func<T, Boolean> where);
        ////IList<T> GetMany(Func<T, bool> where);
        //IList<T> GetByCondition(string ProcName, params object[] parameters);
        //// PagedList<T> GetByCondition<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderByExpression, bool IsDESC, int PageIndex, int PageSize, out int TotalRecord);
        //IList<T> GetByCondition<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderByExpression, bool IsDESC, int PageIndex, int PageSize, out int TotalRecord);
        //PagedList<T> GetPagerList<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderByExpression, bool IsDESC, int PageIndex, int PageSize, out int TotalRecord);
    }
}
