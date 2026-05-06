using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hospital.Interop.API.Data
{
    public class HospitalDbContextFactory : IDesignTimeDbContextFactory<HospitalDbContext>
    {
        public HospitalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=proyecto_123;Username=postgres;Password=123456789"
            );

            return new HospitalDbContext(optionsBuilder.Options);
        }
    }
}