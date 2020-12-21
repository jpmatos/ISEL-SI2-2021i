using Connection;
using IMapper;

namespace Mapper
{
    public class Session : AbstractSession, IMapperSession
    {
        public IMapperContributor CreateMapperContributor()
        {
            return new MapperContributor(this);
        }
    }
}