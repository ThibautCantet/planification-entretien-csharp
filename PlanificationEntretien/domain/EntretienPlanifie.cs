namespace PlanificationEntretien.domain
{
    public class EntretienPlanifie : ResultatPlanificationEntretien
    {
        public EntretienPlanifie(Candidat candidat, Recruteur recruteur, HoraireEntretien horaireEntretien) : base(candidat, recruteur, horaireEntretien)
        {
        }
    }
}