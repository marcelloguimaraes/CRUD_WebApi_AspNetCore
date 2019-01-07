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
    public class SalaController : ControllerBase
    {
        public SalaRepository Repository { get; } = new SalaRepository();

        // GET: api/Sala
        [HttpGet]
        public IActionResult GetAll()
        {

            var lista = (List<Sala>)Repository.GetAll();

            if (lista.Count != 0)
                return Ok(lista);
            else
                return NotFound("Nenhuma sala encontrado");
        }

        // GET: api/Sala/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sala = Repository.GetById(id);

            if (sala != null)
                return Ok(sala);
            else
                return NotFound("Sala não encontrada");
        }

        // POST: api/Sala
        [HttpPost]
        public IActionResult Post([FromBody] Sala sala)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Repository.SelectAllByOneField
            (
                fieldValue: sala.NomeSala,
                fieldName: nameof(sala.NomeSala),
                tableName: nameof(sala)
            );

            if (obj != null)
                return BadRequest("Sala já existe");

            Repository.Add(sala);
            return Ok("Sala adicionada");
        }

        // PUT: api/Sala/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Sala sala)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Repository.GetById(id);

            if (obj == null)
                return NotFound("Sala não encontrada");

            sala.IdSala = id;
            Repository.Update(sala);
            return Ok("Sala atualizada");
        }

        // DELETE: api/Sala/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = Repository.GetById(id);

            if (obj == null)
                return NotFound("Sala não encontrada");

            Repository.Delete(obj);
            return Ok("Sala excluída");
        }
    }
}
