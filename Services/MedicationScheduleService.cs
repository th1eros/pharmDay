using Microsoft.EntityFrameworkCore;
using TGDay.Domain.Models;
using TGDay.Infrastructure.Persistence;

namespace TGDay.Services;

public class MedicationScheduleService
{
    private readonly IDbContextFactory<TGDayDbContext> _contextFactory;

    public MedicationScheduleService(IDbContextFactory<TGDayDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<PatientMedicationLog>> GetActiveSchedulesAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var logs = await context.PatientMedicationLogs
            .AsNoTracking()
            .Where(x => !x.IsArchived)
            .ToListAsync(cancellationToken);

        return logs.OrderByDescending(x => x.ScheduledTime).ToList();
    }

    public async Task<List<string>> GetAllPatientsAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.PatientMedicationLogs
            .AsNoTracking()
            .Where(x => !x.IsArchived)
            .Select(x => x.PatientName)
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<PatientMedicationLog>> GetLogsByPatientAsync(string patientName, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var logs = await context.PatientMedicationLogs
            .AsNoTracking()
            .Where(x => !x.IsArchived && x.PatientName.ToLower() == patientName.ToLower())
            .ToListAsync(cancellationToken);

        return logs.OrderByDescending(x => x.ScheduledTime).ToList();
    }

    public async Task UpdateLogAsync(Guid id, string medicationName, decimal milligrams, decimal weightKg, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var log = await context.PatientMedicationLogs.FindAsync(new object[] { id }, cancellationToken);
        if (log != null)
        {
            log.UpdateDetails(medicationName, milligrams, weightKg);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task AddLogAsync(
        string patientName,
        string medicationName,
        decimal milligrams,
        decimal weightKg,
        DayOfWeek scheduledDay,
        TimeSpan scheduledTime,
        int doseCount,
        CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var log = new PatientMedicationLog(
            patientName,
            medicationName,
            milligrams,
            weightKg,
            scheduledDay,
            scheduledTime,
            doseCount
        );

        await context.PatientMedicationLogs.AddAsync(log, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task ToggleTakenStatusAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var log = await context.PatientMedicationLogs.FindAsync(new object[] { id }, cancellationToken);
        if (log != null)
        {
            log.MarkAsDone();
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task ArchiveLogAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var log = await context.PatientMedicationLogs.FindAsync(new object[] { id }, cancellationToken);
        if (log != null)
        {
            log.Archive();
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RestoreLogAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var log = await context.PatientMedicationLogs.FindAsync(new object[] { id }, cancellationToken);
        if (log != null)
        {
            log.Unarchive();
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<PatientMedicationLog>> GetArchivedSchedulesAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        
        var logs = await context.PatientMedicationLogs
            .AsNoTracking()
            .Where(x => x.IsArchived)
            .ToListAsync(cancellationToken);

        return logs
            .OrderBy(x => x.PatientName)
            .ThenByDescending(x => x.ScheduledTime)
            .ToList();
    }

    public async Task DeleteLogPermanentlyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var log = await context.PatientMedicationLogs.FindAsync(new object[] { id }, cancellationToken);
        if (log != null)
        {
            context.PatientMedicationLogs.Remove(log);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}