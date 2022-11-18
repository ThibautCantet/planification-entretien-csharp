namespace PlanificationEntretien.infrastructure.memory;

public record InMemoryRecruteur
{
    public string Language { get; }
    public string Email { get; }
    public int ExperienceEnAnnees { get; }

    public InMemoryRecruteur(string language, string email, int? experienceEnAnnees)
    {
        Language = language;
        Email = email;
        ExperienceEnAnnees = experienceEnAnnees.GetValueOrDefault(-1);
    }
}