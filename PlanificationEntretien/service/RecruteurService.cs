using PlanificationEntretien.domain;
using PlanificationEntretien.memory;

namespace Planification
{
    public class RecruteurService
    {
        private readonly IRecruteurRepository _recruteurRepository;

        public RecruteurService(IRecruteurRepository recruteurRepository)
        {
            _recruteurRepository = recruteurRepository;
        }

        public void Save(Recruteur recruteur)
        {
            _recruteurRepository.Save(recruteur);
        }
    }
}