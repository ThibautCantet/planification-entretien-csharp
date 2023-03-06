using System.Collections.Generic;

namespace PlanificationEntretien.recruteur.application_service.application;

public interface IRecruteurDao
{
    List<IRecruteurDetail> Find10AnsExperience();
}