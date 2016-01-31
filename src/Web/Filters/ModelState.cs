using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

using Blog.Web.Integration;
using Blog.Web.Utils;

namespace Blog.Web.Filters
{
    public class ModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var s = actionContext.ModelState;
            if (!s.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                        HttpStatusCode.BadRequest, TranslateModelState(s));
                return;
            }
            
            var accessor = (ModelStateAccessor)actionContext.Request.GetDependencyScope().GetService(typeof(ModelStateAccessor));
            accessor.ModelState = actionContext.ModelState;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var s = actionExecutedContext.ActionContext.ModelState;
            if (!s.IsValid)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                        HttpStatusCode.BadRequest, TranslateModelState(actionExecutedContext.ActionContext.ModelState));
            }
        }

        private static IDictionary<string, string[]> TranslateModelState(ModelStateDictionary modelState)
        {
            var result = new Dictionary<string, string[]>();
            foreach (var current in modelState)
            {
                var errors = current.Value.Errors;
                if (errors == null || errors.Count == 0)
                {
                    continue;
                }

                var key = current.Key.SkipOneDot().ToCamelCase();
                result.Add(key, errors.Select(e => e.ErrorMessage).ToArray());
            }

            return result;
        }
    }
}