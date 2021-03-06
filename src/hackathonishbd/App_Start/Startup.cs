﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using hackathonishbd;
using hackathonishbd.Models;
using hackathonishbd.Models.Identity;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace hackathonishbd
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new UserManager(new NHibernateHelper().Users));
            app.CreatePerOwinContext<SignInManager>((options, context) => new SignInManager(context.GetUserManager<UserManager>(), context.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider()
            });
        }
    }
}