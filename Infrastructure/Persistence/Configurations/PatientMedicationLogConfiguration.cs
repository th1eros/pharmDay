namespace TGDay.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGDay.Domain.Models;

public sealed class PatientMedicationLogConfiguration : IEntityTypeConfiguration<PatientMedicationLog>
{
    public void Configure(EntityTypeBuilder<PatientMedicationLog> builder)
    {
        builder.ToTable("patient_medication_logs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PatientName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.MedicationName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Milligrams)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.WeightKg)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.ScheduledDay)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.ScheduledTime)
            .IsRequired();

        builder.HasIndex(x => new { x.PatientName, x.ScheduledDay });
    }
}