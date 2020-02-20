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
        public int MajorId { get; set; }

        public static BcConnection bcConnection { get; set; }

        public static List<Student> GetAllStudents() {
            var sql = "SELECT * From Student";
            var command = new SqlCommand(sql, bcConnection.Connection); //second connection is the sql connection inside Bc class
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                Console.WriteLine("No rows from GetAllStudents()");
                return new List<Student>();   //if reader doesn't have rows, return an empty list
            }
            var students = new List<Student>();
            while (reader.Read()) {
                var student = new Student();
                student.Id = Convert.ToInt32(reader["Id"]);
                student.FirstName = reader["FirstName"].ToString();
                student.LastName = reader["LastName"].ToString();
                student.SAT = Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToDouble(reader["GPA"]);
                //student.MajorId = Convert.ToInt32(reader["MajorId"]);
                students.Add(student);
            }
            return students;
        }
        public Student() {  }
        public Student(BcConnection connection) {
            bcConnection = connection;
        }
    }
}
