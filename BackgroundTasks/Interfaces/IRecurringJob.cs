using Hangfire.Server;
using Hangfire.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMedium.BackgroundTasks.Interfaces
{
    public interface IRecurringJob
    {
        Task Execute(PerformContext context);
        Task<RecurringJobDto> ConfigureJob(IServiceProvider serviceProvider);
    }
}
