using System;
using System.Globalization;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Tests
{
    [Binding]
    public class PlanifierEntretienATest
    {
        private Candidat _candidat;
        private Recruteur _recruteur;
        private EntretienService _entretienService;
        private DateTime _disponibiliteCandidat;
        private DateTime _disponibiliteRecruteur;
        private Entretien _entretien;
        private readonly Mock<IEmailService> _emailService = new Mock<IEmailService>();
        private readonly Mock<IEntretienRepository> _entretienRepository = new Mock<IEntretienRepository>();

        public PlanifierEntretienATest()
        {
            _entretienService = new EntretienService(_entretienRepository.Object, _emailService.Object);
        }

        [Given(@"un candidat ""(.*)"" \(""(.*)""\) avec (.*) ans d’expériences qui est disponible ""(.*)"" à ""(.*)""")]
        public void GivenUnCandidatAvecAnsDExperiencesQuiEstDisponibleA(string language, string email, int xp, string date, string heure)
        {
            _candidat = new Candidat(language, email, xp);
            _disponibiliteCandidat = DateTime.ParseExact($"{date} {heure}", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        }

        [Given(@"un recruteur ""(.*)"" \(""(.*)""\) qui a (.*) ans d’XP qui est dispo ""(.*)""")]
        public void GivenUnRecruteurQuiAAnsDxpQuiEstDispo(string language, string email, int xp, string date)
        {
            _recruteur = new Recruteur(language, email, xp);
            _disponibiliteRecruteur = DateTime.ParseExact($"{date}", "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        [When(@"on tente une planification d’entretien")]
        public void WhenOnTenteUnePlanificationDEntretien()
        {
            _entretien = _entretienService.planifier(_candidat, _recruteur, _disponibiliteCandidat, _disponibiliteRecruteur);
        }

        [Then(@"L’entretien est planifié : ""(.*)"", ""(.*)"" à ""(.*)"" à ""(.*)""")]
        public void ThenLEntretienEstPlanifieAa(string emailCandidat, string emailRecruteur, string date, string heure)
        {
            _entretienRepository.Verify(t => t.Save(It.Is<Entretien>(s => s.Equals(_entretien))));
            Assert.NotNull(_entretien);
        }

        [Then(@"un mail de confirmation est envoyé au candidat \(""(.*)""\) et au recruteur \(""(.*)""\)")]
        public void ThenUnMailDeConfirmationEstEnvoyeAuCandidatEtAuRecruteur(string emailCandidat, string emailRecruteur)
        {
            _emailService.Verify(t => t.SendToCandidat(It.Is<string>(s => s.Equals(emailCandidat))));
            _emailService.Verify(t => t.SendToRecruteur(It.Is<string>(s => s.Equals(emailRecruteur))));
        }

        [Then(@"L’entretien n'est pas planifié")]
        public void ThenLEntretienNestPasPlanifie()
        {
        }

        [Then(@"aucun mail de confirmation est envoyé au candidat ou au recruteur")]
        public void ThenAucunMailDeConfirmationEstEnvoyeAuCandidatOuAuRecruteur()
        {
        }
    }
}