using System;
using Microsoft.Data.SqlClient;

namespace Connection
{
    public interface ISession : IDisposable
    {
        bool BeginTran();
        bool OpenConnection();
        void EndTransaction(bool myVote, bool isMyTransaction);
        void CloseConnection(bool isMyTransaction);

        SqlCommand CreateCommand();
    }
}