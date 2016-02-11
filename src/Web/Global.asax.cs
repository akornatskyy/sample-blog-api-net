﻿using System.Web.Http;
using System.Web.Routing;

namespace Blog.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();  
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
