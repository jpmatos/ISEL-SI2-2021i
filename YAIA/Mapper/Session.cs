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

        public IMapperInvoice CreateMapperInvoice()
        {
            return new MapperInvoice(this);
        }

        public IMapperCreditNote CreateCreditNote()
        {
            return new MapperCreditNote(this);
        }
    }
}