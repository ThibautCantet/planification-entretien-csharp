using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.use_case;

public class AnnulerEntretien
{
    private readonly IEntretienRepository _entretienRepository;

    public AnnulerEntretien(IEntretienRepository entretienRepository)
    {
        _entretienRepository = entretienRepository;
    }

    public void Execute(int entretienId)
    {
        var entretien = _entretienRepository.FindById(entretienId);
        entretien.Annuler();
        _entretienRepository.Save(entretien);
    }
}