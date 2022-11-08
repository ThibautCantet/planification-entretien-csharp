namespace PlanificationEntretien.domain;

public interface IRecruteurRepository
{
    Recruteur FindByEmail(string email);
    void Save(Recruteur candidat);
}