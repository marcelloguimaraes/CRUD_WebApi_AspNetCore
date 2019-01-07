using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace WebAPI_CRUD_Ingresso.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        TEntity GetById(int id, MySqlConnection con);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);

        /// <summary>
        /// Método genérico para selecionar todos os registros de uma determinada tabela
        /// por um determinado valor de um determinado campo do banco de dados
        /// </summary>
        /// <param name="fieldValue">Valor utilizado na cláusula where como filtro da busca</param>
        /// <param name="fieldName">Nome do campo utilizado como filtro da busca</param>
        /// <param name="tableName">Nome da tabela onde será feito a Query</param>
        /// <returns>Uma entidade</returns>
        TEntity SelectAllByOneField(dynamic fieldValue,string fieldName, string tableName);
    }
}
