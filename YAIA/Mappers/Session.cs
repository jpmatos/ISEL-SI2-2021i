using Connection;
using IMappers;

namespace Mappers
{
    public class Session : AbstractSession, IMapperSession
    {
        public IMapperContributor CreateMapperContributor()
        {
            return new MapperContributor(this);
        }
    }
}