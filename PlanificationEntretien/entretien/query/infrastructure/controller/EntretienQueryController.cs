using System.Collections.Generic;
using System.Linq;
using com.soat.planification_entretien.common.cqrs.application;
using com.soat.planification_entretien.common.cqrs.query;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.query.application;

namespace PlanificationEntretien.entretien.infrastructure.controller;

[ApiController]
[Route("/api/entretien")]
public class EntretienQueryController : QueryController
{

    public EntretienQueryController(QueryBusFactory queryBusFactory) : base(queryBusFactory)
    {
        _queryBusFactory.Build();
    }

    public IActionResult Lister()
    {
        var queryResponse = base.GetQueryBus().Dispatch<IEnumerable<IEntretien>>(new ListerEntretienQuery()) as QueryResponse<IEnumerable<IEntretien>>;
        if (queryResponse.FindFirst(typeof(EntretiensListés)) != null)
        {
            var entretiens = queryResponse
                .Value
                .Select(entretien => new EntretienResponse(entretien.EmailCandidat(),
                    entretien.EmailRecruteur(),
                    entretien.Horaire(),
                    entretien.Status()))
                .ToList();
            return Ok(entretiens);
        }

        return BadRequest();
    }
}