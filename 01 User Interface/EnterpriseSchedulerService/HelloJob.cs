using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Common.Logging;

namespace EnterpriseSchedulerService
{
    public class HelloJob : IJob
    {
        public HelloJob()
        {
            Console.WriteLine("Create Jog!come on  baby");


        }

        public HelloJob(string job)
        {
            Console.WriteLine("Create Jog! no params");
        }
        public void Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.MergedJobDataMap;
            string jobSays = dataMap.GetString("jobSays");
            var myFloatValue = dataMap.GetFloat("myFloatValue");
            var myStateData = dataMap["myStateData"];
            Console.Error.WriteLine("Instance " + key + " of DumbJob says: " + jobSays + ", and val is: " + myFloatValue + $",myStateData is :{myStateData}");
        }
    }
}
