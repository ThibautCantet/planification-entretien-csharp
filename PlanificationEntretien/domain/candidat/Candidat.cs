using System;

namespace PlanificationEntretien.domain.candidat;

public class Candidat : IEquatable<Candidat>
{
    private readonly CandidatId _id;
    public int Id => _id.Valeur;
    private readonly Langage _language;
    public string Language => _language.Nom;
    private CandidatEmail _candidatEmail;
    public string Email => _candidatEmail.Adresse;
    private readonly Experience _experience;
    public int ExperienceEnAnnees => _experience.Annee;

    public Candidat(int id, string language, string email, int? experienceEnAnnees)
    {
        _id = new CandidatId(id);
        _language = new Langage(language);
        _candidatEmail = new CandidatEmail(email);
        _experience = new Experience(experienceEnAnnees);
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