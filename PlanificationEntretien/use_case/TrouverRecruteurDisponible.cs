using System;
using System.Linq;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.use_case;

public class TrouverRecruteurDisponible
{
    private readonly IRecruteurAssigneDao _assigneDao;

    public TrouverRecruteurDisponible(IRecruteurAssigneDao assigneDao)
    {
        _assigneDao = assigneDao;
    }

    public RecruteurAssigné? Execute(CandidatEvalué candidat)
    {
        var recruteursDisponibles = _assigneDao.FindByDisponibles();
        return recruteursDisponibles
            .FirstOrDefault(r => r.EstCompatible(candidat));
    }
}