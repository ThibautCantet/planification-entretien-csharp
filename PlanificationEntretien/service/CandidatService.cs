using PlanificationEntretien.domain;
using PlanificationEntretien.memory;

namespace Planification
{
    public class CandidatService
    {
        private readonly ICandidatRepository _candidatRepository;

        public CandidatService(ICandidatRepository candidatRepository)
        {
            _candidatRepository = candidatRepository;
        }

        public void Save(Candidat candidat)
        {
            _candidatRepository.Save(candidat);
        }
    }
}