using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EnterpriseSchedulerService.Startup))]

namespace EnterpriseSchedulerService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

            app.UseErrorPage();
            app.UseWelcomePage("/welcome");


            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 1 });
            GlobalConfiguration.Configuration.UseSqlServerStorage(
                    connectionString,
                    new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) })
                .UseDashboardMetric(SqlServerStorage.TotalConnections)
                .UseDashboardMetric(DashboardMetrics.ProcessingCount)
                .UseDashboardMetric(DashboardMetrics.SucceededCount)
                .UseDashboardMetric(DashboardMetrics.FailedCount)
                .UseDashboardMetric(new DashboardMetric("内存", "内存", page =>
                {
                    var process = Process.GetCurrentProcess();
                    long memory = process.WorkingSet64 / 1024;
                    return new Metric($"{memory:N0}KB") { Style = memory > 512000 ? MetricStyle.Info : MetricStyle.Default };
                }))
                .UseDashboardMetric(new DashboardMetric("内存峰值", "(内存)峰值", page =>
                {
                    var process = Process.GetCurrentProcess();
                    long memory = process.PeakWorkingSet64 / 1024;
                    return new Metric($"{memory:N0}KB") { Style = memory > 512000 ? MetricStyle.Info : MetricStyle.Default };
                }))
                ;

            var options = new DashboardOptions
            {
                Authorization = new[] { new MyRestrictiveAuthorizationFilter() },
                AppPath = ConfigurationManager.AppSettings["AppPath"] ?? ""
            };
            app.UseHangfireDashboard("/Dashboard", options);

            //app.UseHangfireDashboard();
            app.UseHangfireServer();//new BackgroundJobServerOptions { WorkerCount = 1 });

            //RecurringJob.AddOrUpdate(
            //    () => Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Recurring job completed successfully!"),
            //    Cron.Minutely, TimeZoneInfo.Local);

            BackgroundJob.Enqueue(() => SucessTask("1"));

            //RecurringJob.AddOrUpdate("正确任务1", () => SucessTask("1"), Cron.Minutely, TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate("正确任务2", () => SucessTask("2"), Cron.Minutely, TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate("异常任务", () => FailedTask("异常任务,也是开始"), Cron.Minutely, TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate("等待任务", () => AwaitTask("等待任务,等啊等"), Cron.Minutely, TimeZoneInfo.Local);
        }

        private int count = 0;
        //[DisableConcurrentExecution(2 * 60 * 1000 + 1)]
        [DisplayName("发送订单 [#{0}] 到仓库")]
        public bool SucessTask(string str)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {str}任务开始! count = {count}");
            Thread.Sleep(2 * 60 * 1000);
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {str}任务结束! count = {count}");
            count++;
            BackgroundJob.Enqueue(() => SucessTask(count.ToString()));
            Thread.Sleep(10 * 1000);
            return false;

        }

        [AutomaticRetry(Attempts = 1)]
        public void FailedTask(string str)
        {
            Console.WriteLine("异常任务!");
            int i = 0;
            i++;
            Console.WriteLine(str);
            Console.WriteLine(i.ToString());
            throw new ArgumentNullException($"你的这个参数非常的错误,很错误!!");
        }


        public void AwaitTask(string str)
        {
            Console.WriteLine("任务等待一分钟!");

            int i = 0;
            i++;
            Console.WriteLine(str);
            Thread.Sleep(60000);
            Console.WriteLine(i.ToString());
        }



    }


    public class MyRestrictiveAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
