using System;

namespace PlanificationEntretien.entretien.domain;

public record Recruteur()
{
    public int Id { get; }
    private Profil _profil;

    public Recruteur(
        int id,
        string language,
        string email,
        int experienceEnAnnees) : this()
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
}