using System.Data;
using System.Data.SqlClient;

namespace AFC.Common
{
    public class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AFC");
        }

        // Method to read data (Select) into a DataTable
        public DataTable ExecuteReader(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        connection.Open();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public DataTable ExecuteProcdure(string procName, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(procName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        connection.Open();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public (DataTable DeskTypes, DataTable Products) ExecuteProcedureWithMultipleResults(string procName, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(procName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Manually construct and fill the first result set (DeskTypes)
                    DataTable deskTypes = new DataTable();
                    deskTypes.Columns.Add("Id", typeof(int));
                    deskTypes.Columns.Add("Name", typeof(string));
                    deskTypes.Columns.Add("SEO_Slug", typeof(string));
                    deskTypes.Columns.Add("ImageUrl", typeof(string));

                    while (reader.Read())
                    {
                        deskTypes.Rows.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    }

                    // Move to the second result
                    DataTable products = new DataTable();
                    if (reader.NextResult())
                    {
                        products.Columns.Add("Id", typeof(int));
                        products.Columns.Add("Name", typeof(string));
                        products.Columns.Add("SEO_Slug", typeof(string));
                        products.Columns.Add("ImageUrl", typeof(string));
                        products.Columns.Add("SKU", typeof(string));
                        products.Columns.Add("DeskTypeSlug", typeof(string));
                        products.Columns.Add("DeskTypeName", typeof(string));
                        products.Columns.Add("DeskTypeImageUrl", typeof(string));

                        while (reader.Read())
                        {
                            products.Rows.Add(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.IsDBNull(3) ? null : reader.GetString(3),
                                reader.IsDBNull(4) ? null : reader.GetString(4),
                                reader.GetString(5),
                                reader.GetString(6),
                                reader.GetString(7)
                            );
                        }
                    }

                    return (deskTypes, products);
                }
            }
        }

        /// <summary>
        /// Executes a query (INSERT, UPDATE, DELETE) and returns the number of affected rows.
        /// </summary>
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        connection.Open();
                        return command.ExecuteNonQuery(); // Returns number of affected rows
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Executes a scalar query (SELECT COUNT, MAX, MIN, etc.) and returns the result as an object.
        /// </summary>
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return command.ExecuteScalar(); // Returns a single value (first column of first row)
                }
            }


        }
    }
    }
