using System;
using System.Collections.Generic;
using Entity;
using IMapper;
using Microsoft.Data.SqlClient;

namespace Mapper
{
    public class MapperInvoice : IMapperInvoice
    {
        private readonly IMapperSession _mySession;
        private bool _isMyConnection;
        
        private SqlCommand CreateCommand()
        {
            return _mySession.CreateCommand();
        }
        
        public MapperInvoice(IMapperSession s)
        {
            _mySession = s;
        }
        
        public void Create(Invoice entity)
        {
            throw new System.NotImplementedException();
        }

        public Invoice Read(int id)
        {
            throw new System.NotImplementedException();
        }

        public Invoice[] Read(string condition)
        {
            List<Invoice> res = new List<Invoice>();
            _isMyConnection = _mySession.OpenConnection();

            SqlCommand cmd = CreateCommand();
            cmd.CommandText = $"SELECT * FROM Invoice WHERE {condition}";
            
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Invoice invoice = new Invoice();
                invoice.Code = reader[0] != DBNull.Value ? (string) reader[0] : null;
                invoice.Nif = reader[1] != DBNull.Value ? (int) reader[1] : 0;
                invoice.State = reader[2] != DBNull.Value ? (string) reader[2] : null;
                invoice.TotalValue = reader[3] != DBNull.Value ? (decimal) reader[3] : 0;
                invoice.TotalIva = reader[4] != DBNull.Value ? (decimal) reader[4] : 0;
                invoice.CreationDate = reader[5] != DBNull.Value ? (DateTime) reader[5] : DateTime.MinValue;
                invoice.EmissionDate = reader[6] != DBNull.Value ? (DateTime) reader[6] : DateTime.MinValue; 
                res.Add(invoice);
            }

            return res.ToArray();
        }

        public void Update(Invoice entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Invoice entity)
        {
            throw new System.NotImplementedException();
        }
    }
}