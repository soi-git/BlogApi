namespace DataAccessLayer.Interfaces
{
    public interface IBlogBaseRepository<T> where T : class
    {
        IQueryable<T>? GetAll();
        T? Get(int id);
        int Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
