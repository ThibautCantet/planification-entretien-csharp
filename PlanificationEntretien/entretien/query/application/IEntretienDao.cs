using System.Collections.Generic;
using PlanificationEntretien.entretien.application_service;

namespace PlanificationEntretien.entretien.domain;

public interface IEntretienDao
{
    IEnumerable<IEntretien> FindAll();
}