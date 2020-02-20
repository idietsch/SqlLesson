using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary {
    public class StudentController {
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
            reader.Close();
            reader = null;
            return students;
        }

        public static Student GetByPk(int id) {
            var sql = $"SELECT * from Student where Id = {id}";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                return null;
            }
            reader.Read();
            var student = new Student {
                Id = Convert.ToInt32(reader["Id"]),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                SAT = Convert.ToInt32(reader["SAT"]),  //changed from block above to show same thing done a little simpler
                GPA = Convert.ToDouble(reader["GPA"])
                //student.MajorId = Convert.ToInt32(reader["MajorId"]);
            };
            reader.Close();
            reader = null;
            return student;
        }
    }
}
