using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Planification;
using PlanificationEntretien.controller;
using PlanificationEntretien.domain;

[ApiController]
[Route("/api/entretien")]
public class EntretienController : ControllerBase
{
    private readonly IEntretienRepository _entretienRepository;
    private readonly EntretienService _entretienService;
    private readonly IMapper _mapper;

    public EntretienController(IEntretienRepository entretienRepository, IMapper mapper, EntretienService entretienService)
    {
        _entretienRepository = entretienRepository;
        _mapper = mapper;
        _entretienService = entretienService;
    }

    
    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] CreateEntretienRequest createOfferRequest)
    {
        var result = _entretienService.Planifier(createOfferRequest.EmailCandidat, createOfferRequest.DisponibiliteCandidat,
            createOfferRequest.EmailRecruteur, createOfferRequest.DisponibiliteRecruteur);
        if (result)
        {
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = createOfferRequest }, createOfferRequest));
        }
        else
        {
            return Task.FromResult<IActionResult>(BadRequest());
        }
    }
}