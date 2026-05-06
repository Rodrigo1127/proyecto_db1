using Microsoft.AspNetCore.Mvc;
using Hospital.Interop.API.Services;
using Microsoft.EntityFrameworkCore;
using Hospital.Interop.API.Data;

namespace Hospital.Interop.API.Controllers
{
    [ApiController]
    [Route("api/laboratorio")]
    public class LaboratorioController : ControllerBase
    {
        private readonly OrquestadorService _service;
        private readonly HospitalDbContext _context;

        public LaboratorioController(OrquestadorService service, HospitalDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var examenes = await _context.Examenes.ToListAsync();
            return Ok(examenes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetExamenes(int id)
        {
            var data = await _service.ObtenerPacienteCompleto(id);
            if (data == null)
                return NotFound(new { mensaje = "Paciente no encontrado" });
            return Ok(data.Examenes);
        }
    }
}
