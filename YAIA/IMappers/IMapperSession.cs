using Connection;

namespace IMappers
{
    public interface IMapperSession : ISession
    {
        IMapperContributor CreateMapperContributor();
    }
}