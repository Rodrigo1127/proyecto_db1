using Microsoft.AspNetCore.Mvc;
using Hospital.Interop.API.Services;
using Microsoft.EntityFrameworkCore;
using Hospital.Interop.API.Data;
using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Controllers
{
    [ApiController]
    [Route("api/facturacion")]
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

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFacturas(int id)
        {
            var data = await _service.ObtenerPacienteCompleto(id);
            if (data == null)
                return NotFound(new { mensaje = "Paciente no encontrado" });
            return Ok(data.Facturas);
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

            return CreatedAtAction(nameof(GetFacturas), new { id = factura.FacturaId }, factura);
        }
    }
}
