using System;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.candidat.domain_service;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.candidat.application_service;
using PlanificationEntretien.candidat.infrastructure.controller;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class CandidatATest : ATest
    {
        private string _emailCandidat;
        private CreateCandidatRequest _candidatRequest;
        private CreatedAtActionResult _actionResult;

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
            var candidatFactory = new CandidatFactory();
            var creerCandidat = new CreerCandidatCommandHandler(CandidatRepository, candidatFactory);
            var candidatController = new CandidatController(creerCandidat);
            _actionResult = candidatController.Create(_candidatRequest) as CreatedAtActionResult;
        }

        [Then(@"le candidat est correctement enregistré avec ses informations ""(.*)"", ""(.*)"" et ""(.*)"" ans d’expériences")]
        public void ThenLeCandidatEstCorrectementEnregistreAvecSesInformationsEtAnsDExperiences(string java, string email, string xp)
        {
            Assert.IsType<CreatedAtActionResult>(_actionResult);
            Assert.IsType<CreateCandidatResponse>(_actionResult.Value);
            var createCandidatResponse = _actionResult.Value as CreateCandidatResponse;
            Assert.Equal(createCandidatResponse.language, _candidatRequest.Language);
            Assert.Equal(createCandidatResponse.email, _candidatRequest.Email);
            Assert.Equal(createCandidatResponse.xp, _candidatRequest.Xp);
            Assert.NotEqual(0, createCandidatResponse.Id);
            
            var candidat = CandidatRepository.FindById(createCandidatResponse.Id);
            Assert.Equal(candidat,  new Candidat(createCandidatResponse.Id, java, email, int.Parse(xp)));
        }

        [Then(@"le candidat n'est pas enregistré")]
        public void ThenLeCandidatNestPasEnregistre()
        {
            var candidat = CandidatRepository.FindByEmail(_emailCandidat);
            Assert.Null(candidat);
        }
    }
}