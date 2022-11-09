namespace PlanificationEntretien.domain;

public interface ICandidatPort
{
    Candidat FindByEmail(string email);
    void Save(Candidat candidat);
}