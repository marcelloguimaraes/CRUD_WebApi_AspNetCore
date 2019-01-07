using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_CRUD_Ingresso.Models;
using WebAPI_CRUD_Ingresso.Repositories;

namespace WebAPI_CRUD_Ingresso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        public FilmeRepository Repository { get; } = new FilmeRepository();

        [HttpGet]
        public IActionResult GetAll() {

            var lista = (List<Filme>)Repository.GetAll();

            if (lista.Count != 0)
                return Ok(lista);
            else
                return NotFound("Nenhum filme encontrado");
        }

        // GET: api/Filme/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var filme = Repository.GetById(id);

            if (filme != null)
                return Ok(filme);
            else
                return NotFound("Filme não encontrado");
        }

        // POST: api/Filme
        [HttpPost]
        public IActionResult Post([FromBody] Filme filme)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Repository.SelectAllByOneField
            (
                fieldValue: filme.NomeFilme,
                fieldName: nameof(filme.NomeFilme),
                tableName: nameof(filme)
            );

            if (obj != null)
                return BadRequest("Filme já existe");

            Repository.Add(filme);
            return Ok("Filme adicionado");
        }

        // PUT: api/Filme/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Filme filme)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Repository.GetById(id);

            if (obj == null)
                return NotFound("Filme não encontrado");

            filme.IdFilme = id;
            Repository.Update(filme);
            return Ok("Filme atualizado");
        }

        // DELETE: api/Filme/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = Repository.GetById(id);

            if (obj == null)
                return NotFound("Filme não encontrado");

            Repository.Delete(obj);
            return Ok("Filme excluído");
        }
    }
}
