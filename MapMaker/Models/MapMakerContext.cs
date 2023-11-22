using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MapMaker.Models
{
    public class MapMakerContext
    {
        public string ConnectionString { get; set; }

        public MapMakerContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Test> GetAllTests()
        {
            List<Test> list = new List<Test>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Test", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Test()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
