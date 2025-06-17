using System.Collections.Generic;
using System.Data.SqlClient;
using AFC.Models;

namespace AFC.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly string _connectionString;
        private readonly IGenericRepository<Polecart> _polecartRepository;
        public GenericRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AFC");
        }

        public List<T> GetAll(string query)
        {
            List<T> records = new List<T>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T record = Activator.CreateInstance<T>();

                        foreach (var prop in typeof(T).GetProperties())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                            {
                                prop.SetValue(record, reader[prop.Name]);
                            }
                        }

                        records.Add(record);
                    }
                }
            }
            return records;
        }

        public void Add(string query, Dictionary<string, object> parameters)
        {
            ExecuteNonQuery(query, parameters);
        }

        public void Update(string query, Dictionary<string, object> parameters)
        {
            ExecuteNonQuery(query, parameters);
        }

        public void Delete(string query, Dictionary<string, object> parameters)
        {
            ExecuteNonQuery(query, parameters);
        }

        private void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);

                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
