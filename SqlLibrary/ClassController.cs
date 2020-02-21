using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SqlLibrary {
    public class ClassController {
        public static BcConnection bcConnection { get; set; }

        private static Class CPullAndFeed(SqlDataReader reader) {
            return new Class {
                Id = Convert.ToInt32(reader["Id"]),
                Subject = reader["Subject"].ToString(),
                Section = Convert.ToInt32(reader["Section"]),
                InstructorId = Convert.IsDBNull(reader["InstructorId"])
            };
        }

        public static List<Class> GetAllClasses() {
            var sql = "SELECT * from Class;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                return new List<Class>();
            }
            var classes = new List<Class>();
            while (reader.Read()) {
                Class classSingular = CPullAndFeed(reader);
                classes.Add(classSingular);
            }
            reader.Close();
            reader = null;
            return classes;
        }

    }
}
