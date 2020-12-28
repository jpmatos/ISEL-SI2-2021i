namespace Connection
{
    public interface IMapper<T, in TR>
    {
        void Create(T entity);
        T Read(TR id);
        T[] Read(string condition);
        void Update(T entity);
        void Delete(T entity);
    }
}