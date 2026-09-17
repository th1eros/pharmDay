namespace TGDay.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using TGDay.Domain.Models;

public sealed class TGDayDbContext : DbContext
{
    public TGDayDbContext(DbContextOptions<TGDayDbContext> options) : base(options) { }

    public DbSet<PatientMedicationLog> PatientMedicationLogs => Set<PatientMedicationLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TGDayDbContext).Assembly);
    }
}