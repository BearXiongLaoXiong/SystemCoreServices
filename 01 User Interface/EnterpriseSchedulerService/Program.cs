using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Simple;
using Quartz;
using Quartz.Impl;
using Quartz.Core;

namespace EnterpriseSchedulerService
{
    class Program
    {
        static void Main(string[] args)
        {
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter { Level = LogLevel.Info };

            ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetName().Name);
            
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("开始..........");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write("开始啦..........");
            Console.BackgroundColor = ConsoleColor.Black;
            logger.Info("开始..........");
            logger.Info("开始..........");
            logger.Info("开始..........");
            logger.Info("开始..........");

            try
            {
                Console.ReadLine();
                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .UsingJobData("jobSays","Hello World!!!")
                    .UsingJobData("myFloatValue",3.141F)
                    .UsingJobData(new JobDataMap())
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

                // Tell quartz to schedule the job using our trigger
                scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                Thread.Sleep(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }

    



    }


}
