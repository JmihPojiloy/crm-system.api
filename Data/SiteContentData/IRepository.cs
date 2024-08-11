namespace api.Data.SiteContentData
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string name);
        Task AddAsync(T value);
        Task UpdateAsync(T value);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}