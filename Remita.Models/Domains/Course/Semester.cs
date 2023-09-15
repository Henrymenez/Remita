using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remita.Models.Domains.Course
{
    public enum Semester
    {
        first = 1,
        second
    }
    public static class SemesterTypeExtension
    {
        public static string? GetStringValue(this Semester semester)
        {
            return semester switch
            {
                Semester.first => "first",
                Semester.second => "second",
                _ => null
            };
        }

        public static Semester GetSemester(string semester)
        {
            return semester switch
            {
                "first" => Semester.first,
                "second" => Semester.second
            };
        }
    }
}
