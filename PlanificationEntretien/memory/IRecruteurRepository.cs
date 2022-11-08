using PlanificationEntretien.model;

namespace PlanificationEntretien.memory;

public interface IRecruteurRepository
{
    Recruteur FindByEmail(string email);
    void Save(Recruteur candidat);
}