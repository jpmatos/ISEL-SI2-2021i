using System;
using System.Collections.Generic;
using Entity;
using IMapper;
using Microsoft.Data.SqlClient;

namespace Mapper
{
    public class MapperCreditNote : IMapperCreditNote
    {
        private readonly IMapperSession _mySession;
        private bool _isMyConnection;

        private SqlCommand CreateCommand()
        {
            return _mySession.CreateCommand();
        }

        public MapperCreditNote(IMapperSession s)
        {
            _mySession = s;
        }

        public void Create(CreditNote entity)
        {
            throw new System.NotImplementedException();
        }

        public CreditNote Read(int id)
        {
            throw new System.NotImplementedException();
        }

        public CreditNote[] Read(string condition)
        {
            List<CreditNote> res = new List<CreditNote>();
            _isMyConnection = _mySession.OpenConnection();

            SqlCommand cmd = CreateCommand();
            cmd.CommandText = $"SELECT * FROM CreditNote WHERE {condition}";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CreditNote creditNote = new CreditNote();
                creditNote.Code = reader[0] != DBNull.Value ? (string) reader[0] : null;
                creditNote.CodeInvoice = reader[1] != DBNull.Value ? (string) reader[1] : null;
                creditNote.State = reader[2] != DBNull.Value ? (string) reader[2] : null;
                creditNote.TotalValue = reader[3] != DBNull.Value ? (decimal) reader[3] : 0;
                creditNote.TotalIva = reader[4] != DBNull.Value ? (decimal) reader[4] : 0;
                creditNote.CreationDate = reader[5] != DBNull.Value ? (DateTime) reader[5] : DateTime.MinValue;
                creditNote.EmissionDate = reader[6] != DBNull.Value ? (DateTime) reader[6] : DateTime.MinValue;
                res.Add(creditNote);
            }

            return res.ToArray();
        }

        public void Update(CreditNote entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(CreditNote entity)
        {
            throw new System.NotImplementedException();
        }
    }
}