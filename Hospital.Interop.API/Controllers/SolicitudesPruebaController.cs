using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Interop.API.Data;
using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Controllers
{
	[ApiController]
	[Route("api/solicitudes-prueba")]
	public class SolicitudesPruebaController : ControllerBase
	{
		private readonly HospitalDbContext _context;

		public SolicitudesPruebaController(HospitalDbContext context)
		{
			_context = context;
		}

		// CRUD BÁSICO
		[HttpGet]
		[ProducesResponseType(typeof(List<SolicitudPrueba>), 200)]
		public async Task<IActionResult> GetAll()
		{
			var solicitudes = await _context.SolicitudesPrueba
				.Include(sp => sp.Paciente)
				.Include(sp => sp.TipoPrueba)
				.Include(sp => sp.Tecnico)
				.ToListAsync();

			return Ok(solicitudes);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(SolicitudPrueba), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetById(int id)
		{
			var solicitud = await _context.SolicitudesPrueba
				.Include(sp => sp.Paciente)
				.Include(sp => sp.TipoPrueba)
				.Include(sp => sp.Tecnico)
				.FirstOrDefaultAsync(sp => sp.SolicitudPruebaId == id);

			if (solicitud == null)
				return NotFound(new { mensaje = "Solicitud no encontrada" });

			return Ok(solicitud);
		}

		[HttpPost]
		[ProducesResponseType(typeof(SolicitudPrueba), 201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Create([FromBody] SolicitudPrueba solicitud)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			solicitud.FechaSolicitud = DateTime.SpecifyKind(
				solicitud.FechaSolicitud,
				DateTimeKind.Utc
			);

			_context.SolicitudesPrueba.Add(solicitud);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetById), new { id = solicitud.SolicitudPruebaId }, solicitud);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] SolicitudPrueba solicitud)
		{
			if (id != solicitud.SolicitudPruebaId)
				return BadRequest(new { mensaje = "El id de la URL no coincide con la solicitud" });

			var existente = await _context.SolicitudesPrueba.FindAsync(id);

			if (existente == null)
				return NotFound(new { mensaje = "Solicitud no encontrada" });

			existente.PacienteId = solicitud.PacienteId;
			existente.TipoPruebaId = solicitud.TipoPruebaId;
			existente.TecnicoId = solicitud.TecnicoId;
			existente.Estado = solicitud.Estado;
			existente.CostoFinal = solicitud.CostoFinal;

			existente.FechaSolicitud = DateTime.SpecifyKind(
				solicitud.FechaSolicitud,
				DateTimeKind.Utc
			);

			await _context.SaveChangesAsync();

			return Ok(existente);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var solicitud = await _context.SolicitudesPrueba.FindAsync(id);

			if (solicitud == null)
				return NotFound(new { mensaje = "Solicitud no encontrada" });

			_context.SolicitudesPrueba.Remove(solicitud);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// CONSULTA 1: Listado general con JOIN entre 2 tablas
		[HttpGet("mis/solicitudes-por-paciente")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> SolicitudesPorPaciente()
		{
			var resultado = from solicitud in _context.SolicitudesPrueba
							join paciente in _context.Pacientes on solicitud.PacienteId equals paciente.PacienteId
							select new
							{
								solicitud.SolicitudPruebaId,
								PacienteNombre = paciente.Nombre,
								paciente.Documento,
								solicitud.FechaSolicitud,
								solicitud.Estado
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 2: Agrupación con conteo
		[HttpGet("mis/pruebas-por-tipo-conteo")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> PruebasPorTipoConteo()
		{
			var resultado = from solicitud in _context.SolicitudesPrueba
							join tipoPrueba in _context.TiposPrueba on solicitud.TipoPruebaId equals tipoPrueba.TipoPruebaId
							group solicitud by tipoPrueba.Nombre into g
							select new
							{
								TipoPrueba = g.Key,
								TotalSolicitudes = g.Count()
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 3: Agrupación con suma
		[HttpGet("mis/ingresos-por-prueba")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> IngresosPorPrueba()
		{
			var resultado = from solicitud in _context.SolicitudesPrueba
							join tipoPrueba in _context.TiposPrueba on solicitud.TipoPruebaId equals tipoPrueba.TipoPruebaId
							group solicitud by tipoPrueba.Nombre into g
							select new
							{
								TipoPrueba = g.Key,
								TotalIngresos = g.Sum(s => s.CostoFinal),
								CantidadSolicitudes = g.Count()
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 4: Búsqueda filtrada por estado
		[HttpGet("mis/solicitudes-por-estado/{estado}")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> SolicitudesPorEstado(string estado)
		{
			var resultado = from solicitud in _context.SolicitudesPrueba
							join paciente in _context.Pacientes on solicitud.PacienteId equals paciente.PacienteId
							join tipoPrueba in _context.TiposPrueba on solicitud.TipoPruebaId equals tipoPrueba.TipoPruebaId
							where solicitud.Estado == estado
							select new
							{
								solicitud.SolicitudPruebaId,
								PacienteNombre = paciente.Nombre,
								TipoPruebaNombre = tipoPrueba.Nombre,
								solicitud.FechaSolicitud,
								solicitud.Estado
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 5: Registros que no tienen relación en otra tabla
		[HttpGet("mis/pruebas-sin-resultado")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> PruebasSinResultado()
		{
			var solicitudesConResultado = _context.ResultadosPrueba
				.Select(r => r.SolicitudPruebaId);

			var resultado = from solicitud in _context.SolicitudesPrueba
							where !solicitudesConResultado.Contains(solicitud.SolicitudPruebaId)
							join paciente in _context.Pacientes on solicitud.PacienteId equals paciente.PacienteId
							join tipoPrueba in _context.TiposPrueba on solicitud.TipoPruebaId equals tipoPrueba.TipoPruebaId
							select new
							{
								solicitud.SolicitudPruebaId,
								PacienteNombre = paciente.Nombre,
								TipoPruebaNombre = tipoPrueba.Nombre,
								solicitud.FechaSolicitud,
								solicitud.Estado
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 6: Trabajo por técnico
		[HttpGet("mis/pruebas-por-tecnico")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> PruebasPorTecnico()
		{
			var resultado = from solicitud in _context.SolicitudesPrueba
							where solicitud.TecnicoId != null
							join tecnico in _context.Tecnicos on solicitud.TecnicoId equals tecnico.TecnicoId
							group solicitud by tecnico.Nombre into g
							select new
							{
								TecnicoNombre = g.Key,
								TotalPruebas = g.Count(),
								TotalCosto = g.Sum(s => s.CostoFinal)
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 7: Pruebas realizadas por departamento
		[HttpGet("mis/pruebas-por-departamento")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> PruebasPorDepartamento()
		{
			var resultado = from solicitud in _context.SolicitudesPrueba
							join tipoPrueba in _context.TiposPrueba on solicitud.TipoPruebaId equals tipoPrueba.TipoPruebaId
							join departamento in _context.Departamentos on tipoPrueba.DepartamentoId equals departamento.DepartamentoId
							group solicitud by departamento.Nombre into g
							select new
							{
								DepartamentoNombre = g.Key,
								TotalSolicitudes = g.Count(),
								TotalIngresos = g.Sum(s => s.CostoFinal)
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 8: Pacientes con pruebas múltiples
		[HttpGet("mis/pacientes-con-multiples-pruebas")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> PacientesConMultiplesPruebas()
		{
			var resultado = from solicitud in _context.SolicitudesPrueba
							group solicitud by solicitud.PacienteId into g
							where g.Count() > 1
							join paciente in _context.Pacientes on g.Key equals paciente.PacienteId
							select new
							{
								PacienteNombre = paciente.Nombre,
								paciente.Documento,
								TotalPruebas = g.Count(),
								CostoTotal = g.Sum(s => s.CostoFinal)
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 9: Rango de fechas con filtro
		[HttpGet("mis/solicitudes-por-rango-fechas")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> SolicitudesPorRangoFechas([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
		{
			fechaInicio = DateTime.SpecifyKind(fechaInicio, DateTimeKind.Utc);
			fechaFin = DateTime.SpecifyKind(fechaFin, DateTimeKind.Utc);

			var resultado = from solicitud in _context.SolicitudesPrueba
							where solicitud.FechaSolicitud >= fechaInicio && solicitud.FechaSolicitud <= fechaFin
							join paciente in _context.Pacientes on solicitud.PacienteId equals paciente.PacienteId
							join tipoPrueba in _context.TiposPrueba on solicitud.TipoPruebaId equals tipoPrueba.TipoPruebaId
							select new
							{
								solicitud.SolicitudPruebaId,
								PacienteNombre = paciente.Nombre,
								TipoPruebaNombre = tipoPrueba.Nombre,
								solicitud.FechaSolicitud,
								solicitud.CostoFinal
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 10: Comparativa de costos con promedio
		[HttpGet("mis/estadisticas-costos")]
		public async Task<IActionResult> EstadisticasCostos()
		{
			var resultado = from s in _context.SolicitudesPrueba
							join tp in _context.TiposPrueba on s.TipoPruebaId equals tp.TipoPruebaId
							group s by tp.Nombre into g
							select new
							{
								TipoPruebaNombre = g.Key,
								TotalPruebas = g.Count(),
								TotalIngresos = g.Sum(s => s.CostoFinal),
								PromedioCosto = g.Average(s => s.CostoFinal),
								CostoMinimo = g.Min(s => s.CostoFinal),
								CostoMaximo = g.Max(s => s.CostoFinal)
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 11: Distribución de pruebas por estado
		[HttpGet("mis/distribucion-estado-pruebas")]
		public async Task<IActionResult> DistribucionEstadoPruebas()
		{
			var total = await _context.SolicitudesPrueba.CountAsync();

			if (total == 0)
				return Ok(new List<object>());

			var resultado = from s in _context.SolicitudesPrueba
							group s by s.Estado into g
							select new
							{
								Estado = g.Key,
								Cantidad = g.Count(),
								Porcentaje = Math.Round((decimal)g.Count() * 100 / total, 2)
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 12: Técnicos sin asignaciones
		[HttpGet("mis/tecnicos-sin-asignaciones")]
		public async Task<IActionResult> TecnicosSinAsignaciones()
		{
			var conAsignaciones = _context.SolicitudesPrueba
				.Where(sp => sp.TecnicoId != null)
				.Select(sp => sp.TecnicoId);

			var resultado = from t in _context.Tecnicos
							join d in _context.Departamentos on t.DepartamentoId equals d.DepartamentoId
							where !conAsignaciones.Contains(t.TecnicoId)
							select new
							{
								t.TecnicoId,
								TecnicoNombre = t.Nombre,
								DepartamentoNombre = d.Nombre,
								t.Email
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 13: Pruebas pendientes con alertas de retraso
		[HttpGet("mis/pruebas-pendientes-retrasadas")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> PruebasPendientesRetrasadas()
		{
			var diasRetraso = 7;
			var fechaAlerta = DateTime.UtcNow.AddDays(-diasRetraso);

			var resultado = from solicitud in _context.SolicitudesPrueba
							where solicitud.Estado == "PENDIENTE" && solicitud.FechaSolicitud < fechaAlerta
							join paciente in _context.Pacientes on solicitud.PacienteId equals paciente.PacienteId
							join tipoPrueba in _context.TiposPrueba on solicitud.TipoPruebaId equals tipoPrueba.TipoPruebaId
							select new
							{
								solicitud.SolicitudPruebaId,
								PacienteNombre = paciente.Nombre,
								TipoPruebaNombre = tipoPrueba.Nombre,
								solicitud.FechaSolicitud,
								DiasRetraso = (int)(DateTime.UtcNow - solicitud.FechaSolicitud).TotalDays
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}

		// CONSULTA 14: Eficiencia por técnico
		[HttpGet("mis/eficiencia-por-tecnico")]
		[ProducesResponseType(typeof(List<object>), 200)]
		public async Task<IActionResult> EficienciaPorTecnico()
		{
			var resultado = from solicitud in _context.SolicitudesPrueba
							where solicitud.TecnicoId != null
							group solicitud by solicitud.TecnicoId into g
							join tecnico in _context.Tecnicos on g.Key equals tecnico.TecnicoId
							select new
							{
								TecnicoNombre = tecnico.Nombre,
								TotalAsignadas = g.Count(),
								Completadas = g.Count(s => s.Estado == "REALIZADO"),
								Pendientes = g.Count(s => s.Estado == "PENDIENTE"),
								Canceladas = g.Count(s => s.Estado == "CANCELADO"),
								TasaCompletacion = g.Any()
									? (decimal)g.Count(s => s.Estado == "REALIZADO") * 100 / g.Count()
									: 0
							};

			var datos = await resultado.ToListAsync();
			return Ok(datos);
		}
	}
}