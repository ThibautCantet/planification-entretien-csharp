using System;
using System.Globalization;
using PlanificationEntretien.email;
using PlanificationEntretien.model;
using PlanificationEntretien.repository;
using PlanificationEntretien.service;
using service;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class PlanifierEntretienATest
    {
        private Candidat _candidat;
        private DateTime _disponibiliteDuCandidat;
        private Recruteur _recruteur;
        private DateTime _dateDeDisponibiliteDuRecruteur;
        private EntretienService _entretienService;
        private readonly EntretienRepository _inMemoryEntretienRepository = new EntretienRepository();
        private readonly CandidatRepository _inMemoryCandidatRepository = new CandidatRepository();
        private readonly RecruteurRepository _inMemoryRecruteurRepository = new RecruteurRepository();
        private readonly IEmailService _emailService = new FakeEmailService();

        private Boolean _resultatPlanificationEntretien;

        [Given(@"un candidat ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences qui est disponible ""(.*)"" à ""(.*)""")]
        public void GivenUnCandidatAvecAnsDExperiencesQuiEstDisponibleA(string language, string email, string experienceInYears, string date, string time)
        {
            _candidat = new Candidat(language, email, Int32.Parse(experienceInYears));
            _inMemoryCandidatRepository.Save(_candidat);
            _disponibiliteDuCandidat = DateTime.ParseExact(date + " " + time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
        }

        [Given(@"un recruteur ""(.*)"" \(""(.*)""\) qui a ""(.*)"" ans d’XP qui est dispo ""(.*)"" à ""(.*)""")]
        public void GivenUnRecruteurQuiAAnsDxpQuiEstDispo(string language, string email, string experienceInYears, string date, string time)
        {
            _recruteur = new Recruteur(language, email, Int32.Parse(experienceInYears));
            _inMemoryRecruteurRepository.Save(_recruteur);
            _dateDeDisponibiliteDuRecruteur = DateTime.ParseExact(date + " " + time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
        }

        [When(@"on tente une planification d’entretien")]
        public void WhenOnTenteUnePlanificationDEntretien()
        {
            _entretienService = new EntretienService(_inMemoryEntretienRepository, _emailService, _inMemoryCandidatRepository, _inMemoryRecruteurRepository);
            _resultatPlanificationEntretien = _entretienService.Planifier(_candidat.Email, _disponibiliteDuCandidat, _recruteur.Email, _dateDeDisponibiliteDuRecruteur);
        }

        [Then(@"L’entretien est planifié")]
        public void ThenLEntretienEstPlanifie()
        {
            Assert.True(_resultatPlanificationEntretien);
            
            Entretien entretien = _inMemoryEntretienRepository.FindByCandidat(_candidat);
            Entretien expectedEntretien = Entretien.of(_candidat, _recruteur, _disponibiliteDuCandidat);
            Assert.Equal(expectedEntretien, entretien);
        }

        [Then(@"un mail de confirmation est envoyé au candidat et le recruteur")]
        public void ThenUnMailDeConfirmationEstEnvoyeAuCandidatEtLeRecruteur()
        {
            Assert.True(((FakeEmailService) _emailService).UnEmailDeConfirmationAEteEnvoyeAuCandidat(_candidat.Email, _disponibiliteDuCandidat));
            Assert.True(((FakeEmailService) _emailService).UnEmailDeConfirmationAEteEnvoyeAuRecruteur(_recruteur.Email, _disponibiliteDuCandidat));
        }

        [Then(@"L’entretien n'est pas planifié")]
        public void ThenLEntretienNestPasPlanifie()
        {
            Assert.False(_resultatPlanificationEntretien);
            
            Entretien entretien = _inMemoryEntretienRepository.FindByCandidat(_candidat);
            Assert.Null(entretien);
        }

        [Then(@"aucun mail de confirmation est envoyé au candidat ou au recruteur")]
        public void ThenAucunMailDeConfirmationEstEnvoyeAuCandidatOuAuRecruteur()
        {
            Assert.False(((FakeEmailService) _emailService).UnEmailDeConfirmationAEteEnvoyeAuCandidat(_candidat.Email, _disponibiliteDuCandidat));
            Assert.False(((FakeEmailService) _emailService).UnEmailDeConfirmationAEteEnvoyeAuCandidat(_recruteur.Email, _disponibiliteDuCandidat));
        }
    }
}