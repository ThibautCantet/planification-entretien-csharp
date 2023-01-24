using System;

namespace PlanificationEntretien.domain.entretien;

public class Recruteur
{
    public int Id { get; }
    private Profil _profil;

    public Recruteur(
        int id,
        string language,
        string email,
        int experienceEnAnnees)
    {
        Id = id;
        Email = email;
        _profil = new Profil(language, experienceEnAnnees);
    }

    public string Language => _profil.Language;
    public string Email { get; }
    public int ExperienceEnAnnees => _profil.ExperienceEnAnnees;

    public bool EstCompatible(Candidat candidat)
    {
        return _profil.EstCompatible(candidat.Profil);
    }

    protected bool Equals(Recruteur other)
    {
        return _profil.Equals(other._profil) && Id == other.Id && Email == other.Email;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Recruteur)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_profil, Id, Email);
    }
}