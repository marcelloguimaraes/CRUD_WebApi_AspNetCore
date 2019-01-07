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
    public class CidadeRepository : IGenericRepository<Cidade>
    {
        public AppDb Connection { get; } = new AppDb();

        public IEnumerable<Cidade> GetAll()
        {
            using (var con = Connection.GetConnection())
            {
                return con.GetAll<Cidade>();
            }
        }

        public Cidade GetById(int id)
        {
            using (var con = Connection.GetConnection())
            {
                return GetById(id, con);
            }
        }

        public Cidade GetById(int id, MySqlConnection con) => con.Get<Cidade>(id);

        public void Add(Cidade entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Insert(entity);
            }
        }

        public void Delete(Cidade entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Delete(GetById(entity.IdCidade, con));
            }
        }

        public void Update(Cidade entity)
        {
            using (var con = Connection.GetConnection())
            {
                con.Update(entity);
            }
        }

        public Cidade SelectAllByOneField(dynamic fieldValue, string fieldName, string tableName)
        {
            using (var con = Connection.GetConnection())
            {
                dynamic obj;
                try
                {
                    string sql = $"SELECT * FROM {tableName} WHERE {fieldName} = @{fieldName}";
                    obj = con.QueryFirstOrDefault<Cidade>(sql, new { NomeCidade = fieldValue });
                }
                catch (NullReferenceException) { obj = null; }
                return obj;
            }
        }
    }
}
