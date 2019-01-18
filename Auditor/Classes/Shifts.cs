using System.Collections.Generic;

namespace Auditor
{
    public abstract class Shifts
    {
        public const string A = "A";
        public const string B = "B";
        public const string C = "C";
        public static List<string> ShiftsList = new List<string>() { A, B, C };
    }
}