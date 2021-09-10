using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace Planification
{
    public class InMemoryEntretienRepository : EntretienRepository
    {
        private Dictionary<Candidat, Entretien> _entretiens = new Dictionary<Candidat, Entretien>();
        public Entretien FindByCandidat(Candidat candidat)
        {
            Entretien value;
            _entretiens.TryGetValue(candidat, out value);
            return value;
        }

        public void Save(Entretien entretien)
        {
            _entretiens.Add(entretien.Candidat, entretien);
        }
    }
}