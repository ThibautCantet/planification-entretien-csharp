using PlanificationEntretien.domain.entretien;
using Shared;

namespace PlanificationEntretien.use_case;

public class ValiderEntretien
{
    private readonly IEntretienRepository _entretienRepository;

    public ValiderEntretien(IEntretienRepository entretienRepository)
    {
        _entretienRepository = entretienRepository;
    }

    public Event Execute(int entretienId)
    {
        var entretien = _entretienRepository.FindById(entretienId);
        var result = entretien.Valider();
        _entretienRepository.Save(entretien);

        return result;
    }
}