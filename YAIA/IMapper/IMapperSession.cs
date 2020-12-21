using Connection;

namespace IMapper
{
    public interface IMapperSession : ISession
    {
        IMapperContributor CreateMapperContributor();
    }
}