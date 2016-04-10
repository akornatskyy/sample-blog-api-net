using System.Net.Http;

namespace Blog.Web.Infrastructure
{
    public class ApiController : System.Web.Http.ApiController
    {
        protected T GetService<T>()
        {
            return (T)this.Request.GetDependencyScope().GetService(typeof(T));
        }
    }
}