using System;

namespace PlanificationEntretien.domain.entretien;

public class Candidat
{
    public int Id { get; }
    public string Language { get; }
    public string Email { get; }
    public int ExperienceEnAnnees { get; }

    public Candidat(
        int id,
        string language,
        string email,
        int experienceEnAnnees)
    {
        Id = id;
        Language = language;
        Email = email;
        ExperienceEnAnnees = experienceEnAnnees;
    }

    protected bool Equals(Candidat other)
    {
        return Id == other.Id && Language == other.Language && Email == other.Email && ExperienceEnAnnees == other.ExperienceEnAnnees;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Candidat)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Language, Email, ExperienceEnAnnees);
    }
}