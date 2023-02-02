using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.entretien.application_service;

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