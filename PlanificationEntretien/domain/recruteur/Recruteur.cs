using System;

namespace PlanificationEntretien.domain.recruteur;

public class Recruteur : IEquatable<Recruteur>
{
    public int Id { get; }
    public string Language { get; }
    public string Email { get; }
    public int ExperienceEnAnnees { get; }

    public Recruteur(int id, string language, string email, int? experienceEnAnnees)
    {
        Id = new RecruteurId(id).Value;
        Email = Shared.Email.EmailRecruteur(email).Value;
        var profil = new Profil(language, experienceEnAnnees);
        Language = profil.Langage;
        ExperienceEnAnnees = profil.ExperienceEnAnnees;
    }

    public Recruteur(string language, string email, int? experienceEnAnnees) : this(0, language, email,
        experienceEnAnnees)
    {
    }

    public bool Equals(Recruteur other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Language == other.Language && Email == other.Email && ExperienceEnAnnees == other.ExperienceEnAnnees;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Recruteur)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Language, Email, ExperienceEnAnnees);
    }
}