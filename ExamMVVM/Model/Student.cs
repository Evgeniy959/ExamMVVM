using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMVVM
{
    public class Student
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Group { get; set; }
        //public string Teacher { get; set; }
        //public Teacher Teacher { get; set; }
        public double AverageScore { get; set; }
        public int TeacherId { get; set; }
    }
}
