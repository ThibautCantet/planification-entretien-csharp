namespace PlanificationEntretien.domain;

public interface ICandidatRepository
{
    Candidat FindByEmail(string email);
    void Save(Candidat candidat);
}