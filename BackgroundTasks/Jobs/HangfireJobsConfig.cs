using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.DependencyInjection;
using ProjetoMedium.BackgroundTasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProjetoMedium.BackgroundTasks.Jobs
{
    public class HangfireJobsConfig
    {
        private static ICollection<Type> RecurringJobsTypes;

        static HangfireJobsConfig()
        {
            RecurringJobsTypes = new List<Type>();
            LoadRecurringJobs();
        }

        public static async Task ConfigureHangfireJobs(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            foreach (var recurringJob in RecurringJobsTypes)
            {

                IRecurringJob job = (IRecurringJob)Activator.CreateInstance(recurringJob, true);

                var recurringJobDto = await job.ConfigureJob(services.BuildServiceProvider());

                RecurringJob.AddOrUpdate(recurringJobDto.Id, () => job.Execute(null), recurringJobDto.Cron, TimeZoneInfo.FindSystemTimeZoneById(recurringJobDto.TimeZoneId), recurringJobDto.Queue);
            }
        }

        private static void LoadRecurringJobs()
        {
            var jobsTypes = Assembly.GetEntryAssembly().GetTypes().Where(t => !t.IsAbstract && typeof(IRecurringJob).IsAssignableFrom(t));

            foreach (var type in jobsTypes)
            {
                var emptyConstructor = type.GetConstructor(BindingFlags.Instance |
                    BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);

                if (emptyConstructor == null)
                    throw new MissingMethodException(string.Format("Make sure [{0}] has a parameterless constructor", type.Name));

                RecurringJobsTypes.Add(type);
            }

        }
    }
}
