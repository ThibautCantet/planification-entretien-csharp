namespace PlanificationEntretien.entretien.domain;

public record Candidat() {
    public int Id { get; }
    public Profil Profil { get; }

    public Candidat(
        int id,
        string language,
        string email,
        int experienceEnAnnees) : this()
    {
        Id = id;
        Email = email;
        Profil = new Profil(language, experienceEnAnnees);
    }

    public string Language => Profil.Language;
    public string Email { get; }
    public int ExperienceEnAnnees => Profil.ExperienceEnAnnees;
}