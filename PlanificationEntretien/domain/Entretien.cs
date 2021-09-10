namespace PlanificationEntretien.domain
{
    public class Entretien
    {
        public Candidat Candidat { get; }
        public Recruteur Recruteur { get; }
        public HoraireEntretien Horaire { get; }

        public Entretien(Candidat candidat, Recruteur recruteur, HoraireEntretien horaire)
        {
            Candidat = candidat;
            Recruteur = recruteur;
            Horaire = horaire;
        }
    }
}