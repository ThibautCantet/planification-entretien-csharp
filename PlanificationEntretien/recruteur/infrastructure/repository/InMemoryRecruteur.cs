using PlanificationEntretien.recruteur.application_service.application;

namespace PlanificationEntretien.recruteur.infrastructure.repository;

public record InMemoryRecruteur(int Id, string Language, string RecruteurEmail, int? ExperienceEnAnnees, bool EstDisponible) : IRecruteurDetail
{
    public string Competence()
    {
        return ExperienceEnAnnees + " années en " + Language;
    }

    public string Email()
    {
        return RecruteurEmail;
    }
}