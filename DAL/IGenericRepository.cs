namespace AFC.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll(string query);
        void Add(string query, Dictionary<string, object> parameters);
        void Update(string query, Dictionary<string, object> parameters);
        void Delete(string query, Dictionary<string, object> parameters);
    }
}
