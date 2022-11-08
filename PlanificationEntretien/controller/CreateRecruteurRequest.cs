namespace PlanificationEntretien.controller;

public record CreateRecruteurRequest()
{
    public CreateRecruteurRequest(string language, string email, int? xp) : this()
    {
        Language = language;
        Email = email;
        XP = xp;
    }

    public string Language { get; }
    public string Email { get; }
    public int? XP { get; }
}