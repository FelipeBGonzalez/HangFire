using Business.Models;
using Hangfire.Server;
using Hangfire.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjetoMedium.BackgroundTasks.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMedium.BackgroundTasks.Tasks
{
    public class CalculationRecurringJob : IRecurringJob
    {
        public async Task<RecurringJobDto> ConfigureJob(IServiceProvider serviceProvider)
        {
            List<ParametrizacaoJob> listaParametrizacaoJob = new List<ParametrizacaoJob>();
            string template = Path.Combine(Directory.GetCurrentDirectory(), "Tasks") + "\\Jobs.json";            

            using (StreamReader r = new StreamReader(template))
            {
                var json2 = r.ReadToEnd();
                listaParametrizacaoJob = JsonConvert.DeserializeObject<List<ParametrizacaoJob>>(json2);                
            }
            
            ParametrizacaoJob parametrizacaoJob  = listaParametrizacaoJob.FirstOrDefault(x => x.Name == "Calculation");
            return await Task.FromResult<RecurringJobDto>(new RecurringJobDto { Id = parametrizacaoJob.Name, Cron = parametrizacaoJob.Cron, TimeZoneId = parametrizacaoJob.TimeZone, Queue = parametrizacaoJob.Fila });
        }

        public async Task Execute(PerformContext context)
        {
            int resultado = 0;
            resultado = 1 + 1;
        }
    }
}
