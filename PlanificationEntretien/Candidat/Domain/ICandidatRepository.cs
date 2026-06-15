namespace PlanificationEntretien.Candidat.Domain;

public interface ICandidatRepository
{
    Candidat FindById(int id);
    Candidat FindByEmail(string email);
    int Save(Candidat candidat);
    int Next();
}