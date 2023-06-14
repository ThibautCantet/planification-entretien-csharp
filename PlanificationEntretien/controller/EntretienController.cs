using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.service;

namespace PlanificationEntretien.controller;

[ApiController]
[Route("/api/entretien")]
public class EntretienController : ControllerBase
{
    private readonly EntretienService _entretienService;

    public EntretienController(EntretienService entretienService)
    {
        _entretienService = entretienService;
    }

    
    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] EntretienDto entretienDto)
    {
        var result = _entretienService.Planifier(entretienDto.EmailCandidat, entretienDto.DisponibiliteCandidat,
            entretienDto.EmailRecruteur, entretienDto.DisponibiliteRecruteur);
        if (result)
        {
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = entretienDto }, entretienDto));
        }
        else
        {
            return Task.FromResult<IActionResult>(BadRequest());
        }
    }
}