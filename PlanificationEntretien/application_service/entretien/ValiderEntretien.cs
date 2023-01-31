using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.application_service.entretien;

public class ValiderEntretien
{
    private readonly IEntretienRepository _entretienRepository;

    public ValiderEntretien(IEntretienRepository entretienRepository)
    {
        _entretienRepository = entretienRepository;
    }

    public bool Execute(int id)
    {
        var entretien = _entretienRepository.FindById(id);
        if (entretien != null)
        {
            entretien.Valider();
            _entretienRepository.Save(entretien);
            return true;
        }

        return false;
    }
}