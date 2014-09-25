using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Impl.ScheduleJobs;
using Tmc.Core.Infrastructure;

namespace Tmc.Web.Framework.Infrastructure
{
    public class BackgroundStartupTask : IStartupTask
    {
        public void Execute()
        {
            //var cronScheduleExpression = " 	0 0/1 * 1/1 * ? *";

            ////var scheduler = EngineContext.Current.ContainerManager.Resolve<IScheduler>();

            ////IJobDetail job = JobBuilder.Create<UpdateCardTypeJob>()
            ////    .WithIdentity("UpdateCardTypeJob").Build();
            ////ITrigger trigger = TriggerBuilder.Create()
            ////    .WithIdentity("UpdateCardTypeJobTrigger")
            ////    .WithCronSchedule(cronScheduleExpression)
            ////    .Build();

            //using(var lifeTime = EngineContext.Current.ContainerManager.Container.BeginLifetimeScope())
            //{
            //    EngineContext.Current.ContainerManager.Resolve<IScheduler>("", lifeTime);
            //}
            ////scheduler.ScheduleJob(job, trigger);
            //var scheduler = EngineContext.Current.ContainerManager.Resolve<IScheduler>();
            //var job = JobBuilder.Create<UpdateCardTypeJob>()
            //                .WithIdentity("UpdateCardTypeJob", "UpdateCardTypeJobGroup")
            //                .Build();                   // #4

            //var trigger = TriggerBuilder.Create()
            //                            .WithIdentity("SampleTrigger", "SampleWindowsService")
            //                            .WithCronSchedule(cronScheduleExpression)   // #5
            //                            .ForJob("UpdateCardTypeJob", "UpdateCardTypeJobGroup")
            //                            .Build();           // #6

            //this.Scheduler.JobFactory = this.JobFactory;    // #7
            //this.Scheduler.ScheduleJob(job, trigger);       // #8
            //this.Scheduler.ListenerManager.AddJobListener(this.JobListener);    // #9
            //this.Scheduler.Start();                         // #10

        }

        public int Order
        {
            get { return 0; }
        }
    }
}
