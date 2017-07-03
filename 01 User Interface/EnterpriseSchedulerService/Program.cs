using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;



namespace EnterpriseSchedulerService
{
    class Program
    {
        static void Main()
        {
            //LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());
            //var endPoint = ConfigurationManager.AppSettings["EndPoint"];
            //using (WebApp.Start<Startup>(endPoint))
            //{
            //    Console.WriteLine();
            //    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Hangfire Server started.");
            //    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Dashboard is available at {endPoint}/Dashboard");
            //    Console.WriteLine();
            //    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Type JOB to add a background job.");
            //    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Press ENTER to exit...");


            //    string command;
            //    while ((command = Console.ReadLine()) != String.Empty)
            //    {
            //        if ("job".Equals(command, StringComparison.OrdinalIgnoreCase))
            //            BackgroundJob.Enqueue(() => Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Background job completed successfully!"));
            //    }
            //}
        }

    }
}
