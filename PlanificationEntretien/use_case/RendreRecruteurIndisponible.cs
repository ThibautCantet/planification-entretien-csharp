using PlanificationEntretien.domain.recruteur;

namespace PlanificationEntretien.use_case;

public class RendreRecruteurIndisponible
{
    private readonly IRecruteurRepository _recruteurRepository;

    public RendreRecruteurIndisponible(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }
    
    public void Execute(string recruteurEmail)
    {
        var recruteur = _recruteurRepository.FindByEmail(recruteurEmail);
        recruteur.RendreIndisponible();
        _recruteurRepository.Save(recruteur);
    }
}