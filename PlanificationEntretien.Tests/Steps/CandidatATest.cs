using System;
using PlanificationEntretien.controller;
using PlanificationEntretien.model;
using PlanificationEntretien.repository;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class CandidatATest
    {
        private Candidat _candidat;
        private CandidatDto _candidatRequest;
        private CandidatRepository _inMemoryCandidatRepository = new CandidatRepository();

        [Given(@"un candidat ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences")]
        public void GivenUnCandidatAvecAnsDExperiences(string language, string email, string xp)
        {
            int? value = String.IsNullOrEmpty(xp) ? null : Int32.Parse(xp);
            _candidat = new Candidat(language, email, value);
            _candidatRequest = new CandidatDto(language, email, value);
        }

        [When(@"on tente d'enregistrer le candidat")]
        public void WhenOnTenteDenregistrerLeCandidat()
        {
            var candidatController = new CandidatController(_inMemoryCandidatRepository);
            candidatController.Create(_candidatRequest);
        }

        [Then(@"le candidat est correctement enregistré avec ses informations ""(.*)"", ""(.*)"" et ""(.*)"" ans d’expériences")]
        public void ThenLeCandidatEstCorrectementEnregistreAvecSesInformationsEtAnsDExperiences(string java, string p1, string p2)
        {
            var candidat = _inMemoryCandidatRepository.FindByEmail(_candidat.Email);
            Assert.Equal(_candidat, candidat);
        }

        [Then(@"le candidat n'est pas enregistré")]
        public void ThenLeCandidatNestPasEnregistre()
        {
            var candidat = _inMemoryCandidatRepository.FindByEmail(_candidat.Email);
            Assert.Null(candidat);
        }
    }
}