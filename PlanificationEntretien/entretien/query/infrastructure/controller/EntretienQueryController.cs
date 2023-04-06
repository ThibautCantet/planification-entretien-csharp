using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.entretien.application_service;

namespace PlanificationEntretien.entretien.infrastructure.controller;

[ApiController]
[Route("/api/entretien")]
public class EntretienQueryController : ControllerBase
{
    private readonly ListerEntretienQueryHandler _listerEntretienQueryHandler;

    public EntretienQueryController(ListerEntretienQueryHandler listerEntretienQueryHandler)
    {
        _listerEntretienQueryHandler = listerEntretienQueryHandler;
    }

    public IActionResult Lister()
    {
        var entretiens = _listerEntretienQueryHandler.Handle(new ListerEntretienQuery())
            .Select(entretien => new EntretienResponse(entretien.EmailCandidat(),
                entretien.EmailRecruteur(),
                entretien.Horaire(),
                entretien.Status()))
            .ToList();
        return Ok(entretiens);
    }
}