using System;

namespace PlanificationEntretien
{
    public record Entretien()
    {
        public DateTime DateEtHeure { get; }
        public string EmailCandidat { get; }
        public string EmailRecruteur { get; }

        public Entretien(DateTime dateEtHeure, string emailCandidat, string emailRecruteur) : this()
        {
            DateEtHeure = dateEtHeure;
            EmailCandidat = emailCandidat;
            EmailRecruteur = emailRecruteur;
        }
    }
}