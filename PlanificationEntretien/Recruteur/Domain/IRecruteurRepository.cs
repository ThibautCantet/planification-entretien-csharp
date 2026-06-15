using System.Collections.Generic;

namespace PlanificationEntretien.Recruteur.Domain;

public interface IRecruteurRepository
{
    Recruteur FindById(int id);
    Recruteur FindByEmail(string email);
    int Save(Recruteur recruteur);
    List<Recruteur> FindAll();
    int Next();
}