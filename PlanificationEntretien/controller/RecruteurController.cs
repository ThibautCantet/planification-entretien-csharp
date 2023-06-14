using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.model;
using PlanificationEntretien.repository;

namespace PlanificationEntretien.controller;

[ApiController]
[Route("/api/recruteure")]
public class RecruteurController : ControllerBase
{
    private readonly RecruteurRepository _recruteurRepository;

    public RecruteurController(RecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] RecruteurDto recruteurDto)
    {
        var candidat = new Recruteur(recruteurDto.Language,
            recruteurDto.Email,
            recruteurDto.XP);
        if (!string.IsNullOrEmpty(recruteurDto.Email) && IsValid(recruteurDto.Email)
                                                   && !string.IsNullOrEmpty(recruteurDto.Language)
                                                   && recruteurDto.XP > 0)
        {
            _recruteurRepository.Save(candidat);
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = recruteurDto },
                recruteurDto));
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