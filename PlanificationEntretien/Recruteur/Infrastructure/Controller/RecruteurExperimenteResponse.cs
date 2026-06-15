namespace PlanificationEntretien.Recruteur.Infrastructure.Controller;

public record RecruteurExperimenteResponse(string Email, string Detail)
{
    public RecruteurExperimenteResponse(string email, string techno, int xp) : this(email,  xp + " années en " + techno)
    {
    }
};