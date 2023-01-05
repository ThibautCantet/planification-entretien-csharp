using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.infrastructure.controller;
using PlanificationEntretien.domain;
using PlanificationEntretien.infrastructure.repository;
using PlanificationEntretien.use_case;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class RecruteurATest : ATest
    {
        private CreateRecruteurRequest _createRecruteurRequest;
        private string _emailRecruteur;
        private Recruteur _recruteur;
        private IActionResult _recruteurs;

        [Given(@"un recruteur ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences")]
        public void GivenUnRecruteurAvecAnsDExperiences(string language, string email, string xp)
        {
            int? value = String.IsNullOrEmpty(xp) ? null : Int32.Parse(xp);
            _emailRecruteur = email;
            _createRecruteurRequest = new CreateRecruteurRequest(language, email, value);
        }

        [When(@"on tente d'enregistrer le recruteur")]
        public void WhenOnTenteDenregistrerLeRecruteur()
        {
            var creerRecruteur = new CreerRecruteur(RecruteurRepository);
            var listerRecruteurExperimente = new ListerRecruteurExperimente(RecruteurRepository);
            var recruteurController = new RecruteurController(creerRecruteur, listerRecruteurExperimente);
            recruteurController.Create(_createRecruteurRequest);
        }

        [Then(@"le recruteur est correctement enregistré avec ses informations ""(.*)"", ""(.*)"" et ""(.*)"" ans d’expériences")]
        public void ThenLeRecruteurEstCorrectementEnregistreAvecSesInformationsEtAnsDExperiences(string java, string email, string xp)
        {
            var recruteur = RecruteurRepository.FindByEmail(_emailRecruteur);
            Assert.Equal(recruteur, new Recruteur(java, email, int.Parse(xp)));
        }

        [Then(@"le recruteur n'est pas enregistré")]
        public void ThenLeRecruteurNestPasEnregistre()
        {
            var recruteur = RecruteurRepository.FindByEmail(_emailRecruteur);
            Assert.Null(recruteur);
        }
        
        [When(@"on liste les recruteurs expérimentés")]
        public async Task WhenOnListeLesRecruteursExperimentes()
        {
            var creerRecruteur = new CreerRecruteur(RecruteurRepository);
            var listerRecruteurExperimente = new ListerRecruteurExperimente(RecruteurRepository);
            var recruteurController = new RecruteurController(creerRecruteur, listerRecruteurExperimente);
            _recruteurs = await recruteurController.ListerExperimentes();
        }

        [Then(@"on récupères les recruteurs suivants")]
        public void ThenOnRecuperesLesRecruteursSuivants(Table table)
        {
            var expectedRecruteurs = table.Rows
                .Select(row => BuildRecruteurExperimente(row.Values.ToList()[1], row.Values.ToList()[2]))
                .ToList();
            
            var okResult = Assert.IsType<OkObjectResult>(_recruteurs);
            var recruteurs = Assert.IsType<List<RecruteurExperimenteResponse>>(okResult.Value);
            
            Assert.Equal(recruteurs, expectedRecruteurs);
        }
        
        private static RecruteurExperimenteResponse BuildRecruteurExperimente(string emailRecruteur, string detail)
        {
            return new RecruteurExperimenteResponse(emailRecruteur, detail);
        }
        
        [Given(@"les recruteurs existant suivants")]
        public void GivenLesRecruteursExistants(Table table)
        {
            var recruteurs = table.Rows.Select(row => BuildRecruteur(row));
            foreach (var recruteur in recruteurs)
            {
                RecruteurRepository.Save(recruteur);
            }
        }

        public static Recruteur BuildRecruteur(TableRow row)
        {
            var values = row.Values.ToList();
            return new Recruteur( values[2], values[1], int.Parse(values[3]));
        }
    }
}