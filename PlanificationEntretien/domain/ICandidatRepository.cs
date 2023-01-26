namespace PlanificationEntretien.domain;

public interface ICandidatRepository
{
    Candidat FindByEmail(string email);
    int Save(Candidat candidat);
}