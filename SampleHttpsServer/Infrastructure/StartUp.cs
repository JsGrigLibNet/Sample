using Microsoft.Owin;
using SampleHttpsServer.Infrastructure;

[assembly: OwinStartup(typeof(StartUp))]

namespace SampleHttpsServer.Infrastructure
{
    using System;
    using System.Net.Http.Formatting;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.Routing;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.Google;
    using Microsoft.Owin.StaticFiles;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Owin;
    using SampleHttpsServer;
    using SlickProxyLib;

    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            SetUpGeneralCookieAuthentication(app);
            SetUpGoogleAuthentication(app);

            app.UseSlickProxy(handle => { });

            string uiFolder = AppDomain.CurrentDomain.BaseDirectory + "/../../public";
            var fileSystem = new PhysicalFileSystem(uiFolder);
            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                FileSystem = fileSystem,
                EnableDefaultFiles = true
            };
            app.UseFileServer(options);

            SetUpWebApi(app);
        }

        private static void SetUpGeneralCookieAuthentication(IAppBuilder app)
        {
            var cookieOpts = new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/account/login"),
                CookieSecure = CookieSecureOption.SameAsRequest,
                ExpireTimeSpan = System.TimeSpan.FromMinutes(30),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            };

            app.UseCookieAuthentication(cookieOpts);

            app.SetDefaultSignInAsAuthenticationType(cookieOpts.AuthenticationType);
        }

        private static void SetUpGoogleAuthentication(IAppBuilder app)
        {
            var googleOpts = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "xxxxxxx",
                ClientSecret = "xxxxx",
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        var profileUrl = context.User["image"]["url"].ToString();
                        context.Identity.AddClaim(new Claim(ClaimTypes.Uri, profileUrl));
                        return Task.FromResult(0);
                    }
                },
                CallbackPath = new PathString("/callback"),
            };
            googleOpts.Scope.Add("email");

            app.UseGoogleAuthentication(googleOpts);
        }

        private static void SetUpWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Services.Replace(typeof(IHttpControllerTypeResolver), new ControllerResolver());

            config.MapHttpAttributeRoutes();
            config.Routes.IgnoreRoute("elmah", "{resource}.axd/{*pathInfo}");
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute("FilesRoute", "{*pathInfo}", null, null, new StopRoutingHandler());

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.UseDataContractJsonSerializer = false; // defaults to false, but no harm done
            jsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            jsonFormatter.SerializerSettings.Formatting = Formatting.None;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            app.UseWebApi(config);
        }
    }
}