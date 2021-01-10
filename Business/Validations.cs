using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class Validations
    {
        public static void ValidarExpressaoCron(string expressao, string mensagem)
        {
            if (CrontabSchedule.TryParse(expressao) == null)
            {
                throw new InvalidOperationException(mensagem);
            }
        }

        public static void ValidarTimeZoneId(string timeZone, string mensagem)
        {
            var allTimeZones = new HashSet<string>(TimeZoneInfo.GetSystemTimeZones()
                                    .Select(t => t.Id));

            if (!allTimeZones.Contains(timeZone))
            {
                throw new InvalidOperationException(mensagem);
            }
        }

        public static void ValidarNomeQueue(string queue, string mensagem)
        {

        }
    }
}
