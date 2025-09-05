using System.Collections.Generic;

namespace PlanificationEntretien.domain.entretien;

public interface IRecruteurAssigneDao
{
    RecruteurAssigné FindById(int id);

    List<RecruteurAssigné> FindByDisponibles();
}