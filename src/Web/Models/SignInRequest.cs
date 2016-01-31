using System.ComponentModel.DataAnnotations;

using Blog.Web.Properties;

namespace Blog.Web.Models
{
    public sealed class SignInRequest
    {
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [MinLength(2, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MinLength")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MaxLength")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [MinLength(8, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MinLength")]
        [MaxLength(12, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MaxLength")]
        public string Password { get; set; }
    }
}