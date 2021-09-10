using System;
using System.Globalization;
using Planification;
using PlanificationEntretien.domain;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Tests
{
    [Binding]
    public class PlanifierEntretienATest
    {
        private Candidat _candidat;
        private Disponibilite _disponibiliteDuCandidat;
        private Recruteur _recruteur;
        private DateTime _dateDeDisponibiliteDuRecruteur;
        private PlanifierEntretien _planifierEntretien;
        private readonly EntretienRepository _entretienRepository = new InMemoryEntretienRepository();
        private readonly IEmailService _emailService = new FakeEmailService();

        private ResultatPlanificationEntretien _resultatPlanificationEntretien;

        [Given(@"un candidat ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences qui est disponible ""(.*)"" à ""(.*)""")]
        public void GivenUnCandidatAvecAnsDExperiencesQuiEstDisponibleA(string language, string email, string experienceInYears, string date, string time)
        {
            _candidat = new Candidat(language, email, Int32.Parse(experienceInYears));
            _disponibiliteDuCandidat = new Disponibilite(DateTime.ParseExact(date + " " + time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture));
        }

        [Given(@"un recruteur ""(.*)"" \(""(.*)""\) qui a ""(.*)"" ans d’XP qui est dispo ""(.*)""")]
        public void GivenUnRecruteurQuiAAnsDxpQuiEstDispo(string language, string email, string experienceInYears, string date)
        {
            _recruteur = new Recruteur(language, email, Int32.Parse(experienceInYears));
            _dateDeDisponibiliteDuRecruteur = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        [When(@"on tente une planification d’entretien")]
        public void WhenOnTenteUnePlanificationDEntretien()
        {
            _planifierEntretien = new PlanifierEntretien(_entretienRepository, _emailService);
            _resultatPlanificationEntretien = _planifierEntretien.Execute(_candidat, _disponibiliteDuCandidat, _recruteur, _dateDeDisponibiliteDuRecruteur);
        }

        [Then(@"L’entretien est planifié")]
        public void ThenLEntretienEstPlanifie()
        {
            var resultatPlanificationEntretien = new EntretienPlanifie(_candidat, _recruteur, new HoraireEntretien(_disponibiliteDuCandidat.Horaire));
            Assert.Equal(resultatPlanificationEntretien, _resultatPlanificationEntretien);
            
            Entretien entretien = _entretienRepository.FindByCandidat(_candidat);
            HoraireEntretien horaire = new HoraireEntretien(_disponibiliteDuCandidat.Horaire);
            Entretien expectedEntretien = Entretien.of(_candidat, _recruteur, horaire);
            Assert.Equal(expectedEntretien, entretien);
        }

        [Then(@"un mail de confirmation est envoyé au candidat et le recruteur")]
        public void ThenUnMailDeConfirmationEstEnvoyeAuCandidatEtLeRecruteur()
        {
            HoraireEntretien horaire = new HoraireEntretien(_disponibiliteDuCandidat.Horaire);
            Assert.True(((FakeEmailService) _emailService).UnEmailDeConfirmationAEteEnvoyeAuCandidat(_candidat.Email, horaire));
            Assert.True(((FakeEmailService) _emailService).UnEmailDeConfirmationAEteEnvoyeAuRecruteur(_recruteur.Email, horaire));
        }

        [Then(@"L’entretien n'est pas planifié")]
        public void ThenLEntretienNestPasPlanifie()
        {
            Assert.Equal(_resultatPlanificationEntretien, new EntretienEchouee(_candidat, _recruteur, new HoraireEntretien(_disponibiliteDuCandidat.Horaire)));
            
            Entretien entretien = _entretienRepository.FindByCandidat(_candidat);
            Assert.Null(entretien);
        }

        [Then(@"aucun mail de confirmation est envoyé au candidat ou au recruteur")]
        public void ThenAucunMailDeConfirmationEstEnvoyeAuCandidatOuAuRecruteur()
        {
            HoraireEntretien horaire = new HoraireEntretien(_disponibiliteDuCandidat.Horaire);
            Assert.False(((FakeEmailService) _emailService).UnEmailDeConfirmationAEteEnvoyeAuCandidat(_candidat.Email, horaire));
            Assert.False(((FakeEmailService) _emailService).UnEmailDeConfirmationAEteEnvoyeAuCandidat(_recruteur.Email, horaire));
        }
    }
}