using System;

namespace PlanificationEntretien.domain.entretien;

public class CandidatEvalué
{
    readonly Profil _profil;
    public int Id { get; }
    public string Email { get; }

    public string Langage => _profil.Language;

    public int ExperienceEnAnnees => _profil.AnnéeExperience;

    public CandidatEvalué(int id, string language, string email, int experienceEnAnnees)
    {
        Id = id;
        Email = email;
        _profil = new Profil(language, experienceEnAnnees);
    }

    protected bool Equals(CandidatEvalué? other)
    {
        return Id == other.Id && Email == other.Email
            && Langage == other.Langage
            &&  ExperienceEnAnnees == other.ExperienceEnAnnees;;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((CandidatEvalué)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Email, Langage, ExperienceEnAnnees);
    }
}