using System;

namespace PlanificationEntretien.domain
{
    public class Disponibilite
    {
        public DateTime Horaire { get; }

        public Disponibilite(DateTime horaire)
        {
            Horaire = horaire;
        }
    }
}