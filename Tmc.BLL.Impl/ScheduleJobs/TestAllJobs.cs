using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.BLL.Impl.ScheduleJobs
{
    public class TestAllJobs
    {
        private readonly IScheduler _scheduler;
        private readonly IJobFactory _jobFactory;

        public TestAllJobs(IScheduler scheduler, IJobFactory jobFactory)
        {
            this._scheduler = scheduler;
            this._jobFactory = jobFactory;
        }
        public void Start()
        {
            _scheduler.Start();

            JobDetailImpl jobDetail = new JobDetailImpl("1Job", null, typeof(UpdateCardTypeJob));
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("1JobTrigger")
                .WithSimpleSchedule(x => x.RepeatForever().WithInterval(new TimeSpan(0, 0, 1, 0, 0))).StartNow().Build();
            _scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
