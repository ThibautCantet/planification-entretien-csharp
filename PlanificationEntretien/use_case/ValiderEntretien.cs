using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.use_case;

public class ValiderEntretien
{
    private readonly IEntretienRepository _entretienRepository;

    public ValiderEntretien(IEntretienRepository entretienRepository)
    {
        _entretienRepository = entretienRepository;
    }

    public void Execute(int entretienId)
    {
        var entretien = _entretienRepository.FindById(entretienId);
        entretien.Valider();
        _entretienRepository.Save(entretien);
    }
}