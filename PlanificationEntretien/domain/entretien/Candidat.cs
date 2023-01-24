using System;

namespace PlanificationEntretien.domain.entretien;

public class Candidat
{
    public int Id { get; }
    public Profil Profil { get; }

    public Candidat(
        int id,
        string language,
        string email,
        int experienceEnAnnees)
    {
        Id = id;
        Email = email;
        Profil = new Profil(language, experienceEnAnnees);
    }

    public string Language => Profil.Language;
    public string Email { get; }
    public int ExperienceEnAnnees => Profil.ExperienceEnAnnees;

    protected bool Equals(Candidat other)
    {
        return Id == other.Id && Profil.Equals(other.Profil) && Email == other.Email;
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
        return HashCode.Combine(Id, Profil, Email);
    }
}