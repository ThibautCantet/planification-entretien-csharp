using PlanificationEntretien.domain;

namespace PlanificationEntretien.memory
{
    public interface ICandidatRepository
    {
        Candidat FindByEmail(string email);
        void Save(Candidat candidat);
    }
}