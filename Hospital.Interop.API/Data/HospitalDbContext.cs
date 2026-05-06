using Hospital.Interop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Interop.API.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Cita> Citas => Set<Cita>();
        public DbSet<Examen> Examenes => Set<Examen>();
        public DbSet<Factura> Facturas => Set<Factura>();
        public DbSet<Medicamento> Medicamentos => Set<Medicamento>();
        public DbSet<Departamento> Departamentos => Set<Departamento>();
        public DbSet<Tecnico> Tecnicos => Set<Tecnico>();
        public DbSet<TipoPrueba> TiposPrueba => Set<TipoPrueba>();
        public DbSet<SolicitudPrueba> SolicitudesPrueba => Set<SolicitudPrueba>();
        public DbSet<ResultadoPrueba> ResultadosPrueba => Set<ResultadoPrueba>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Paciente
            modelBuilder.Entity<Paciente>()
                .HasKey(p => p.PacienteId);

            modelBuilder.Entity<Paciente>()
                .Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Paciente>()
                .Property(p => p.Documento)
                .HasMaxLength(20);

            modelBuilder.Entity<Paciente>()
                .Property(p => p.Telefono)
                .HasMaxLength(15);

            modelBuilder.Entity<Paciente>()
                .Property(p => p.Direccion)
                .HasMaxLength(300);

            modelBuilder.Entity<Paciente>()
                .Property(p => p.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<Paciente>()
                .Property(p => p.Genero)
                .HasMaxLength(1);

            // Cita
            modelBuilder.Entity<Cita>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Cita>()
                .Property(c => c.Hora)
                .HasMaxLength(10);

            modelBuilder.Entity<Cita>()
                .Property(c => c.Departamento)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Cita>()
                .Property(c => c.Estado)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Cita>()
                .Property(c => c.Observaciones)
                .HasMaxLength(500);

            // Examen
            modelBuilder.Entity<Examen>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Examen>()
                .Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Examen>()
                .Property(e => e.Resultado)
                .IsRequired()
                .HasMaxLength(150);

            // Factura
            modelBuilder.Entity<Factura>()
                .HasKey(f => f.FacturaId);

            modelBuilder.Entity<Factura>()
                .Property(f => f.Monto)
                .HasPrecision(10, 2)
                .IsRequired();

            modelBuilder.Entity<Factura>()
                .Property(f => f.Estado)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Factura>()
                .Property(f => f.Concepto)
                .IsRequired()
                .HasMaxLength(200);

            // Medicamento
            modelBuilder.Entity<Medicamento>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Medicamento>()
                .Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Medicamento>()
                .Property(m => m.Dosis)
                .IsRequired()
                .HasMaxLength(100);

            // Departamento
            modelBuilder.Entity<Departamento>()
                .HasKey(d => d.DepartamentoId);

            modelBuilder.Entity<Departamento>()
                .Property(d => d.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            // Tecnico
            modelBuilder.Entity<Tecnico>()
                .HasKey(t => t.TecnicoId);

            modelBuilder.Entity<Tecnico>()
                .Property(t => t.DepartamentoId)
                .IsRequired();

            modelBuilder.Entity<Tecnico>()
                .Property(t => t.Cargo)
                .HasMaxLength(50);

            // TipoPrueba
            modelBuilder.Entity<TipoPrueba>()
                .HasKey(tp => tp.TipoPruebaId);

            modelBuilder.Entity<TipoPrueba>()
                .Property(tp => tp.DepartamentoId)
                .IsRequired();

            // SolicitudPrueba
            modelBuilder.Entity<SolicitudPrueba>()
                .HasKey(sp => sp.SolicitudPruebaId);

            modelBuilder.Entity<SolicitudPrueba>()
                .HasOne(sp => sp.Paciente)
                .WithMany()
                .HasForeignKey(sp => sp.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SolicitudPrueba>()
                .HasOne(sp => sp.TipoPrueba)
                .WithMany()
                .HasForeignKey(sp => sp.TipoPruebaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudPrueba>()
                .HasOne(sp => sp.Tecnico)
                .WithMany()
                .HasForeignKey(sp => sp.TecnicoId)
                .OnDelete(DeleteBehavior.SetNull);

            // ResultadoPrueba
            modelBuilder.Entity<ResultadoPrueba>()
                .HasKey(rp => rp.ResultadoPruebaId);

            modelBuilder.Entity<ResultadoPrueba>()
                .HasOne(rp => rp.SolicitudPrueba)
                .WithMany()
                .HasForeignKey(rp => rp.SolicitudPruebaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}