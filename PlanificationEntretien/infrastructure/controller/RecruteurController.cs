using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain;
using PlanificationEntretien.use_case;

namespace PlanificationEntretien.infrastructure.controller;

[ApiController]
[Route("/api/recruteure")]
public class RecruteurController : ControllerBase
{
    private readonly CreerRecruteur _creerRecruteur;

    public RecruteurController(CreerRecruteur creerRecruteur)
    {
        _creerRecruteur = creerRecruteur;
    }

    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] CreateRecruteurRequest createRecruteurRequest)
    {
        var recruteur = new Recruteur(createRecruteurRequest.Language,
            createRecruteurRequest.Email,
            createRecruteurRequest.XP);
        if (_creerRecruteur.Execute(recruteur))
        {
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = createRecruteurRequest },
                createRecruteurRequest));
        }
        return Task.FromResult<IActionResult>(BadRequest());
    }
}