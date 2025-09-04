using System;

namespace PlanificationEntretien.domain.entretien;

public class RecruteurAssigné
{
    public int Id { get; }
    public string Email { get; }
    public Profil Profil { get; }

    public RecruteurAssigné(int id, string language, string email, int experienceEnAnnees)
    {
        Id = id;
        Email = email;
        Profil = new Profil(language, experienceEnAnnees);
    }

    public bool EstCompatible(CandidatEvalué candidat)
    {
        return Profil.EstCompatible(candidat);
    }

    protected bool Equals(RecruteurAssigné? other)
    {
        return Id == other.Id && Email == other.Email && Profil.Equals(other.Profil);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((RecruteurAssigné)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Profil.AnnéeExperience, Profil.Language);
    }
}