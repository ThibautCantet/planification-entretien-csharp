using System.Collections.Generic;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public class ListerEntretienQueryHandler
{
    private readonly IEntretienDao _entretienDao;

    public ListerEntretienQueryHandler(IEntretienDao entretienDao)
    {
        _entretienDao = entretienDao;
    }
    
    public IEnumerable<IEntretien> Handle(ListerEntretienQuery query)
    {
        return _entretienDao.FindAll();
    }
}