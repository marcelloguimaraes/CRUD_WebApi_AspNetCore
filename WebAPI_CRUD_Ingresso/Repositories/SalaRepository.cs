using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_CRUD_Ingresso.Interfaces;
using WebAPI_CRUD_Ingresso.Models;

namespace WebAPI_CRUD_Ingresso.Repositories
{
    public class SalaRepository : IGenericRepository<Sala>
    {
        public AppDb Connection { get; } = new AppDb();

        public IEnumerable<Sala> GetAll()
        {
            using (var con = Connection.GetConnection())
            {
                var salas = con.GetAll<Sala>().ToList();

                for (int i = 0; i < salas.Count; i++)
                {
                    string sql = "SELECT IdSessao," +
                             "       Preco," +
                             "       DATE_FORMAT(DataSessao, '%d/%m/%Y') DataSessao," +
                             "       DATE_FORMAT(Hora, '%H:%i') Hora," +
                             "       TipoIdioma," +
                             "       se.IdCinema," +
                             "       se.IdSala," +
                             "       se.IdFilme" +
                             "  from sessao se" +
                             " inner join sala sa on se.IdSala = sa.IdSala" +
                             " and se.IdSala = @IdSala";

                    con.Open();

                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@IdSala", salas[i].IdSala);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    var sessoes = new List<Sessao>();

                    while (dr.Read())
                    {
                        Sessao sessao = new Sessao
                        {
                            IdSessao = Convert.ToInt32(dr["IdSessao"]),
                            DataSessao = dr["DataSessao"].ToString(),
                            Hora = dr["Hora"].ToString(),
                            IdCinema = Convert.ToInt32(dr["IdCinema"]),
                            IdFilme = Convert.ToInt32(dr["IdFilme"]),
                            IdSala = Convert.ToInt32(dr["IdSala"]),
                            Preco = Convert.ToDouble(dr["Preco"]),
                            TipoIdioma = dr["TipoIdioma"].ToString()
                        };
                        sessoes.Add(sessao);
                    }

                    //preenche as sessões
                    salas[i].Sessoes = sessoes;
                    con.Close();
                }

                return salas;
            }
        }

        public Sala GetById(int id)
        {
            using (var con = Connection.GetConnection())
            {
                return GetById(id, con);
            }
        }

        public Sala GetById(int id, MySqlConnection con)
        {
            string sql = "SELECT * FROM sala WHERE IdSala = @IdSala";
            var sala = con.QueryFirstOrDefault<Sala>(sql, param: new { IdSala = id});

            sql = "SELECT   IdSessao," +
                    "       Preco," +
                    "       DATE_FORMAT(DataSessao, '%d/%m/%Y') DataSessao," +
                    "       DATE_FORMAT(Hora, '%H:%i') Hora," +
                    "       TipoIdioma," +
                    "       se.IdCinema," +
                    "       se.IdSala," +
                    "       se.IdFilme" +
                    "  from sessao se" +
                    " inner join sala sa on se.IdSala = sa.IdSala" +
                    " and se.IdSala = @IdSala";

            con.Open();

            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@IdSala", sala.IdSala);
            MySqlDataReader dr = cmd.ExecuteReader();

            var sessoes = new List<Sessao>();

            while (dr.Read())
            {
                Sessao sessao = new Sessao
                {
                    IdSessao = Convert.ToInt32(dr["IdSessao"]),
                    DataSessao = dr["DataSessao"].ToString(),
                    Hora = dr["Hora"].ToString(),
                    IdCinema = Convert.ToInt32(dr["IdCinema"]),
                    IdFilme = Convert.ToInt32(dr["IdFilme"]),
                    IdSala = Convert.ToInt32(dr["IdSala"]),
                    Preco = Convert.ToDouble(dr["Preco"]),
                    TipoIdioma = dr["TipoIdioma"].ToString()
                };
                sessoes.Add(sessao);
            }

            //preenche as sessões
            sala.Sessoes = sessoes;
            con.Close();

            return sala;
        }

        public void Add(Sala entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Insert(entity);
            }
        }

        public void Delete(Sala entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Delete(GetById(entity.IdSala, con));
            }
        }

        public void Update(Sala entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Update(entity);
            }
        }

        public Sala SelectAllByOneField(dynamic fieldValue, string fieldName, string tableName)
        {
            using (var con = Connection.GetConnection())
            {
                dynamic obj;
                try
                {
                    string sql = $"SELECT * FROM {tableName} WHERE {fieldName} = @{fieldName}";
                    obj = con.QueryFirstOrDefault<Sala>(sql, new { NomeSala = fieldValue });
                }
                catch (NullReferenceException) { obj = null; }
                return obj;
            }
        }
    }
}
