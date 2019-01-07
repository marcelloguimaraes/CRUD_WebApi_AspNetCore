using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace WebAPI_CRUD_Ingresso
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Con;
        public MySqlCommand Cmd;
        public MySqlDataReader Dr;

        public readonly string ConnectionString = "server=localhost;userid=root;password=admin;port=3306;database=ingresso;";

        public MySqlConnection GetConnection()
        {
            return Con = new MySqlConnection(ConnectionString);
        }

        public MySqlTransaction BeginTransaction(MySqlConnection con)
        {
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            MySqlTransaction myTrans = con.BeginTransaction();
            cmd.Connection = con;
            cmd.Transaction = myTrans;

            return myTrans;
        }

        public void Dispose()
        {
            Con.Close();
        }
    }
}
