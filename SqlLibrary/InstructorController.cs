using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SqlLibrary {
    public class InstructorController {
        public static BcConnection bcConnection { get; set; }

        public static List<Instructor> GetAllInstructors() {
            var sql = "SELECT * from Instructor;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                Console.WriteLine("No Instructors found");
                reader.Close();
                reader = null;
                return new List<Instructor>();
            }
            var instructors = new List<Instructor>();
            while (reader.Read()) {
                Instructor instructor = IPullAndFeed(reader);
                instructors.Add(instructor);
            }
            reader.Close();
            reader = null;
            return instructors;
        }

        private static Instructor IPullAndFeed(SqlDataReader reader) {
            return new Instructor {
                Id = Convert.ToInt32(reader["Id"]),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                YearsExperience = Convert.ToInt32(reader["YearsExperience"]),
                IsTenured = Convert.ToBoolean(reader["IsTenured"])
            };
        }

        
        public static Instructor GetByPk(int id) {
            var sql = "SELECT * from Instructor where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", 30);
            var reader = command.ExecuteReader();
            while (!reader.HasRows) {
                Console.WriteLine("Instructor not found");
                reader.Close();
                return null;
            }
            reader.Read();
            var instructor = IPullAndFeed(reader);
            //var instructor = new Instructor {
            //    Id = Convert.ToInt32(reader["Id"]),
            //    FirstName = reader["FirstName"].ToString(),
            //    LastName = reader["LastName"].ToString(),
            //    YearsExperience = Convert.ToInt32(reader["YearsExperience"]),
            //    IsTenured = Convert.ToBoolean(reader["IsTenured"])
            //};
            reader.Close();
            reader = null;
            return instructor;
        }
    }
}
