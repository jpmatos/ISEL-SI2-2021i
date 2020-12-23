using System;
using System.Data;
using Entity;
using IMapper;
using Microsoft.Data.SqlClient;

namespace Mapper
{
    public class MapperContributor : IMapperContributor
    {
        private readonly IMapperSession _mySession;
        private bool _isMyConnection;
        
        private SqlCommand CreateCommand()
        {
            return _mySession.CreateCommand();
        }
        
        public MapperContributor(IMapperSession s)
        {
            _mySession = s;
        }
        
        public void Create(Contributor entity)
        {
            throw new System.NotImplementedException();
        }

        public Contributor Read(int id)
        {
            _isMyConnection = _mySession.OpenConnection();
            
            Contributor contributor = new Contributor();

            SqlCommand cmd = CreateCommand();
            cmd.CommandText = "SELECT @NIF=NIF, @Name=Name, @Address=Address FROM Contributor WHERE NIF = @NIF_IN";

            SqlParameter p1 = cmd.Parameters.Add("@NIF", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            
            SqlParameter p2 = cmd.Parameters.Add("@Name", SqlDbType.Char, 128);
            p2.Direction = ParameterDirection.Output;

            SqlParameter p3 = cmd.Parameters.Add("@Address", SqlDbType.Char, 128);
            p3.Direction = ParameterDirection.Output;
            
            SqlParameter p4 = new SqlParameter("@NIF_IN", id);
            cmd.Parameters.Add(p4);

            cmd.ExecuteNonQuery();
            if(p1.Value is System.DBNull)
                throw new Exception("Contributor not found: NIF=" + id);

            contributor.NIF = (int) p1.Value;
            contributor.Name = ((string) p2.Value).TrimEnd();
            contributor.Address = ((string) p3.Value).TrimEnd();
            
            _mySession.CloseConnection(_isMyConnection);

            return contributor;
        }

        public void Update(Contributor entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Contributor entity)
        {
            throw new System.NotImplementedException();
        }
    }
}