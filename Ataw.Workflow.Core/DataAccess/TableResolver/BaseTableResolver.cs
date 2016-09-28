using System;
using System.Data.Entity;
namespace Ataw.Workflow.Core.DataAccess
{
    public abstract class BaseTableResolver<T> : IDisposable where T : class
    {
        public DbContext DataContext
        {
            get;
            set;
        }

        public IDbSet<T> DbSet
        {
            get;
            set;
        }

        public void UpdateDatabase()
        {
            //DataContext.Database.s

        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
