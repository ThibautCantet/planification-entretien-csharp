using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.memory;

public interface IRecruteurRepository
{
    Recruteur FindByEmail(string email);
    void Save(Recruteur candidat);
}