namespace Connection
{
    public interface IMapper <T, in TR>
    {
        void Create(T entity);
        T Read(TR id);
        void Update(T entity);
        void Delete(T entity);
    }
}