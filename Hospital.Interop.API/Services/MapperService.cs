using Hospital.Interop.API.Models;
using Hospital.Interop.API.Models.DTOs;

namespace Hospital.Interop.API.Services
{
    public class MapperService
    {
        /// <summary>
        /// Convierte un Paciente a PacienteDTOSinId (sin ID)
        /// </summary>
        public PacienteDTOSinId MapearPacienteSinId(Paciente paciente)
        {
            return new PacienteDTOSinId
            {
                Nombre = paciente.Nombre,
                Documento = paciente.Documento,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
                Email = paciente.Email,
                FechaNacimiento = paciente.FechaNacimiento,
                Genero = paciente.Genero
            };
        }

        /// <summary>
        /// Convierte un Paciente a PacienteDTOConId (con ID)
        /// </summary>
        public PacienteDTOConId MapearPacienteConId(Paciente paciente)
        {
            return new PacienteDTOConId
            {
                PacienteId = paciente.PacienteId,
                Nombre = paciente.Nombre,
                Documento = paciente.Documento,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
                Email = paciente.Email,
                FechaNacimiento = paciente.FechaNacimiento,
                Genero = paciente.Genero
            };
        }

        /// <summary>
        /// Convierte PacienteCompleto a PacienteCompletoDTO (sin IDs)
        /// </summary>
        public PacienteCompletoDTO MapearPacienteCompletSinId(PacienteCompleto pacienteCompleto)
        {
            return new PacienteCompletoDTO
            {
                Paciente = MapearPacienteSinId(pacienteCompleto.Paciente),
                Examenes = pacienteCompleto.Examenes.Cast<object>().ToList(),
                Citas = pacienteCompleto.Citas.Cast<object>().ToList(),
                Facturas = pacienteCompleto.Facturas.Cast<object>().ToList()
            };
        }

        /// <summary>
        /// Convierte PacienteCompleto a PacienteCompletoConIdDTO (con IDs)
        /// </summary>
        public PacienteCompletoConIdDTO MapearPacienteCompletoConId(PacienteCompleto pacienteCompleto)
        {
            return new PacienteCompletoConIdDTO
            {
                Paciente = MapearPacienteConId(pacienteCompleto.Paciente),
                Examenes = pacienteCompleto.Examenes.Cast<object>().ToList(),
                Citas = pacienteCompleto.Citas.Cast<object>().ToList(),
                Facturas = pacienteCompleto.Facturas.Cast<object>().ToList()
            };
        }
    }
}
