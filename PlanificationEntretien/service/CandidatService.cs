using System;
using System.Net.Mail;
using PlanificationEntretien.domain;
using PlanificationEntretien.memory;

namespace Planification
{
    public class CandidatService
    {
        private readonly ICandidatRepository _candidatRepository;

        public CandidatService(ICandidatRepository candidatRepository)
        {
            _candidatRepository = candidatRepository;
        }

        public void Save(Candidat candidat)
        {
            if (!string.IsNullOrEmpty(candidat.Email) && IsValid(candidat.Email)
                                                      && !string.IsNullOrEmpty(candidat.Language)
                                                      && candidat.ExperienceEnAnnees > 0)
            {
                _candidatRepository.Save(candidat);
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