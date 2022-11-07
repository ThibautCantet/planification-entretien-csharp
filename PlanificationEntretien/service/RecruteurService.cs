using System;
using System.Net.Mail;
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

        public void Create(Recruteur recruteur)
        {
            if (!string.IsNullOrEmpty(recruteur.Email) && IsValid(recruteur.Email)
                && !string.IsNullOrEmpty(recruteur.Language)
                && recruteur.ExperienceEnAnnees > 0)
            {
                _recruteurRepository.Save(recruteur);
            }
        }
        
        private static bool IsValid(string email)
        {
            try
            {
                new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}