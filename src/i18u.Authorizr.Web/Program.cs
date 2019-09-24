using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace i18u.Authorizr.Web
{
    /// <summary>
    /// The main entrypoint for this microservice.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entrypoint method for this microservice.
        /// </summary>
        /// <param name="args">The (unused) string arguments to provide when starting this application.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
