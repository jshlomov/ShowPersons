using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ShowPersons
{
    public class DBContext
    {
        public string _connectionString;

        public DBContext()
        {

            // SET SECRETS.JSON TO COPY ALWAYS AFTER ADDING!!
            IConfiguration builder = new ConfigurationBuilder()
                .AddJsonFile("secrets.json", optional: true) // Add secrets.json
                .Build();
            // Read a value from the configuration
            _connectionString = builder["ConnectionString"];
        }

        public DataTable MakeQuery(string query)
        {
            DataTable output = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(output);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return output;
        }

        public DataTable MakeQuery(string queryStr, SqlParameter[] parameters)
        {
            DataTable output = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(output);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                        // Further error handling
                    }
                }
            }
            return output;
        }
    }
}
