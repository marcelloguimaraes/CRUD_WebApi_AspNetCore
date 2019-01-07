using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using WebAPI_CRUD_Ingresso.Interfaces;
using WebAPI_CRUD_Ingresso.Models;

namespace WebAPI_CRUD_Ingresso.Repositories
{
    public class CinemaRepository : IGenericRepository<Cinema>
    {

        public AppDb Connection { get; } = new AppDb();

        public void Add(Cinema entity)
        {
            using (var con = Connection.GetConnection())
            {
                var myTrans = Connection.BeginTransaction(con);

                try
                {
                    con.Insert(entity);

                    foreach (int sala in entity.SalasArray)
                    {
                        string sql = "INSERT INTO cinema_sala(IdCinema, IdSala) VALUES(@IdCinema, @IdSala)";

                        con.Execute(sql, param: new { entity.IdCinema, IdSala = sala });
                    }

                    myTrans.Commit();
                }
                catch (MySqlException e)
                {
                    myTrans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(Cinema entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Delete(entity);
            }
        }

        public IEnumerable<Cinema> GetAll()
        {
            string sql = "SELECT *" +
                         "  FROM cinema cine" +
                         "  INNER JOIN cidade cid ON cine.IdCidade = cid.IdCidade";

            var con = Connection.GetConnection();

            var cinemas = con.Query<Cinema, Cidade, Cinema>(
            sql,
            (cinema, cidade) =>
            {
                cinema.Cidade = cidade;
                return cinema;
            },
            splitOn: "IdCidade").ToList();

            for(var i = 0; i < cinemas.Count; i++)
            {
                sql = "SELECT s.IdSala, s.NomeSala " +
                        "  FROM sala s " +
                        "  INNER JOIN cinema_sala cs ON s.IdSala = cs.IdSala" +
                        "  AND cs.IdCinema = @IdCinema";

                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@IdCinema", cinemas[i].IdCinema);
                MySqlDataReader dr = cmd.ExecuteReader();

                var salas = new List<Sala>();

                while (dr.Read())
                {
                    Sala sala = new Sala
                    {
                        IdSala = Convert.ToInt32(dr["IdSala"]),
                        NomeSala = dr["NomeSala"].ToString()
                    };
                    salas.Add(sala);
                }

                //Preenche as salas
                cinemas[i].Salas = salas;
                con.Close();

                for (int y = 0; y < cinemas[i].Salas.Count; y++)
                {
                    sql = "SELECT    IdSessao," +
                             "       Preco," +
                             "       DATE_FORMAT(DataSessao, '%d/%m/%Y') DataSessao," +
                             "       DATE_FORMAT(Hora, '%H:%i') Hora," +
                             "       TipoIdioma," +
                             "       se.IdCinema," +
                             "       se.IdSala," +
                             "       se.IdFilme" +
                             " from sessao se" +
                             " inner join cinema cine on se.IdCinema = cine.IdCinema" +
                             " inner join sala sa on sa.IdSala = se.IdSala" +
                             " and se.IdCinema = @IdCinema" +
                             " and se.IdSala = @IdSala";

                    con.Open();

                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@IdCinema", cinemas[i].IdCinema);
                    cmd.Parameters.AddWithValue("@IdSala", cinemas[i].Salas[y].IdSala);
                    dr = cmd.ExecuteReader();

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
                    cinemas[i].Salas[y].Sessoes = sessoes;
                    con.Close();
                }

                con.Close();
            }

            return cinemas;
        }

        public Cinema GetById(int id)
        {
            using (var con = Connection.GetConnection())
            {
                return GetById(id, con);
            }
        }

        public Cinema GetById(int id, MySqlConnection con)
        {
            string sql = "SELECT * FROM cinema cine INNER JOIN cidade cid ON cine.IdCidade = cid.IdCidade AND cine.IdCinema = @Id";

            var obj = con.Query<Cinema, Cidade, Cinema>(
                sql,
                map: (cinema, cidade) =>
                {
                    cinema.Cidade = cidade;
                    return cinema;
                },
                splitOn: "IdCidade",
                param: new { Id = id }).ToArray();

            Cinema objCinema = obj[0];

            sql = "SELECT s.IdSala, s.NomeSala " +
                    "  FROM sala s " +
                    "  INNER JOIN cinema_sala cs ON s.IdSala = cs.IdSala" +
                    "  AND cs.IdCinema = @IdCinema";

            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@IdCinema", objCinema.IdCinema);
            MySqlDataReader dr = cmd.ExecuteReader();

            var salas = new List<Sala>();

            while (dr.Read())
            {
                Sala sala = new Sala
                {
                    IdSala = Convert.ToInt32(dr["IdSala"]),
                    NomeSala = dr["NomeSala"].ToString()
                };
                salas.Add(sala);
            }

            objCinema.Salas = salas;

            con.Close();

            return objCinema;
        }

        public Cinema SelectAllByOneField(dynamic fieldValue, string fieldName, string tableName)
        {
            using (var con = Connection.GetConnection())
            {
                dynamic obj;
                try
                {
                    string sql = $"SELECT * FROM {tableName} WHERE {fieldName} = @{fieldName}";
                    obj = con.QueryFirstOrDefault<Cinema>(sql, new { NomeCinema = fieldValue });
                }
                catch (NullReferenceException) { obj = null; }
                return obj;
            }
        }

        public void Update(Cinema entity)
        {
            using (var con = Connection.GetConnection())
            {
                var myTrans = Connection.BeginTransaction(con);

                try
                {
                    con.Update(entity);

                    //Exclui as salas
                    con.Execute("DELETE FROM cinema_sala WHERE IdCinema = @IdCinema", param: new { entity.IdCinema});

                    //Percorre as novas salas enviadas e as insere
                    foreach (int sala in entity.SalasArray)
                    {
                        string sql = "INSERT INTO cinema_sala(IdCinema, IdSala) VALUES(@IdCinema, @IdSala) ";

                        con.Execute(sql, param: new { entity.IdCinema, IdSala = sala });
                    }

                    myTrans.Commit();
                }
                catch (MySqlException e)
                {
                    myTrans.Rollback();
                    throw;
                }
            }
        }
    }
}
