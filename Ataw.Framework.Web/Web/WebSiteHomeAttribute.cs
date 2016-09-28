using System;
namespace Ataw.Framework.Web
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class WebSiteHomeAttribute : Attribute
    {
        public int Order
        {
            get;
            private set;
        }

        public WebSiteHomeAttribute(int order)
        {
            Order = order;
        }
    }
}
