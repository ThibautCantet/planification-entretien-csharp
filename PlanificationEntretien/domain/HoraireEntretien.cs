using System;

namespace PlanificationEntretien.domain
{
    public class HoraireEntretien
    {
        public DateTime Horaire { get; }

        public HoraireEntretien(DateTime horaire)
        {
            Horaire = horaire;
        }
    }
}