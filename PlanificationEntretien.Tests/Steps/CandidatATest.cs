using System;
using PlanificationEntretien.application;
using PlanificationEntretien.domain;
using PlanificationEntretien.infrastructure.memory;
using PlanificationEntretien.use_case;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class CandidatATest : ATest
    {
        private Candidat _candidat;
        private CreateCandidatRequest _candidatRequest;

        [Given(@"un candidat ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences")]
        public void GivenUnCandidatAvecAnsDExperiences(string language, string email, string xp)
        {
            int? value = String.IsNullOrEmpty(xp) ? null : Int32.Parse(xp);
            _candidat = new Candidat(language, email, value);
            _candidatRequest = new CreateCandidatRequest(language, email, value);
        }

        [When(@"on tente d'enregistrer le candidat")]
        public void WhenOnTenteDenregistrerLeCandidat()
        {
            var creerCandidat = new CreerCandidat(_candidatPort);
            var candidatController = new CandidatController(creerCandidat);
            candidatController.Create(_candidatRequest);
        }

        [Then(@"le candidat est correctement enregistré avec ses informations ""(.*)"", ""(.*)"" et ""(.*)"" ans d’expériences")]
        public void ThenLeCandidatEstCorrectementEnregistreAvecSesInformationsEtAnsDExperiences(string java, string p1, string p2)
        {
            var candidat = _candidatPort.FindByEmail(_candidat.Email);
            Assert.Equal(_candidat, candidat);
        }

        [Then(@"le candidat n'est pas enregistré")]
        public void ThenLeCandidatNestPasEnregistre()
        {
            var candidat = _candidatPort.FindByEmail(_candidat.Email);
            Assert.Null(candidat);
        }
    }
}