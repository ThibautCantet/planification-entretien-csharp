using System;
using System.Collections.Generic;
using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.query.application;

namespace PlanificationEntretien.entretien.application_service;

public class ListerEntretienQueryHandler : IQueryHandler<ListerEntretienQuery, QueryResponse<IEnumerable<IEntretien>>, IEnumerable<IEntretien>>
{
    private readonly IEntretienDao _entretienDao;

    public ListerEntretienQueryHandler(IEntretienDao entretienDao)
    {
        _entretienDao = entretienDao;
    }
    
    public QueryResponse<IEnumerable<IEntretien>> Handle(ListerEntretienQuery query)
    {
        return new QueryResponse<IEnumerable<IEntretien>>(_entretienDao.FindAll(), new EntretiensListés());
    }

    public Type ListenTo()
    {
        return typeof(ListerEntretienQuery);
    }
}