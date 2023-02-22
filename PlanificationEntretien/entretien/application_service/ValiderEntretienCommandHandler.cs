using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public class ValiderEntretienCommandHandler
{
    private readonly IEntretienRepository _entretienRepository;

    public ValiderEntretienCommandHandler(IEntretienRepository entretienRepository)
    {
        _entretienRepository = entretienRepository;
    }

    public bool Handle(int id)
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