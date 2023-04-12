using com.soat.planification_entretien.common.cqrs.application;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.recruteur.domain;
using Candidat = PlanificationEntretien.entretien.domain.Candidat;
using Recruteur = PlanificationEntretien.entretien.domain.Recruteur;

namespace PlanificationEntretien.entretien.infrastructure.controller;

[ApiController]
[Route("/api/entretien")]
public class EntretienCommandController : CommandController
{
    private readonly ValiderEntretienCommandHandler _validerEntretienCommandHandler;
    private readonly ICandidatRepository _candidatRepository;
    private readonly IRecruteurRepository _recruteurRepository;

    public EntretienCommandController(
        ValiderEntretienCommandHandler validerEntretienCommandHandler,
        ICandidatRepository candidatRepository,
        IRecruteurRepository recruteurRepository,
        CommandBusFactory commandBusFactory) : base(commandBusFactory)
    {
        _commandBusFactory.Build();
        _validerEntretienCommandHandler = validerEntretienCommandHandler;
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
        if (_validerEntretienCommandHandler.Handle(new ValiderEntretienCommand(id)))
        {
            return Ok();
        }

        return null;
    }
}