using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Interop.API.Data;
using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Controllers
{
    [ApiController]
    [Route("api/resultados-prueba")]
    [Tags("Laboratorio Clínico")]
    public class ResultadosPruebaController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public ResultadosPruebaController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ResultadoPrueba>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var resultados = await _context.ResultadosPrueba
                .Include(r => r.SolicitudPrueba)
                .ToListAsync();

            return Ok(resultados);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultadoPrueba), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var resultado = await _context.ResultadosPrueba
                .Include(r => r.SolicitudPrueba)
                .FirstOrDefaultAsync(r => r.ResultadoPruebaId == id);

            if (resultado == null)
                return NotFound(new { mensaje = "Resultado no encontrado" });

            return Ok(resultado);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultadoPrueba), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] ResultadoPrueba resultado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.ResultadosPrueba.Add(resultado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = resultado.ResultadoPruebaId }, resultado);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] ResultadoPrueba resultado)
        {
            if (id != resultado.ResultadoPruebaId)
                return BadRequest(new { mensaje = "ID no coincide" });

            var existe = await _context.ResultadosPrueba.AnyAsync(r => r.ResultadoPruebaId == id);
            if (!existe)
                return NotFound(new { mensaje = "Resultado no encontrado" });

            _context.Entry(resultado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(resultado);
        }
    }
}
