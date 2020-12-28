using System;
using System.Diagnostics;
using IMapper;
using Mapper;
using Microsoft.Data.SqlClient;

namespace Controller.ADO
{
    public static class ObtainNextCode
    {
        public static string Execute(string option)
        {
            using var s = new Session();
            bool isMyConnection = false;
            try
            {
                isMyConnection = s.OpenConnection();
                switch (option)
                {
                    case "invoice":
                        IMapperInvoice iMap = s.CreateMapperInvoice();
                        Entity.Invoice[] invoices = iMap.Read("YEAR(Invoice.creation_date) = YEAR(GETDATE())");
                        return "FT" + DateTime.Now.Year + "-" + (invoices.Length + 1);
                    case "creditnote":
                        IMapperCreditNote cnMap = s.CreateCreditNote();
                        Entity.CreditNote[] creditNotes =
                            cnMap.Read("YEAR(CreditNote.creation_date) = YEAR(GETDATE())");
                        return "NT" + DateTime.Now.Year + "-" + (creditNotes.Length + 1);
                        ;
                    default:
                        Debug.WriteLine($"Invalid option '{option}'");
                        return "INVALID";
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
            finally
            {
                s.CloseConnection(isMyConnection);
            }
        }
    }
}