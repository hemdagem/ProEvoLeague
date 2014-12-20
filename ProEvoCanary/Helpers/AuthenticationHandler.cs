﻿using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
        public void SignIn(UserModel login)
        {
            var identity = ClaimsIdentityFactory.Create(login);
            IOwinContext ctx = HttpContext.Current.Request.GetOwinContext();
            IAuthenticationManager authManager = ctx.Authentication;
            authManager.SignIn(identity);
        }
    }
}