using System;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebAPI_CRUD_Ingresso.Interfaces;
using WebAPI_CRUD_Ingresso.Models;

namespace WebAPI_CRUD_Ingresso.Repositories
{
    public class FilmeRepository : IGenericRepository<Filme>
    {
        public AppDb Connection { get; } = new AppDb();

        public IEnumerable<Filme> GetAll()
        {
            using (var con = Connection.GetConnection())
            {
                return con.GetAll<Filme>();
            }
        }

        public Filme GetById(int id)
        {
            using (var con = Connection.GetConnection())
            {
                return GetById(id, con);
            }
        }

        public Filme GetById(int id, MySqlConnection con) => con.Get<Filme>(id);

        public void Add(Filme entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Insert(entity);
            }
        }

        public void Delete(Filme entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Delete(GetById(entity.IdFilme, con));
            }
        }

        public void Update(Filme entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Update(entity);
            }
        }

        public Filme SelectAllByOneField(dynamic fieldValue, string fieldName, string tableName)
        {
            using(var con = Connection.GetConnection())
            {
                dynamic obj;
                try
                {
                    string sql = $"SELECT * FROM {tableName} WHERE {fieldName} = @{fieldName}";
                    obj = con.QueryFirstOrDefault<Filme>(sql, new { NomeFilme = fieldValue });
                }
                catch (NullReferenceException) { obj = null; }
                return obj;
            }
        }

        /// <summary>
        /// Realiza um "merge" entre o objeto enviado no json e o registro do banco de dados, 
        /// permitindo enviar somente o campo que necessita alterar o valor.
        /// </summary>
        /// <param name="filme">Objeto recebido em json pela api e que será persistido no banco de dados</param>
        /// <param name="filmeDb">Objeto que contém o registro no banco de dados</param>
        /*public void MontaObjetoParaAlteracao(Filme entity, Filme entityDb)
        {
            foreach (var prop in entity.GetType().GetProperties())
            {
                foreach (var propDb in entityDb.GetType().GetProperties())
                {
                    if (prop.Name == propDb.Name)
                    {
                        string propValue = prop.GetValue(entity) == null ? null : prop.GetValue(entity).ToString();
                        string proDbValue = propDb.GetValue(entityDb) == null ? null : propDb.GetValue(entityDb).ToString();

                        if (propValue == proDbValue)
                        {
                            prop.SetValue(entity, propDb.GetValue(entityDb));
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(propValue) || propValue == "0")
                                prop.SetValue(entity, propDb.GetValue(entityDb));
                            else
                                prop.SetValue(entity, prop.GetValue(entity));
                        }
                        break;
                    }
                }
            }
        }*/
    }
}
