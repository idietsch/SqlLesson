using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary {
    public class Student {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SAT { get; set; }
        public double GPA { get; set; }
        public int? MajorId { get; set; }

        public override string ToString() {
            return $"{Id}|{FirstName} {LastName}|{SAT}|{GPA}";
        }
        public Student() {  }
    }
}
