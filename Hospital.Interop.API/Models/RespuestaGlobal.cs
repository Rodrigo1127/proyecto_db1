using System.Collections.Generic;

namespace Hospital.Interop.API.Models
{
    public class RespuestaGlobal
    {
        public Paciente? Paciente { get; set; }

        public List<Examen> Examenes { get; set; } = new();

        public List<Cita> Citas { get; set; } = new();

        public List<Medicamento> Medicamentos { get; set; } = new();

        public List<Factura> Facturas { get; set; } = new();

        public List<string> Advertencias { get; set; } = new();
    }
}