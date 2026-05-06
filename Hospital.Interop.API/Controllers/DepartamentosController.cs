using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Interop.API.Data;
using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Controllers
{
    [ApiController]
    [Route("api/departamentos")]
    public class DepartamentosController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public DepartamentosController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Departamento>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var departamentos = await _context.Departamentos
                .Where(d => d.Activo)
                .ToListAsync();

            return Ok(departamentos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Departamento), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(d => d.DepartamentoId == id);

            if (departamento == null)
                return NotFound(new { mensaje = "Departamento no encontrado" });

            return Ok(departamento);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Departamento), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Departamento departamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = departamento.DepartamentoId }, departamento);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, [FromBody] Departamento departamento)
        {
            if (id != departamento.DepartamentoId)
                return BadRequest(new { mensaje = "ID no coincide" });

            var existe = await _context.Departamentos.AnyAsync(d => d.DepartamentoId == id);
            if (!existe)
                return NotFound(new { mensaje = "Departamento no encontrado" });

            _context.Entry(departamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(departamento);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);

            if (departamento == null)
                return NotFound(new { mensaje = "Departamento no encontrado" });

            departamento.Activo = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
