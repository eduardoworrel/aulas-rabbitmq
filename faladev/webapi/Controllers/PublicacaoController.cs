using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

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
            return await _context.Publicacoes
            .OrderByDescending(a=> a.DataHoraCriacao)
            .ToListAsync();
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

            PublicaTextoBruto(publicacao);

            return CreatedAtAction("GetPublicacao", new { id = publicacao.Id }, publicacao);
        }

        private void PublicaTextoBruto(Publicacao publicacao)
        {
            var factory = new ConnectionFactory() { 
                        HostName = "143.244.137.227",
                        UserName = "admin",
                        Password = "devInRabbit"
                        };

            using(var connection = factory.CreateConnection()) //disposable
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "refinaTextoBruto",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                channel.ExchangeDeclare("coordena", ExchangeType.Direct);

                channel.QueueBind(queue:  "refinaTextoBruto",
                            exchange: "coordena",
                            routingKey: "refinaTextoBruto");

                 var body = Encoding.UTF8
                 .GetBytes(JsonConvert.SerializeObject(
                    new PublicacaoRefinada{
                        DataHoraCriacao = publicacao.DataHoraCriacao,
                        Texto = publicacao.Texto,
                        IP = GetIp() 
                    }
                 ));

                Console.WriteLine(" [x] Sent {0}",  GetIp());
                channel.BasicPublish(exchange: "coordena",
                                    routingKey: "refinaTextoBruto",
                                    basicProperties: null,
                                    body: body);
            }

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
        private string GetIp()
        {
            var x = User;
            
            string ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            if (string.IsNullOrEmpty(ip) )
            {
                ip = HttpContext.GetServerVariable("REMOTE_ADDR");
            }
            return ip;
        }
    }
   
}
