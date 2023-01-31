using System;

namespace PlanificationEntretien.domain.recruteur;

public class Recruteur : IEquatable<Recruteur>
{
    public int Id { get; }
    public string Language => _language.Nom;
    private readonly Langage _language;
    private RecruteurEmail _recruteurEmail;
    public string Email => _recruteurEmail.Adresse;
    private Experience _experience;
    public int ExperienceEnAnnees => _experience.Annee;
    public bool EstDisponible { get; private set; }

    public Recruteur(int id, string language, string email, int? experienceEnAnnees, bool estDisponible)
    {
        if (string.IsNullOrEmpty(language))
        {
            throw new ArgumentException();
        }

        Id = id;
        _language = new Langage(language);
        _recruteurEmail = new RecruteurEmail(email);
        _experience = new Experience(experienceEnAnnees.GetValueOrDefault(-1));
        EstDisponible = estDisponible;
    }

    public Recruteur(string language, string email, int? experienceEnAnnees) : this(0, language, email,
        experienceEnAnnees, true)
    {
    }
    
    public Recruteur(int id, string language, string email, int? experienceEnAnnees) : this(id, language, email,
        experienceEnAnnees, true)
    {
    }

    public void RendreIndisponible()
    {
        EstDisponible = false;
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