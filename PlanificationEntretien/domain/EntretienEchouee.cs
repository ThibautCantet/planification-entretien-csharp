using PlanificationEntretien.domain;

namespace PlanificationEntretien.Tests
{
    public class EntretienEchouee : ResultatPlanificationEntretien
    {
        public EntretienEchouee(Candidat candidat, Recruteur recruteur, HoraireEntretien horaireEntretien) : base(candidat, recruteur, horaireEntretien)
        {
        }
    }
}