using PlanificationEntretien.recruteur.application_service;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.entretien.application_service;

public class RendreRecruteurIndisponibleCommandHandler
{
    private readonly  IRecruteurRepository _recruteurRepository;

    public RendreRecruteurIndisponibleCommandHandler(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public void Handle(RendreRecruteurIndisponibleCommand command)
    {
        var recruteur = _recruteurRepository.FindById(command.RecruteurId);
        if (recruteur != null) 
        {
            recruteur.RendreIndisponible();
            _recruteurRepository.Save(recruteur);
        }
    }
}