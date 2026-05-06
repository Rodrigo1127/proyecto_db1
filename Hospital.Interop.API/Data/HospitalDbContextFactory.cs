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
                "Host=trolley.proxy.rlwy.net;Port=52558;Database=railway;Username=postgres;Password=FbtiVlVhxQIMhPYVEldwwEjnqjNtKKjr;SSL Mode=Require;Trust Server Certificate=true"
            );

            return new HospitalDbContext(optionsBuilder.Options);
        }
    }
}