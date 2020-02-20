using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary {
    public class StudentController {
        public static BcConnection bcConnection { get; set; }

        public static List<Student> GetAllStudents() {
            var sql = "SELECT * From Student s Left Join Major m on m.Id = s.MajorId";
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
                if(Convert.IsDBNull(reader["Description"])) {
                    student.Major = null;
                } else {
                    var major = new Major {
                        Description = reader["Description"].ToString(),
                        MinSat = Convert.ToInt32(reader["MinSat"])
                    };
                    student.Major = major;
                }
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
                reader.Close();
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

        public static bool InsertStudent(Student student) {
            var majorId = "";
            if (student.MajorId == null) {
                majorId = "NULL";
            } else {
                majorId = student.MajorId.ToString();
            } 
            //var sql = $"INSERT into Student (Id, FirstName, LastName, SAT, GPA, MajorId) VALUES ({student.Id}, '{student.FirstName}', '{student.LastName}', {student.SAT}, {student.GPA}, {majorId})";
            //Using string interpolation for sql statements is not good practice. easy to change and hack

            var sql = $"INSERT into Student (Id, FirstName, LastName, SAT, GPA, MajorId) VALUES (@Id, @FirstName, @LastName, @SAT, @GPA, @MajorId); ";
            
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@SAT", student.SAT);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@MajorId", student.MajorId ?? Convert.DBNull);
            //Preferred method, even though it uses more space 
            var recsAffected = command.ExecuteNonQuery();
            if(recsAffected != 1) {
                throw new Exception("Insert failed");
            }
            return true;
        }

        public static bool UpdateStudent(Student student) {
            var sql = "UPDATE Student Set" + " FirstName = @FirstName, LastName = @LastName, SAT = @SAT, GPA = @GPA, MajorId = @MajorId Where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@SAT", student.SAT);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@MajorId", student.MajorId ?? Convert.DBNull);
            //Preferred method, even though it uses more space 
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Update failed");
            }
            return true;

        }

        public static bool DeleteStudent(Student student) {
            var sql = "DELETE from Student Where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", student.Id);

            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Delete failed");
            }
            return true;
        }
        public static bool DeleteStudent(int id) {
            var std = GetByPk(id);
            if(std == null) {
                return false;
            }
            var success = DeleteStudent(std);
            return true;
        }
    }
}
