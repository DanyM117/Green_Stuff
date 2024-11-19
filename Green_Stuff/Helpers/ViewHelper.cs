using Green_Stuff.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Green_Stuff.Helpers
{
    public static class ViewHelper
    {
        public static DbLabpwebContext GetDbContext(this IHtmlHelper htmlHelper)
        {
            return (DbLabpwebContext)htmlHelper.ViewContext.HttpContext.RequestServices.GetService(typeof(DbLabpwebContext));
        }
    }
}
