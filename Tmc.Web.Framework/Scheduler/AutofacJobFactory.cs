using Autofac;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Web.Framework.Scheduler
{
    public class AutofacJobFactory : IJobFactory
    {
        private readonly IContainer _container;
        public AutofacJobFactory(IContainer container)
        {
            this._container = container;
        }
        public IJob NewJob(TriggerFiredBundle bundle, Quartz.IScheduler scheduler)
        {
            return (IJob)_container.Resolve(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            
        }
    }
}
