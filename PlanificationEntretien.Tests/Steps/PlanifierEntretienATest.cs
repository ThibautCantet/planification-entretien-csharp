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
            var entretien = new Entretien(_disponibiliteCandidat, _candidat.Email, _recruteur.Email);
            _entretienRepository.Setup(x => x.Save(It.IsAny<Entretien>())).Returns(entretien);

            _entretien = _entretienService.planifier(_candidat, _recruteur, _disponibiliteCandidat, _disponibiliteRecruteur);
        }

        [Then(@"L’entretien est planifié : ""(.*)"", ""(.*)"" à ""(.*)"" à ""(.*)""")]
        public void ThenLEntretienEstPlanifieAa(string emailCandidat, string emailRecruteur, string date, string heure)
        {
            var dateEtHeure = DateTime.ParseExact($"{date} {heure}", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            var entretien = new Entretien(dateEtHeure, emailCandidat, emailRecruteur);
            _entretienRepository.Verify(repository => repository.Save(It.Is<Entretien>(e => entretien.Equals(e))));
            Assert.NotNull(_entretien);
        }

        [Then(@"un mail de confirmation est envoyé au candidat \(""(.*)""\) et au recruteur \(""(.*)""\)")]
        public void ThenUnMailDeConfirmationEstEnvoyeAuCandidatEtAuRecruteur(string emailCandidat, string emailRecruteur)
        {
            _emailService.Verify(emailService => emailService.SendToCandidat(It.Is<string>(s => s.Equals(emailCandidat))));
            _emailService.Verify(emailService => emailService.SendToRecruteur(It.Is<string>(s => s.Equals(emailRecruteur))));
        }

        [Then(@"L’entretien n'est pas planifié")]
        public void ThenLEntretienNestPasPlanifie()
        {
            _entretienRepository.Verify(repository => repository.Save(It.IsAny<Entretien>()), Times.Never);
            Assert.Null(_entretien);
        }

        [Then(@"aucun mail de confirmation est envoyé au candidat ou au recruteur")]
        public void ThenAucunMailDeConfirmationEstEnvoyeAuCandidatOuAuRecruteur()
        {
            _emailService.Verify(emailService => emailService.SendToCandidat(It.IsAny<string>()), Times.Never);
            _emailService.Verify(emailService => emailService.SendToRecruteur(It.IsAny<string>()), Times.Never);
        }
    }
}