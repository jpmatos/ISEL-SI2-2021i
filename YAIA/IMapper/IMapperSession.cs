using Connection;

namespace IMapper
{
    public interface IMapperSession : ISession
    {
        IMapperContributor CreateMapperContributor();
        IMapperInvoice CreateMapperInvoice();
        IMapperCreditNote CreateCreditNote();
    }
}