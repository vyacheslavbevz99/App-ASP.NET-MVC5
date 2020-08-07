using System.Web;
using System.Web.Mvc;

namespace MVCApplication__CSharp_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
