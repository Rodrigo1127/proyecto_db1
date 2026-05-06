using Microsoft.AspNetCore.Mvc;
using Hospital.Interop.API.Services;
using Microsoft.EntityFrameworkCore;
using Hospital.Interop.API.Data;
using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Controllers
{
    [ApiController]
    [Route("api/facturacion")]
    [Tags("Facturación y Pagos")]
    public class FacturacionController : ControllerBase
    {
        private readonly OrquestadorService _service;
        private readonly HospitalDbContext _context;

        public FacturacionController(OrquestadorService service, HospitalDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Factura>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var facturas = await _context.Facturas.ToListAsync();
            return Ok(facturas);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar(
            [FromQuery] int? pacienteId = null,
            [FromQuery] string? estado = null)
        {
            try
            {
                var query = _context.Facturas.AsQueryable();

                if (pacienteId.HasValue)
                    query = query.Where(f => f.PacienteId == pacienteId.Value);

                if (!string.IsNullOrWhiteSpace(estado))
                    query = query.Where(f => f.Estado == estado);

                var facturas = await query.ToListAsync();
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al buscar facturas", detalle = ex.Message });
            }
        }

        [HttpGet("paciente/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByPaciente(int id)
        {
            var data = await _service.ObtenerPacienteCompleto(id);
            if (data == null)
                return NotFound(new { mensaje = "Paciente no encontrado" });
            return Ok(data.Facturas);
        }

        [HttpGet("{id}", Name = "GetFactura")]
        [ProducesResponseType(typeof(Factura), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
                return NotFound(new { mensaje = "Factura no encontrada" });
            return Ok(factura);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Factura), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Factura factura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            factura.FechaFactura = DateTime.SpecifyKind(factura.FechaFactura, DateTimeKind.Utc);

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = factura.FacturaId }, factura);
        }
    }
}
