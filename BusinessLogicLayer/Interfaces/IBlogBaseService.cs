namespace BusinessLogicLayer.Interfaces
{
    public interface IBlogBaseService<T> where T : class
    {
        T? Get(int id);
        List<T>? GetAll();
        int Create(T item);
        void Delete(int id);
        void Update(T item);
    }
}
