using com.soat.planification_entretien.common.cqrs.application;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.command.domain;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.recruteur.domain;
using Candidat = PlanificationEntretien.entretien.domain.Candidat;
using Recruteur = PlanificationEntretien.entretien.domain.Recruteur;

namespace PlanificationEntretien.entretien.infrastructure.controller;

[ApiController]
[Route("/api/entretien")]
public class EntretienCommandController : CommandController
{
    private readonly ICandidatRepository _candidatRepository;
    private readonly IRecruteurRepository _recruteurRepository;

    public EntretienCommandController(ICandidatRepository candidatRepository,
        IRecruteurRepository recruteurRepository,
        CommandBusFactory commandBusFactory) : base(commandBusFactory)
    {
        _commandBusFactory.Build();
        _candidatRepository = candidatRepository;
        _recruteurRepository = recruteurRepository;
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateEntretienRequest createOfferRequest)
    {
        var candidat = _candidatRepository.FindById(createOfferRequest.IdCandidat);
        var recruteur = _recruteurRepository.FindById(createOfferRequest.IdRecruteur);
        var events = base.GetCommandBus().Dispatch(new PlanifierEntretienCommand(
            new Candidat(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteCandidat,
            new Recruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteRecruteur));
        var entretienCrée = events.FindFirst<EntretienCréé>();
        if (entretienCrée != null)
        {
            var response = new CreateEntretienResponse(entretienCrée.EntretienId, candidat.Email, recruteur.Email,
                createOfferRequest.DisponibiliteCandidat);
            return CreatedAtAction("Create", new { id = createOfferRequest }, response);
        }

        return BadRequest();
    }

    public IActionResult Valider(int id)
    {
        var commandResponse = base.GetCommandBus().Dispatch(new ValiderEntretienCommand(id));
        if (commandResponse.FindAny<EntretienValidé>())
        {
            return Ok();
        }

        return null;
    }

    public IActionResult Annuler(int id)
    {
        var commandResponse = base.GetCommandBus().Dispatch(new AnnulerEntretienCommand(id));
        if (commandResponse.FindAny<EntretienAnnulé>())
        {
            return Ok();
        }

        return null;
    }
}