namespace SampleHttpsServer
{
    using Microsoft.Owin.Hosting;
    using System;
    using System.Diagnostics;
    using SampleHttpsServer.Infrastructure;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string httpLocalhost = "https://localhost:44302";
            string loginCheck = "https://localhost:44302/api/values/getall";
            string logout = "https://localhost:44302/account/logoff";
            using (WebApp.Start<StartUp>(httpLocalhost))
            {
                Console.WriteLine("Press [enter] to quit...");
                Process.Start(httpLocalhost);
                Console.ReadLine();
            }
        }
    }
}