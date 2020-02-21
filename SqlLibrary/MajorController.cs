using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SqlLibrary {
    public class MajorController {
        public static BcConnection bcConnection { get; set; }

        private static Major MPullAndFeed(SqlDataReader reader) {
            var major = new Major();
            major.Id = Convert.ToInt32(reader["Id"]);
            major.Description = (reader["Description"].ToString());
            major.MinSat = Convert.ToInt32(reader["MinSat"]);
            return major;
        }

        public static List<Major> GetAllMajors() {
            var sql = "SELECT * from Major;";
            var command = new SqlCommand(sql, bcConnection.Connection); //sets up connection
            var reader = command.ExecuteReader();
            if(!reader.HasRows) {
                reader.Close();
                reader = null;
                Console.WriteLine("No rows for GetAllMajors");
                return new List<Major>();
            }
            var majors = new List<Major>();
            while(reader.Read()) {
                var major = MPullAndFeed(reader);
                //var major = new Major();
                //major.Id = Convert.ToInt32(reader["Id"]);
                //major.Description = (reader["Description"].ToString());
                //major.MinSat = Convert.ToInt32(reader["MinSat"]);
                majors.Add(major);

            }
            reader.Close();
            reader = null;
            return majors;
        }

        public static Major GetByPk(int id) {
            var sql = "SELECT * from Major where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", 1);
            var reader = command.ExecuteReader();
            while (!reader.HasRows) {
                reader.Close();
                return null;
            }
            reader.Read();
            var major = MPullAndFeed(reader);
                //var major = new Major {
                //    Id = Convert.ToInt32(reader["Id"]),  //Pulls Id and etc. as objects, hence why they must be converted
                //    Description = reader["Description"].ToString(),
                //    MinSat = Convert.ToInt32(reader["MinSat"])
                //};
                reader.Close();
                reader = null;
                return major;
            
        }
    }
}
