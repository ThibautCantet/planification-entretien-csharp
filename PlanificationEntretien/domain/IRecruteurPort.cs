namespace PlanificationEntretien.domain;

public interface IRecruteurPort
{
    Recruteur FindByEmail(string email);
    void Save(Recruteur candidat);
}