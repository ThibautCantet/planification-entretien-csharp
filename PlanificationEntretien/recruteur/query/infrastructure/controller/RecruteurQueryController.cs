using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.recruteur.application_service;

namespace PlanificationEntretien.recruteur.infrastructure.controller;

[ApiController]
[Route("/api/recruteure")]
public class RecruteurQueryController : ControllerBase
{
    private readonly ListerRecruteurExperimenteQueryHandler _listerRecruteurExperimenteQueryHandler;

    public RecruteurQueryController(ListerRecruteurExperimenteQueryHandler listerRecruteurExperimenteQueryHandler)
    {
        _listerRecruteurExperimenteQueryHandler = listerRecruteurExperimenteQueryHandler;
    }

    [HttpGet("")]
    public Task<IActionResult> ListerExperimentes()
    {
        var recruteurs = _listerRecruteurExperimenteQueryHandler.Handle(new ListerRecruteurExperimenteQuery())
            .Select(r => new RecruteurExperimenteResponse(r.Email, r.Competence))
            .ToList();
        return Task.FromResult<IActionResult>(Ok(recruteurs));
    }
}