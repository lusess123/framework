using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    interface IQueryResolver<T> where T : class
    {
        ICollection<T> QueryByParams(ICollection<KeyValueItem> queryParams, IUnitOfData dbContext);
    }
}
