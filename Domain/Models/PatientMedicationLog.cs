namespace TGDay.Domain.Models;

public class PatientMedicationLog
{
    public Guid Id { get; private set; }
    public string PatientName { get; private set; } = string.Empty;
    public string MedicationName { get; private set; } = string.Empty;
    public decimal Milligrams { get; private set; }
    public decimal WeightKg { get; private set; }
    public DayOfWeek ScheduledDay { get; private set; }
    public TimeSpan ScheduledTime { get; private set; }
    public int DoseCount { get; private set; }
    public bool IsDone { get; private set; }
    public bool IsArchived { get; private set; }
    public DateTime? TakenAt { get; private set; }

    private PatientMedicationLog() { }

    public PatientMedicationLog(
        string patientName,
        string medicationName,
        decimal milligrams,
        decimal weightKg,
        DayOfWeek scheduledDay,
        TimeSpan scheduledTime,
        int doseCount)
    {
        Id = Guid.NewGuid();
        PatientName = patientName;
        MedicationName = medicationName;
        Milligrams = milligrams;
        WeightKg = weightKg;
        ScheduledDay = scheduledDay;
        ScheduledTime = scheduledTime;
        DoseCount = doseCount;
        IsDone = false;
        IsArchived = false;
    }

    public void UpdateDetails(string medicationName, decimal milligrams, decimal weightKg)
    {
        MedicationName = medicationName;
        Milligrams = milligrams;
        WeightKg = weightKg;
    }

    public void MarkAsDone()
    {
        IsDone = true;
        TakenAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        IsArchived = true;
    }
    public void Unarchive()
    {
        IsArchived = false;
    }
}