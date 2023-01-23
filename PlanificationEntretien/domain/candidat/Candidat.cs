using System;

namespace PlanificationEntretien.domain.candidat;

public class Candidat : IEquatable<Candidat>
{
    public int Id { get; }
    public string Language { get; }
    private CandidatEmail _candidatEmail;
    public string Email => _candidatEmail.Adresse;
    private readonly Experience _experience;
    public int ExperienceEnAnnees => _experience.Annee;

    public Candidat(int id, string language, string email, int? experienceEnAnnees)
    {
        if (string.IsNullOrEmpty(language))
        {
            throw new ArgumentException();
        }

        Id = id;
        Language = language;
        _candidatEmail = new CandidatEmail(email);
        _experience = new Experience(experienceEnAnnees);
    }

    public Candidat(string language, string email, int? experienceEnAnnees) : this(0, language, email, experienceEnAnnees)
    {
    }

    public bool Equals(Candidat? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Language == other.Language && Email == other.Email && ExperienceEnAnnees == other.ExperienceEnAnnees;
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
        return HashCode.Combine(Language, Email, ExperienceEnAnnees);
    }
}