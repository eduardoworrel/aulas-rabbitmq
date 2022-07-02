using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ConhecimentoService
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacaoRefinadaController : ControllerBase
    {
        private readonly MyContext _context;

        public PublicacaoRefinadaController(MyContext context)
        {
            _context = context;
        }

        // GET: api/PublicacaoRefinada
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicacaoRefinada>>> GetPublicacoesRefinadas()
        {
          if (_context.PublicacoesRefinadas == null)
          {
              return NotFound();
          }
            return await _context.PublicacoesRefinadas.ToListAsync();
        }

        // GET: api/PublicacaoRefinada/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicacaoRefinada>> GetPublicacaoRefinada(int id)
        {
          if (_context.PublicacoesRefinadas == null)
          {
              return NotFound();
          }
            var publicacaoRefinada = await _context.PublicacoesRefinadas.FindAsync(id);

            if (publicacaoRefinada == null)
            {
                return NotFound();
            }

            return publicacaoRefinada;
        }

        // PUT: api/PublicacaoRefinada/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublicacaoRefinada(int id, PublicacaoRefinada publicacaoRefinada)
        {
            if (id != publicacaoRefinada.Id)
            {
                return BadRequest();
            }

            _context.Entry(publicacaoRefinada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicacaoRefinadaExists(id))
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

        // POST: api/PublicacaoRefinada
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PublicacaoRefinada>> PostPublicacaoRefinada([FromForm] PublicacaoRefinada publicacaoRefinada)
        {
          if (_context.PublicacoesRefinadas == null)
          {
              return Problem("Entity set 'MyContext.PublicacoesRefinadas'  is null.");
          }
            _context.PublicacoesRefinadas.Add(publicacaoRefinada);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublicacaoRefinada", new { id = publicacaoRefinada.Id }, publicacaoRefinada);
        }

        // DELETE: api/PublicacaoRefinada/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublicacaoRefinada(int id)
        {
            if (_context.PublicacoesRefinadas == null)
            {
                return NotFound();
            }
            var publicacaoRefinada = await _context.PublicacoesRefinadas.FindAsync(id);
            if (publicacaoRefinada == null)
            {
                return NotFound();
            }

            _context.PublicacoesRefinadas.Remove(publicacaoRefinada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublicacaoRefinadaExists(int id)
        {
            return (_context.PublicacoesRefinadas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
