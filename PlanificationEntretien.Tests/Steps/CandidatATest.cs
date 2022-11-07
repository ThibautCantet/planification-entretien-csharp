using System;
using Planification;
using PlanificationEntretien.domain;
using PlanificationEntretien.memory;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class CandidatATest
    {
        private Candidat _candidat;
        private ICandidatRepository _candidatRepository = new InMemoryCandidatRepository();

        [Given(@"un candidat ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences")]
        public void GivenUnCandidatAvecAnsDExperiences(string language, string email, string xp)
        {
            int? value = String.IsNullOrEmpty(xp) ? (int?)null : Int32.Parse(xp);
            _candidat = new Candidat(language, email, value);
        }

        [When(@"on tente d'enregistrer le candidat")]
        public void WhenOnTenteDenregistrerLeCandidat()
        {
            var candidatService = new CandidatService(_candidatRepository);
            candidatService.Save(_candidat);
        }

        [Then(@"le candidat est correctement enregistré avec ses informations ""(.*)"", ""(.*)"" et ""(.*)"" ans d’expériences")]
        public void ThenLeCandidatEstCorrectementEnregistreAvecSesInformationsEtAnsDExperiences(string java, string p1, string p2)
        {
            var candidat = _candidatRepository.FindByEmail(_candidat.Email);
            Assert.Equal(_candidat, candidat);
        }

        [Then(@"le candidat n'est pas enregistré")]
        public void ThenLeCandidatNestPasEnregistre()
        {
            var candidat = _candidatRepository.FindByEmail(_candidat.Email);
            Assert.Null(candidat);
        }
    }
}