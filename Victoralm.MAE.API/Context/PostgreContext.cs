using Microsoft.EntityFrameworkCore;
using Victoralm.MAE.API.Models;

namespace Victoralm.MAE.API.Context;

public class PostgreContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public PostgreContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //connect to postgres with connection string from app settings
        var ops = options.UseNpgsql(Configuration.GetConnectionString("PostgreSql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure default schema
        modelBuilder.HasDefaultSchema(Configuration.GetSection("envVariables:env").Value);
        base.OnModelCreating(modelBuilder);
    }

    // DbSets
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Medic> Medics { get; set; }
    public DbSet<MedicalSpecialty> MedicalSpecialties { get; set; }
}
