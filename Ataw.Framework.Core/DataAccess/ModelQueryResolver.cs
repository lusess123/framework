using System.Collections.Generic;
namespace Ataw.Framework.Core
{

    public abstract class ModelQueryResolver<T> : IQueryResolver<T> where T : class
    {

        public ICollection<T> QueryByParams(ICollection<KeyValueItem> queryParams, IUnitOfData dbContext)
        {
            return new List<T>();
        }
    }
}
