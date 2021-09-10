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
        private Disponibilite _disponibiliteDuRecruteur;
        private EntretienService _entretienService;
        private readonly IEntretienRepository _entretienRepository = new InMemoryEntretienRepository();
        private readonly IEmailService _emailService = new FakeEmailService();

        [Given(@"candidat avec comme email ""(.*)"" et avec (.*) années d’XP, faisant du ""(.*)"" et disponible le ""(.*)"" à ""(.*)""")]
        public void GivenCandidatAvecCommeEmailEtAvecAnneesDxpFaisantDuEtDisponibleLeA(string email, int experienceInYears, string language, string date, string time)
        {
            _candidat = new Candidat(language, email, experienceInYears);
            _disponibiliteDuCandidat = new Disponibilite(DateTime.ParseExact(date + " " + time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture));
        }

        [Given(@"un recruteur avec comme email ""(.*)"" et (.*) années d’XP, faisant du ""(.*)"" et disponible le ""(.*)"" à ""(.*)""")]
        public void GivenUnRecruteurAvecCommeEmailEtAnneesDxpFaisantDuEtDisponibleLeA(string email, int experienceInYears, string language, string date, string time)
        {
            _recruteur = new Recruteur(language, email, experienceInYears);
            _disponibiliteDuRecruteur = new Disponibilite(DateTime.ParseExact(date + " " + time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture));
        }

        [When(@"je planifie l’entretien")]
        public void WhenJePlanifieLEntretien()
        {
            _entretienService = new EntretienService();
            _entretienService.Planifier(_candidat, _disponibiliteDuCandidat, _recruteur, _disponibiliteDuRecruteur);
        }

        [Then(@"le rdv est pris \(enregistré dans la base de données\)")]
        public void ThenLeRdvEstPrisEnregistreDansLaBaseDeDonnees()
        {
            Entretien entretien = _entretienRepository.FindByCandidat(_candidat);
            HoraireEntretien horaire = new HoraireEntretien(_disponibiliteDuCandidat.Horaire);
            Entretien expectedEntretien = new Entretien(_candidat, _recruteur, horaire);
            Assert.Same(expectedEntretien, entretien);
        }

        [Then(@"un email a été envoyé au candidat")]
        public void ThenUnEmailAEteEnvoyeAuCandidat()
        {
            HoraireEntretien horaire = new HoraireEntretien(_disponibiliteDuCandidat.Horaire);
            Assert.True(((FakeEmailService) _emailService).UnEmailDeConfirmationAEteEnvoyeAuCandidat(_candidat.Email, horaire));
        }

        [Then(@"un email a été envoyé au recruteur")]
        public void ThenUnEmailAEteEnvoyeAuRecruteur()
        {
            HoraireEntretien horaire = new HoraireEntretien(_disponibiliteDuCandidat.Horaire);
            Assert.True(((FakeEmailService) _emailService).UnEmailDeConfirmationAEteEnvoyeAuRecruteur(_recruteur.Email, horaire));
        }
    }
}