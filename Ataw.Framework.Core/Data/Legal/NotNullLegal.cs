
using System;
namespace Ataw.Framework.Core
{
    [CodePlug("notNull", BaseClass = typeof(BaseLegal),
    CreateDate = "2013-04-18", Author = "zhengyk", Description = "非空验证")]
    public class NotNullLegal : BaseLegal
    {
        protected override bool Legal(object value)
        {
            return !(value == null || value == DBNull.Value || value.ToString().IsEmpty());
            // throw new NotImplementedException();
        }
    }
}
