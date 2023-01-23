using System;
using PlanificationEntretien.infrastructure.controller;
using PlanificationEntretien.domain;
using PlanificationEntretien.use_case;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class CandidatATest : ATest
    {
        private string _emailCandidat;
        private CreateCandidatRequest _candidatRequest;

        [Given(@"un candidat ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences")]
        public void GivenUnCandidatAvecAnsDExperiences(string language, string email, string xp)
        {
            int? value = String.IsNullOrEmpty(xp) ? 0 : Int32.Parse(xp);
            _emailCandidat = email;
            _candidatRequest = new CreateCandidatRequest(language, email, value);
        }

        [When(@"on tente d'enregistrer le candidat")]
        public void WhenOnTenteDenregistrerLeCandidat()
        {
            var creerCandidat = new CreerCandidat(CandidatRepository);
            var candidatController = new CandidatController(creerCandidat);
            candidatController.Create(_candidatRequest);
        }

        [Then(@"le candidat est correctement enregistré avec ses informations ""(.*)"", ""(.*)"" et ""(.*)"" ans d’expériences")]
        public void ThenLeCandidatEstCorrectementEnregistreAvecSesInformationsEtAnsDExperiences(string java, string email, string xp)
        {
            var candidat = CandidatRepository.FindByEmail(_emailCandidat);
            Assert.Equal(candidat,  new Candidat(java, email, int.Parse(xp)));
        }

        [Then(@"le candidat n'est pas enregistré")]
        public void ThenLeCandidatNestPasEnregistre()
        {
            var candidat = CandidatRepository.FindByEmail(_emailCandidat);
            Assert.Null(candidat);
        }
    }
}