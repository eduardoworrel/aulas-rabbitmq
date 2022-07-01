using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace webapi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacaoController : ControllerBase
    {
        private readonly MyContext _context;

        public PublicacaoController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Publicacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publicacao>>> GetPublicacoes()
        {
          if (_context.Publicacoes == null)
          {
              return NotFound();
          }
            return await _context.Publicacoes.ToListAsync();
        }

        // GET: api/Publicacao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publicacao>> GetPublicacao(int id)
        {
          if (_context.Publicacoes == null)
          {
              return NotFound();
          }
            var publicacao = await _context.Publicacoes.FindAsync(id);

            if (publicacao == null)
            {
                return NotFound();
            }

            return publicacao;
        }

        // PUT: api/Publicacao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublicacao(int id, Publicacao publicacao)
        {
            if (id != publicacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(publicacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Publicacao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publicacao>> PostPublicacao(Publicacao publicacao)
        {
          if (_context.Publicacoes == null)
          {
              return Problem("Entity set 'MyContext.Publicacoes'  is null.");
          }
           publicacao.DataHoraCriacao = DateTime.Now;
            _context.Publicacoes.Add(publicacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublicacao", new { id = publicacao.Id }, publicacao);
        }

        // DELETE: api/Publicacao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublicacao(int id)
        {
            if (_context.Publicacoes == null)
            {
                return NotFound();
            }
            var publicacao = await _context.Publicacoes.FindAsync(id);
            if (publicacao == null)
            {
                return NotFound();
            }

            _context.Publicacoes.Remove(publicacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublicacaoExists(int id)
        {
            return (_context.Publicacoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
