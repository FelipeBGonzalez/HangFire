using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class ParametrizacaoJob
    {
        //public Guid Id { get;  set; }
        public string Name { get;  set; }
        public string Fila { get;  set; }
        public string TimeZone { get;  set; }
        public string Cron { get;  set; }      

        //public ParametrizacaoJob(string nome, string expressaoCron, string fila = "default", string timeZone = "E. South America Standard Time")
        //{
        //    Id = Guid.NewGuid();
        //    Nome = nome;
        //    ExpressaoCron = expressaoCron;            
        //    Fila = fila;
        //    TimeZone = timeZone;           

        //    Validar();
        //}

        //protected ParametrizacaoJob() { } //Used for EF

        public void AlterarExpressaoCron(string novaExpressao)
        {
            Cron = novaExpressao;
            Validations.ValidarExpressaoCron(Cron, $"A expressão CRON {Cron} não é válida");
        }
        
        private void Validar()
        {
            Validations.ValidarExpressaoCron(Cron, $"A expressão CRON {Cron} não é válida");
            Validations.ValidarTimeZoneId(TimeZone, $"TimeZone {TimeZone} não é válida");
        }
    }
}