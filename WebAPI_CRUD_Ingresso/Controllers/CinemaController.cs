using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI_CRUD_Ingresso.Models;
using WebAPI_CRUD_Ingresso.Repositories;

namespace WebAPI_CRUD_Ingresso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        public CinemaRepository Repository { get; } = new CinemaRepository();

        [HttpGet]
        public IActionResult GetAll()
        {

            var lista = (List<Cinema>)Repository.GetAll();

            if (lista.Count != 0)
                return Ok(lista);
            else
                return NotFound("Nenhum cinema encontrado");
        }

        // GET: api/Cinema/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cinema = Repository.GetById(id);

            if (cinema != null)
                return Ok(cinema);
            else
                return NotFound("Cinema não encontrado");
        }

        // POST: api/Cinema
        [HttpPost]
        public IActionResult Post([FromBody] Cinema cinema)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Repository.SelectAllByOneField
            (
                fieldValue: cinema.NomeCinema,
                fieldName: nameof(cinema.NomeCinema),
                tableName: nameof(cinema)
            );

            if (obj != null)
                return BadRequest("Cinema já existe");

            Repository.Add(cinema);
            return Ok("Cinema adicionado");
        }

        // PUT: api/Cinema/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cinema cinema)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Repository.GetById(id);

            if (obj == null)
                return NotFound("Cinema não encontrado");

            cinema.IdCinema = id;
            Repository.Update(cinema);
            return Ok("Cinema atualizado");
        }

        // DELETE: api/Cinema/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = Repository.GetById(id);

            if (obj == null)
                return NotFound("Cinema não encontrado");

            Repository.Delete(obj);
            return Ok("Cinema excluído");
        }
    }
}