using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum FrequencyEnum : short
    {
        [Description("1 Vez ao dia")]
        Qd = 1,
        [Description("1 Vez por semana")]
        TimeAweek = 2,
        [Description("1 Vez por mês")]
        Onceinamonth = 3,
        [Description("12 horas em 12 horas")]
        Hoursat12Oclock = 4,
        [Description("8 horas em 8 horas")]
        Hours8Hours = 5,
        [Description("6 horas em 6 horas")]
        Hoursin6Hours = 6,
        [Description("4 horas em 4 horas")]
        Hours4Hours = 7,
        [Description("Em caso de")]
        Incaseofa = 8
    }
}
