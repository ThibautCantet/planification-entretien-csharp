using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.soat.planification_entretien.common.cqrs.application;
using com.soat.planification_entretien.common.cqrs.query;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.recruteur.application_service;
using PlanificationEntretien.recruteur.application_service.application;

namespace PlanificationEntretien.recruteur.infrastructure.controller;

[ApiController]
[Route("/api/recruteure")]
public class RecruteurQueryController : QueryController
{
    public RecruteurQueryController(QueryBusFactory queryBusFactory) : base(queryBusFactory)
    {
        _queryBusFactory.Build();
    }

    [HttpGet("")]
    public Task<IActionResult> ListerExperimentes()
    {
        var queryResponse = base.GetQueryBus().Dispatch<List<RecruteurDetail>>(new ListerRecruteurExperimenteQuery()) as QueryResponse<List<RecruteurDetail>>;
        if (queryResponse.FindFirst(typeof(RecruteursExperimentésListés)) != null)
        {
            var recruteurs = queryResponse
                .Value
                .Select(r => new RecruteurExperimenteResponse(r.Email, r.Competence))
                .ToList();
            return Task.FromResult<IActionResult>(Ok(recruteurs));
        }

        return Task.FromResult<IActionResult>(BadRequest());
    }
}