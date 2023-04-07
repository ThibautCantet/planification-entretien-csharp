using System.Collections.Generic;

namespace PlanificationEntretien.recruteur.application_service.application;

public interface IRecruteurDao
{
    List<RecruteurDetail> Find10AnsExperience();
    void AddExperimente(RecruteurDetail recruteurDetail);
}