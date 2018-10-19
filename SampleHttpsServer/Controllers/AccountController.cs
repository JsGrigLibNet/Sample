namespace SampleHttpsServer
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Web.Http;
    using SampleHttpsServer.Controllers;

    [RoutePrefix("account"), AllowAnonymous]
    public class AccountController : ApiController
    {
        [HttpGet, Route("app")]
        public AppData App()
        {
            return new AppData()
            {
                Name = this?.User?.Identity?.Name ?? "",
                IsAuthenticated = this.User?.Identity?.IsAuthenticated,
                Links= GridController.Links,
                ApplicationName = GridController.ApplicationName,
                ApplicationTitle = GridController.ApplicationTitle,
                CompanyName = GridController.CompanyName,
                Year= DateTime.UtcNow.Year
            };

        }
        [HttpGet, Route("login")]
        public IHttpActionResult Login(string returnUrl= null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (!this.User?.Identity?.IsAuthenticated ?? true)
            {
                var authProps = new AuthenticationProperties
                {
                    RedirectUri = returnUrl
                };
                this.Request.GetOwinContext().Authentication.Challenge(authProps, "Google");
                return this.StatusCode(HttpStatusCode.Unauthorized);
            }
            else
            {
                return this.Ok();
            }
        }

        [HttpGet, Route("loginNoDb")]
        public IHttpActionResult LoginNoDb(string email, string password)
        {
            try
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, email.Split('@')[0]));
                claims.Add(new Claim(ClaimTypes.Email, email));
                var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = this.Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(id);
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.StatusCode(HttpStatusCode.Unauthorized);
            }
        }

        [HttpGet, Route("logoff")]
        public IHttpActionResult Logout()
        {
            this.Request.GetOwinContext().Authentication.SignOut();
            return Redirect(Url.Content("~/"));
        }
    }
}