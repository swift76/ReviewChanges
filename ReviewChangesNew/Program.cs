using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Windows.Forms;
using System.IO;

namespace ReviewChangesNew
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static IConfiguration Configuration;
        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            Settings appSettings = new Settings();
            Configuration.GetSection("Settings").Bind(appSettings);

            Dictionary<string, string> connectionStrings = new Dictionary<string, string>();
            connectionStrings.Add("test", Configuration.GetConnectionString("test"));
            connectionStrings.Add("real", Configuration.GetConnectionString("real"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin(appSettings, connectionStrings));
        }
    }
}
