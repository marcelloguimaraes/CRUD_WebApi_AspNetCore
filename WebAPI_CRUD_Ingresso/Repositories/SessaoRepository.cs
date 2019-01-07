using System;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using WebAPI_CRUD_Ingresso.Interfaces;
using WebAPI_CRUD_Ingresso.Models;

namespace WebAPI_CRUD_Ingresso.Repositories
{
    public class SessaoRepository : IGenericRepository<Sessao>
    {
        public AppDb Connection { get; } = new AppDb();

        public void Add(Sessao entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Insert(entity);
            }
        }

        public void Delete(Sessao entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Delete(GetById(entity.IdSessao, con));
            }
        }

        public IEnumerable<Sessao> GetAll()
        {
            using (var con = Connection.GetConnection())
            {
                //return con.GetAll<Sessao>();
                string sql = "SELECT IdSessao," +
                             "       Preco," +
                             "       DATE_FORMAT(DataSessao, '%d/%m/%Y') DataSessao," +
                             "       DATE_FORMAT(Hora, '%H:%i') Hora," +
                             "       TipoIdioma," +
                             "       IdCinema," +
                             "       IdSala," +
                             "       IdFilme" +
                             " from sessao";

                return con.Query<Sessao>(sql);
            }
        }

        public Sessao GetById(int id)
        {
            using (var con = Connection.GetConnection())
            {
                return GetById(id, con);
            }
        }

        public Sessao GetById(int id, MySqlConnection con)
        {
            string sql = "SELECT IdSessao," +
                             "       Preco," +
                             "       DATE_FORMAT(DataSessao, '%d/%m/%Y') DataSessao," +
                             "       DATE_FORMAT(Hora, '%H:%i') Hora," +
                             "       TipoIdioma," +
                             "       IdCinema," +
                             "       IdSala," +
                             "       IdFilme" +
                             " FROM sessao" +
                             " WHERE IdSessao = @IdSessao";

            return con.QueryFirstOrDefault<Sessao>(sql, param: new { IdSessao = id });
        }

        public Sessao SelectAllByOneField(dynamic fieldValue, string fieldName, string tableName)
        {
            Sessao sessao = null;
            return sessao;
        }

        public void Update(Sessao entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Update(entity);
            }
        }
    }
}
