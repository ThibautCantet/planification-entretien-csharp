using PlanificationEntretien.Recruteur.Domain;

namespace PlanificationEntretien.Recruteur.ApplicationService;

public class RendreRecruteurIndisponible
{
    private readonly  IRecruteurRepository _recruteurRepository;

    public RendreRecruteurIndisponible(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public void Execute(int recruteurId)
    {
        var recruteur = _recruteurRepository.FindById(recruteurId);
        if (recruteur != null) 
        {
            recruteur.RendreIndisponible();
            _recruteurRepository.Save(recruteur);
        }
    }
}