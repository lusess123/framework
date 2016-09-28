
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core.DataAccess
{
    public abstract class RepositoryBase<T> : IDisposable, IRepository<T> where T : class
    {
        private AtawDbContext fDataContext;
        protected readonly IDbSet<T> dbset;

        protected RepositoryBase(AtawDbContext DbContext)
        {
            fDataContext = DbContext;
            dbset = DataContext.Set<T>();
        }

      
        /// <summary>
        /// 获得提供用于查询和使用对象形式的实体数据功能
        /// </summary>
        public DbContext DataContext
        {
            get { return fDataContext; }
        }



        public IDbSet<T> DbSet
        {
            get
            {
                return dbset;
            }
        }

        #region lizi

        ///// <summary>
        ///// 添加方法
        ///// </summary>
        ///// <param name="entity">对象实体类</param>
        //public virtual void Add(T entity)
        //{
        //    dbset.Add(entity);
        //}

        ///// <summary>
        ///// 更新方法
        ///// </summary>
        ///// <param name="entity">对象实体类</param>
        //public virtual void Update(T entity)
        //{
        //    dbset.Attach(entity);
        //    fDataContext.Entry(entity).State = EntityState.Modified;
        //}

        ///// <summary>
        ///// 删除方法
        ///// </summary>
        ///// <param name="entity">对象实体类</param>
        //public virtual void Delete(T entity)
        //{
        //    dbset.Remove(entity);
        //}

        ///// <summary>
        ///// 根据指定条件删除方法
        ///// </summary>
        ///// <param name="where">删除条件</param>
        //public void Delete(Func<T, Boolean> where)
        //{
        //    IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
        //    foreach (T obj in objects)
        //    {
        //        dbset.Remove(obj);
        //    }
        //}

        ///// <summary>
        ///// 根据id获取一条记录
        ///// </summary>
        ///// <param name="id">编号id</param>
        ///// <returns></returns>
        //public virtual T GetById(long id)
        //{
        //    return dbset.Find(id);
        //}

        ///// <summary>
        ///// 根据条件获得所有的记录
        ///// </summary>
        ///// <param name="where">查找条件</param>
        ///// <returns>所有的记录数</returns>
        //public virtual IEnumerable<T> GetMany(Func<T, bool> where)
        //{
        //    if (where != null)
        //    {
        //        return dbset.Where(where).ToList();
        //    }
        //    else
        //    {
        //        return dbset.ToList();
        //    }
        //}

        ///// <summary>
        ///// 根据条件获得一条记录
        ///// </summary>
        ///// <param name="where">查找条件</param>
        ///// <returns>一条记录</returns>
        //public T Get(Func<T, Boolean> where)
        //{
        //    return dbset.Where(where).FirstOrDefault<T>();

        //}

        ///// <summary>
        ///// 查询并分页
        ///// </summary>
        ///// <param name="ProcName">存储过程名称</param>
        ///// <param name="param">参数列表</param>
        ///// <returns>返回数据集</returns>
        //public virtual IEnumerable<T> GetByCondition(string ProcName, params object[] parameters)
        //{
        //    return DataContext.Database.SqlQuery<T>(ProcName, parameters).ToList();
        //}

        ///// <summary>
        ///// 执行存储过程
        ///// </summary>
        ///// <param name="ProcName">存储过程名称</param>
        ///// <param name="param">参数列表</param>
        ///// <returns>返回影响的行数</returns>
        //public virtual int ExecuteSqlCommand(string ProcName, SqlParameterCollection param)
        //{
        //    return DataContext.Database.ExecuteSqlCommand(ProcName, param);
        //}

        /////// <summary>
        /////// Ajax 无刷新分页
        /////// </summary>
        /////// <typeparam name="S">排序字段的类型</typeparam>
        /////// <param name="where">查找条件</param>
        /////// <param name="orderByExpression">排序字段</param>
        /////// <param name="IsDESC">排序方式</param>
        /////// <param name="PageIndex">当前页</param>
        /////// <param name="PageSize">每页显示记录数</param>
        /////// <param name="TotalRecord">总记录数</param>
        /////// <returns>所有的记录数</returns>
        ////public virtual PagedList<T> GetPagerList<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderByExpression, bool IsDESC, int PageIndex, int PageSize, out int TotalRecord)
        ////{
        ////    TotalRecord = 0;

        ////    var list = IsDESC ? dbset.OrderByDescending(orderByExpression) : dbset.OrderBy(orderByExpression);

        ////    if (TotalRecord >= 0)
        ////    {
        ////        if (where != null)
        ////        {
        ////            var result = list.Where(where);
        ////            TotalRecord = result.Count();
        ////            return result.ToPagedList(PageIndex, PageSize);
        ////        }
        ////        else
        ////        {
        ////            TotalRecord = list.Count();
        ////            return list.ToPagedList(PageIndex, PageSize);
        ////        }
        ////    }
        ////    return list.ToPagedList(PageIndex, PageSize);
        ////}


        ///// <summary>
        ///// Ajax 无刷新分页
        ///// </summary>
        ///// <typeparam name="S">排序字段的类型</typeparam>
        ///// <param name="where">查找条件</param>
        ///// <param name="orderByExpression">排序字段</param>
        ///// <param name="IsDESC">排序方式</param>
        ///// <param name="PageIndex">当前页</param>
        ///// <param name="PageSize">每页显示记录数</param>
        ///// <param name="TotalRecord">总记录数</param>
        ///// <returns>所有的记录数</returns>
        //public virtual IEnumerable<T> GetByCondition<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderByExpression, bool IsDESC, int PageIndex, int PageSize, out int TotalRecord)
        //{
        //    TotalRecord = 0;

        //    var list = IsDESC ? dbset.OrderByDescending(orderByExpression) : dbset.OrderBy(orderByExpression);

        //    if (TotalRecord >= 0)
        //    {
        //        if (where != null)
        //        {
        //            var result = list.Where(where.Compile());
        //            TotalRecord = result.Count();
        //            return result.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        //        }
        //        else
        //        {
        //            TotalRecord = list.Count();
        //            return list.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        //        }
        //    }
        //    return null;
        //}
        #endregion


        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (fDataContext != null)
                {
                    fDataContext.Dispose();
                }
            }
        }
        #endregion



        public virtual T GetByKey(object key)
        {
            return dbset.Find(key);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            // Expression<Func<T, bool>> eWhere = where as Expression<Func<T, bool>>;
            return dbset.Where(where).FirstOrDefault();
        }

        public virtual ICollection<T> GetMany(Func<T, bool> where)
        {
            return dbset.Where(where).ToList();
        }

        public virtual IQueryable<T> QueryMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }

        //public IQueryable<T> QueryPage<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderByExpression, Pagination page)
        //{
        //    IQueryable<T> result = null;
        //    if (where != null)
        //    {
        //        result = dbset.Where(where);
        //        // page 不赋值
        //    }
        //    return result.Skip((page.PageIndex) * page.PageSize).Take(page.PageSize);
        //}

        public virtual ICollection<T> GetMany<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderByExpression, bool IsDESC, int PageIndex, int PageSize, out int TotalRecord)
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

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Add(ICollection<T> entitys)
        {
            //int i = 0;
            foreach (var entity in entitys)
            {
                dbset.Add(entity);
                // i++;
            }
            // return i;
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            fDataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Edit(T entity)
        {

        }
        

        public virtual void Edit(T bean, Action<T> editBlock)
        {
            editBlock(bean);
        }

        public virtual void Update(T entity, params string[] columns)
        {
            dbset.Attach(entity);
            var stateEntry = ((IObjectContextAdapter)fDataContext).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            foreach (string str in columns)
            {
                stateEntry.SetModifiedProperty(str);
            }
        }

        public virtual void DeleteByKey(params object[] keyValues)
        {
            var endity = dbset.Find(keyValues);
            dbset.Remove(endity);
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Func<T, bool> predicate)
        {
            IEnumerable<T> objects = dbset.Where<T>(predicate).AsEnumerable();
            foreach (T obj in objects)
            {
                dbset.Remove(obj);
            }
        }

        public virtual T GetByKey(params object[] keyValues)
        {
            return dbset.Find(keyValues);
        }


        //public IQueryable<T> QueryMany(Expression<Func<T, bool>> where)
        //{
        //    return dbset.Where(where);
        //}


    }
}
