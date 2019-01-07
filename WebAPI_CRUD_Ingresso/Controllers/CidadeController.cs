using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_CRUD_Ingresso.Models;
using WebAPI_CRUD_Ingresso.Repositories;

namespace WebAPI_CRUD_Ingresso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        public CidadeRepository Repository { get; } = new CidadeRepository();

        [HttpGet]
        public IActionResult GetAll()
        {

            var lista = (List<Cidade>)Repository.GetAll();

            if (lista.Count != 0)
                return Ok(lista);
            else
                return NotFound("Nenhuma cidade encontrada");
        }

        // GET: api/Cidade/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cidade = Repository.GetById(id);

            if (cidade != null)
                return Ok(cidade);
            else
                return NotFound("Cidade não encontrada");
        }

        // POST: api/Cidade
        [HttpPost]
        public IActionResult Post([FromBody] Cidade cidade)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Repository.SelectAllByOneField
            (
                fieldValue: cidade.NomeCidade,
                fieldName: nameof(cidade.NomeCidade),
                tableName: nameof(cidade)
            );

            if (obj != null)
                return BadRequest("Cidade já existe");

            Repository.Add(cidade);
            return Ok("Cidade adicionada");
        }

        // PUT: api/Cidade/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cidade cidade)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Repository.GetById(id);

            if (obj == null)
                return NotFound("Cidade não encontrada");

            cidade.IdCidade = id;
            Repository.Update(cidade);
            return Ok("Cidade atualizada");
        }

        // DELETE: api/Cidade/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = Repository.GetById(id);

            if (obj == null)
                return NotFound("Cidade não encontrada");

            Repository.Delete(obj);
            return Ok("Cidade excluída");
        }
    }
}