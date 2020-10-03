using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aspnet_mongodb.Models;
using aspnet_mongodb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aspnet_mongodb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LivrariaController : ControllerBase
    {
        private readonly LivrariaService _livrariaService;
        private readonly ILogger<LivrariaController> _logger;

        public LivrariaController(LivrariaService livrariaService, ILogger<LivrariaController> logger)
        {
            _livrariaService = livrariaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Livro>> Get()
        {
            try
            {
                return await _livrariaService.Todos();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao listar os Livros - {ex.Message}");

                throw;
            }            
        }

        [HttpGet("{id}")]
        public async Task<Livro> Get(string id)
        {
            try
            {
                return await _livrariaService.PorId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao listar o Livro {id} - {ex.Message}");

                throw;
            }            
        }

        [HttpGet("categoria/{categoria}")]
        public async Task<IEnumerable<Livro>> BuscarPorCategoria(string categoria)
        {
            try
            {
                return await _livrariaService.PorCategoria(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao listar o Livro da Categoria {categoria} - {ex.Message}");

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Livro livro)
        {
            try
            {
                await _livrariaService.Cadastrar(livro);

                return Ok(livro);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao cadastrar o Livro {ex.Message}");

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Livro livro, string id)
        {
            try
            {
                var livroDb = await _livrariaService.PorId(id);

                if (livroDb == null)
                    return NotFound();

                await _livrariaService.Atualizar(livro, id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar o Livro {ex.Message}");

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var livroDb = await _livrariaService.PorId(id);

                if (livroDb == null)
                    return NotFound();

                await _livrariaService.Remover(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir o Livro {ex.Message}");

                throw;
            }

            return NoContent();
        }
    }
}
