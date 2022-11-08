using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.memory;

public interface ICandidatRepository
{
    Candidat FindByEmail(string email);
    void Save(Candidat candidat);
}