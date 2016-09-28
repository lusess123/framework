
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core.DataAccess
{
    public class Repository<T,TContext> : RepositoryBase<T>, IDisposable where T : class  where TContext : AtawDbContext
    {
        public virtual IQueryable<T> QueryDefault(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }

        public TContext DBContext
        {
            get
            {
                return DataContext as TContext;
            }
        }

        public Repository(AtawDbContext dbContext)
            : base(dbContext)
        {
        }

        protected void SetDefaultValue<TKey>(ref  TKey  val ,TKey newVal,TKey emptyVal = default(TKey))
        {
            if (!val.Equals( emptyVal))
            {
                val = newVal;
            }
        }
        

        public int Commit()
        {
            //ObjectContext objCont = ((IObjectContextAdapter)DataContext).ObjectContext;
            //return DataContext.SaveChanges();
            return DataContext.SaveChanges();
        }

        public string GetUniId()
        {
            // var naWenContext = DataContext as NaWenContext;
            if (DBContext != null)
                return DBContext.GetUniId();
            else
                throw new Exception("数据库实例错误");
        }


        public override void Add(T entity)
        {
            AtawBaseModel model = entity as AtawBaseModel;
            if (model != null)
            {
                if (model.FControlUnitID.IsEmpty())
                {
                    model.FControlUnitID = AtawAppContext.Current.FControlUnitID;
                }
                if (model.CREATE_ID.IsEmpty())
                {
                    model.CREATE_ID = AtawAppContext.Current.UserId;
                }
                if (model.CREATE_TIME == null)
                {
                    model.CREATE_TIME = DBContext.Now;
                }
                if (model.UPDATE_TIME == null)
                {
                    model.UPDATE_TIME = DBContext.Now;
                }
                if (model.UPDATE_ID.IsEmpty())
                {
                    model.UPDATE_ID = AtawAppContext.Current.UserId;
                }
            }
            RecordInsertLog(entity);

            base.Add(entity);
        }

        public virtual void RecordInsertLog(T entity)
        { 
        }
        public virtual void RecordUpdateLog(T entity)
        {
        }

        public override void Update(T entity)
        {
            AtawBaseModel model = entity as AtawBaseModel;
            if (model != null)
            {

                //if (model.UDDATETIME == null)
                //{
                    model.UPDATE_TIME = DBContext.Now;
               // }
                //if (model.UPDATE_ID.IsEmpty())
                //{
                    model.UPDATE_ID = AtawAppContext.Current.UserId;
               // }
            }
            RecordUpdateLog(entity);
            base.Update(entity);
        }

        public override void Edit(T entity)
        {
            AtawBaseModel model = entity as AtawBaseModel;
            if (model != null)
            {

                //if (model.UDDATETIME == null)
                //{
                model.UPDATE_TIME = DBContext.Now;
                // }
                //if (model.UPDATE_ID.IsEmpty())
                //{
                model.UPDATE_ID = AtawAppContext.Current.UserId;
                // }
            }
            RecordUpdateLog(entity);
            base.Edit(entity);
        }

    }
}
