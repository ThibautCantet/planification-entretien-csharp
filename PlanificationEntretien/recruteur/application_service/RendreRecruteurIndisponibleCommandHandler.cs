using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.entretien.application_service;

public class RendreRecruteurIndisponibleCommandHandler
{
    private readonly  IRecruteurRepository _recruteurRepository;

    public RendreRecruteurIndisponibleCommandHandler(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public void Handle(int recruteurId)
    {
        var recruteur = _recruteurRepository.FindById(recruteurId);
        if (recruteur != null) 
        {
            recruteur.RendreIndisponible();
            _recruteurRepository.Save(recruteur);
        }
    }
}