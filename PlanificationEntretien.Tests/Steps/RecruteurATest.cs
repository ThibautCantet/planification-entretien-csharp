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
    public class RecruteurATest
    {
        private Recruteur _recruteur;
        private IRecruteurPort _recruteurPort = new InMemoryRecruteurAdapter();
        private CreateRecruteurRequest _createRecruteurRequest;

        [Given(@"un recruteur ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences")]
        public void GivenUnRecruteurAvecAnsDExperiences(string language, string email, string xp)
        {
            int? value = String.IsNullOrEmpty(xp) ? null : Int32.Parse(xp);
            _recruteur = new Recruteur(language, email, value);
            _createRecruteurRequest = new CreateRecruteurRequest(language, email, value);
        }

        [When(@"on tente d'enregistrer le recruteur")]
        public void WhenOnTenteDenregistrerLeRecruteur()
        {
            var creerRecruteur = new CreerRecruteur(_recruteurPort);
            var recruteurController = new RecruteurController(creerRecruteur);
            recruteurController.Create(_createRecruteurRequest);
        }

        [Then(@"le recruteur est correctement enregistré avec ses informations ""(.*)"", ""(.*)"" et ""(.*)"" ans d’expériences")]
        public void ThenLeRecruteurEstCorrectementEnregistreAvecSesInformationsEtAnsDExperiences(string java, string p1, string p2)
        {
            var recruteur = _recruteurPort.FindByEmail(_recruteur.Email);
            Assert.Equal(_recruteur, recruteur);
        }

        [Then(@"le recruteur n'est pas enregistré")]
        public void ThenLeRecruteurNestPasEnregistre()
        {
            var recruteur = _recruteurPort.FindByEmail(_recruteur.Email);
            Assert.Null(recruteur);
        }
    }
}