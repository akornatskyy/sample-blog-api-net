using System.Web.Http.ModelBinding;

namespace Blog.Web.Integration
{
    public sealed class ModelStateAccessor
    {
        public ModelStateDictionary ModelState { get; set; }
    }
}