using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace Planification
{
    public class InMemoryEntretienRepository : EntretienRepository
    {
        private Dictionary<Candidat, Entretien> _entretiens = new Dictionary<Candidat, PlanificationEntretien.domain.Entretien>();
        public Entretien FindByCandidat(Candidat candidat)
        {
            return _entretiens[candidat];
        }
    }
}