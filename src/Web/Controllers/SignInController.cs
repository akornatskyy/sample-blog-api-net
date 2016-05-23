using System.Threading.Tasks;
using System.Web.Http;

using Blog.Web.Constants;
using Blog.Web.Facade;
using Blog.Web.Models;

namespace Blog.Web.Controllers
{
    [RoutePrefix(RoutePatterns.Prefix)]
    public class SignInController : Infrastructure.ApiController
    {
        [Route(RoutePatterns.SignIn)]
        public async Task<IHttpActionResult> Post([FromBody]SignInRequest req)
        {
            if (req == null)
            {
                return this.BadRequest();
            }

            var resp = await this.GetService<SignInFacade>().Authenticate(req);
            if (resp == null)
            {
                return this.BadRequest();
            }

            return this.Ok(resp);
        }
    }
}