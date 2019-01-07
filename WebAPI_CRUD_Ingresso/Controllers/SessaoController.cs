using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI_CRUD_Ingresso.Models;
using WebAPI_CRUD_Ingresso.Repositories;

namespace WebAPI_CRUD_Ingresso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessaoController : ControllerBase
    {
        public SessaoRepository Repository { get; } = new SessaoRepository();

        // GET: api/Sessao
        [HttpGet]
        public IActionResult GetAll()
        {

            var lista = (List<Sessao>)Repository.GetAll();

            if (lista.Count != 0)
                return Ok(lista);
            else
                return NotFound("Nenhuma sessão encontrada");
        }

        // GET: api/Sessao/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sessao = Repository.GetById(id);

            if (sessao != null)
                return Ok(sessao);
            else
                return NotFound("Sessão não encontrada");
        }

        // POST: api/Sessao
        [HttpPost]
        public IActionResult Post([FromBody] Sessao sessao)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Repository.Add(sessao);
            return Ok("Sessão adicionada");
        }

        // PUT: api/Sessao/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Sessao sessao)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Repository.GetById(id);

            if (obj == null)
                return NotFound("Sessao não encontrada");

            sessao.IdSessao = id;
            Repository.Update(sessao);
            return Ok("Sessão atualizada");
        }

        // DELETE: api/Sessao/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = Repository.GetById(id);

            if (obj == null)
                return NotFound("Sessão não encontrada");

            Repository.Delete(obj);
            return Ok("Sessão excluída");
        }
    }
}