using System.Collections.Generic;

namespace Auditor
{
    public abstract class Schedules
    {
        //TODO change schedules names according to requirements

        public const string Audit5sLpaProduction = "schedule_5s_lpa_prod";
        public const string Audit5sLpaSV = "schedule_5s_lpa_sv";
        public const string Audit5sAdministration = "schedule_5s_admin";
        public static List<string> SchedulesList = new List<string>() { Audit5sLpaProduction, Audit5sLpaSV, Audit5sAdministration };
    }
}