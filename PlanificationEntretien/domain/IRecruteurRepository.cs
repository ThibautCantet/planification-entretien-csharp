using System.Collections.Generic;

namespace PlanificationEntretien.domain;

public interface IRecruteurRepository
{
    Recruteur FindByEmail(string email);
    void Save(Recruteur recruteur);
    List<Recruteur> FindAll();
}