namespace PlanificationEntretien.recruteur.application_service.application;

public record RecruteurDetail(
    int Id,
    string Competence,
    string Email)
{
    public RecruteurDetail(int id, string language, int experienceInYears, string email) : this(id, string.Format("{0} années en {1}", experienceInYears, language), email) {
    }
}