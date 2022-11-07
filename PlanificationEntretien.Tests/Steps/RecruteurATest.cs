using System;
using Planification;
using PlanificationEntretien.domain;
using PlanificationEntretien.memory;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class RecruteurATest
    {
        private Recruteur _recruteur;
        private IRecruteurRepository _recruteurRepository = new InMemoryRecruteurRepository();

        [Given(@"un recruteur ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences")]
        public void GivenUnRecruteurAvecAnsDExperiences(string language, string email, string xp)
        {
            int? value = String.IsNullOrEmpty(xp) ? (int?)null : Int32.Parse(xp);
            _recruteur = new Recruteur(language, email, value);
        }

        [When(@"on tente d'enregistrer le recruteur")]
        public void WhenOnTenteDenregistrerLeRecruteur()
        {
            var recruteurService = new RecruteurService(_recruteurRepository);
            recruteurService.Create(_recruteur);
        }

        [Then(@"le recruteur est correctement enregistré avec ses informations ""(.*)"", ""(.*)"" et ""(.*)"" ans d’expériences")]
        public void ThenLeRecruteurEstCorrectementEnregistreAvecSesInformationsEtAnsDExperiences(string java, string p1, string p2)
        {
            var recruteur = _recruteurRepository.FindByEmail(_recruteur.Email);
            Assert.Equal(_recruteur, recruteur);
        }

        [Then(@"le recruteur n'est pas enregistré")]
        public void ThenLeRecruteurNestPasEnregistre()
        {
            var recruteur = _recruteurRepository.FindByEmail(_recruteur.Email);
            Assert.Null(recruteur);
        }
    }
}