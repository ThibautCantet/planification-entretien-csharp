using System.Collections.Generic;

namespace PlanificationEntretien
{
    public interface IEntretienRepository
    {
        IEnumerable<Entretien> FindAll();
        Entretien Save(Entretien @is);
    }
}