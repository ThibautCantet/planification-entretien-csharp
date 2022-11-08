using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain;
using PlanificationEntretien.memory;

namespace PlanificationEntretien.controller;

[ApiController]
[Route("/api/recruteure")]
public class RecruteurController : ControllerBase
{
    private readonly IRecruteurRepository _recruteurRepository;

    public RecruteurController(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] CreateRecruteurRequest createRecruteurRequest)
    {
        var candidat = new Recruteur(createRecruteurRequest.Language,
            createRecruteurRequest.Email,
            createRecruteurRequest.XP);
        if (!string.IsNullOrEmpty(createRecruteurRequest.Email) && IsValid(createRecruteurRequest.Email)
                                                   && !string.IsNullOrEmpty(createRecruteurRequest.Language)
                                                   && createRecruteurRequest.XP > 0)
        {
            _recruteurRepository.Save(candidat);
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = createRecruteurRequest },
                createRecruteurRequest));
        }
        return Task.FromResult<IActionResult>(BadRequest());
    }


    private static bool IsValid(string email)
    {
        try
        {
            new MailAddress(email);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}