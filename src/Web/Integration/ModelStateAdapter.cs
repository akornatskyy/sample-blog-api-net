using System.Diagnostics;

using Blog.Service.Interface;

namespace Blog.Web.Integration
{
    public sealed class ModelStateAdapter : IErrorState
    {
        private readonly ModelStateAccessor accessor;

        public ModelStateAdapter(ModelStateAccessor container)
        {
            this.accessor = container;
        }

        public bool HasErrors
        {
            get
            {
                return this.accessor.ModelState != null && !this.accessor.ModelState.IsValid;
            }
        }

        public void AddError(string message)
        {
            Trace.Assert(this.accessor.ModelState != null);
            this.accessor.ModelState.AddModelError("__ERROR__", message);
        }

        public void AddError(string key, string message)
        {
            Trace.Assert(this.accessor.ModelState != null);
            this.accessor.ModelState.AddModelError(key, message);
        }
    }
}