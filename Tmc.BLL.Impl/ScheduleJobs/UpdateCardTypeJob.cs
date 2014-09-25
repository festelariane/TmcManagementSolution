using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.BLL.Impl.ScheduleJobs
{
    public class UpdateCardTypeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Update cardtype job: started");
        }
    }
}
