using System;
using Microsoft.Data.SqlClient;

namespace Connection
{
    public abstract class AbstractSession : ISession
    {
        private SqlTransaction _currentTrans;
        private SqlConnection _currentConn;
        private readonly string _connectionString;
        private bool _transactionVotes;

        public AbstractSession()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.InitialCatalog = "SI2_Grupo02_2021i";
            builder.UserID = "sa";//cr.Username;
            builder.Password = "Jpp55tm123";//cr.Password;
            builder.MaxPoolSize = 10;
            
            _connectionString = builder.ConnectionString; 
            //TODO Connection String here
        }

        public bool BeginTran()
        {
            bool st = false;
            if (_currentConn == null)
            {
                throw new Exception("Connection is closed");
            }

            if (_currentTrans == null)
            {
                _currentTrans = _currentConn.BeginTransaction();
                _transactionVotes = true;
                st = true;
            }

            return st;
        }

        public void EndTransaction(bool myVote, bool isMyTransaction)
        {
            _transactionVotes &= myVote;
            if (isMyTransaction)
            {
                if (_transactionVotes)
                    _currentTrans.Commit();
                else
                    _currentTrans.Rollback();
                _currentTrans = null;
            }
        }

        public bool OpenConnection()
        {
            bool sc = false;
            if (_currentConn == null)
            {
                _currentConn = new SqlConnection(_connectionString);
                _currentConn.Open();
                sc = true;
            }

            return sc;
        }

        public void CloseConnection(bool isMyConnection)
        {
            if (isMyConnection && _currentConn != null)
            {
                _currentConn.Close();
                _currentConn = null;
            }
        }

        public SqlCommand CreateCommand()
        {
            SqlCommand cmd = _currentConn.CreateCommand();

            cmd.Transaction = _currentTrans;

            return cmd;
        }

        public void Dispose()
        {
            _currentConn?.Dispose();
        }
    }
}