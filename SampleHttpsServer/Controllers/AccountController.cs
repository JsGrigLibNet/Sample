namespace SampleHttpsServer
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;

    [RoutePrefix("account"), AllowAnonymous]
    public class AccountController : ApiController
    {
      
        [HttpGet, Route("login")]
        public IHttpActionResult Login(string returnUrl)
        {

            if (!this.User?.Identity?.IsAuthenticated?? true)
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
            return this.Ok();
        }
    }
}