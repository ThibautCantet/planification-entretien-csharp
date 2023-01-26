using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain;
using PlanificationEntretien.email;
using PlanificationEntretien.infrastructure.controller;
using PlanificationEntretien.use_case;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class PlanifierEntretienATest : ATest
    {
        private Candidat _candidat;
        private DateTime _disponibiliteDuCandidat;
        private Recruteur _recruteur;
        private DateTime _dateDeDisponibiliteDuRecruteur;
        private PlanifierEntretien _planifierEntretien;
        private readonly IEmailService _emailService = new FakeEmailService();
        private CreatedAtActionResult _createEntretienResponse;

        [Given(
            @"un candidat ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences qui est disponible ""(.*)"" à ""(.*)""")]
        public void GivenUnCandidatAvecAnsDExperiencesQuiEstDisponibleA(string language, string email,
            string experienceInYears, string date, string time)
        {
            _candidat = new Candidat(language, email, Int32.Parse(experienceInYears));
            CandidatRepository.Save(_candidat);
            _disponibiliteDuCandidat =
                DateTime.ParseExact(date + " " + time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
        }

        [Given(@"un recruteur ""(.*)"" \(""(.*)""\) qui a ""(.*)"" ans d’XP qui est dispo ""(.*)"" à ""(.*)""")]
        public void GivenUnRecruteurQuiAAnsDxpQuiEstDispo(string language, string email, string experienceInYears,
            string date, string time)
        {
            _recruteur = new Recruteur(language, email, Int32.Parse(experienceInYears));
            RecruteurRepository.Save(_recruteur);
            _dateDeDisponibiliteDuRecruteur =
                DateTime.ParseExact(date + " " + time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
        }

        [When(@"on tente une planification d’entretien")]
        public void WhenOnTenteUnePlanificationDEntretien()
        {
            _planifierEntretien = new PlanifierEntretien(EntretienRepository, _emailService);
            var entretienController =
                new EntretienController(_planifierEntretien, CandidatRepository, RecruteurRepository);

            _createEntretienResponse = entretienController.Create(new CreateEntretienRequest(_candidat.Email,
                _recruteur.Email,
                _disponibiliteDuCandidat,
                _dateDeDisponibiliteDuRecruteur)) as CreatedAtActionResult;
        }

        [Then(@"L’entretien est planifié")]
        public void ThenLEntretienEstPlanifie()
        {
            Assert.IsType<CreatedAtActionResult>(_createEntretienResponse);
            Assert.IsType<CreateEntretienResponse>(_createEntretienResponse.Value);
            var createEntretienRequest = _createEntretienResponse.Value as CreateEntretienResponse;
            Assert.Equal(createEntretienRequest.EmailCandidat, _candidat.Email);
            Assert.Equal(createEntretienRequest.EmailRecruteur, _recruteur.Email);
            Assert.Equal(createEntretienRequest.Horaire, _disponibiliteDuCandidat);
            
            Entretien entretien = EntretienRepository.FindByCandidat(_candidat);
            Entretien expectedEntretien = Entretien.of(_candidat, _recruteur, _disponibiliteDuCandidat);
            Assert.Equal(expectedEntretien, entretien);
        }

        [Then(@"un mail de confirmation est envoyé au candidat et le recruteur")]
        public void ThenUnMailDeConfirmationEstEnvoyeAuCandidatEtLeRecruteur()
        {
            Assert.True(
                ((FakeEmailService)_emailService).UnEmailDeConfirmationAEteEnvoyeAuCandidat(_candidat.Email,
                    _disponibiliteDuCandidat));
            Assert.True(
                ((FakeEmailService)_emailService).UnEmailDeConfirmationAEteEnvoyeAuRecruteur(_recruteur.Email,
                    _disponibiliteDuCandidat));
        }

        [Then(@"L’entretien n'est pas planifié")]
        public void ThenLEntretienNestPasPlanifie()
        {
            Entretien entretien = EntretienRepository.FindByCandidat(_candidat);
            Assert.Null(entretien);
        }

        [Then(@"aucun mail de confirmation est envoyé au candidat ou au recruteur")]
        public void ThenAucunMailDeConfirmationEstEnvoyeAuCandidatOuAuRecruteur()
        {
            Assert.False(
                ((FakeEmailService)_emailService).UnEmailDeConfirmationAEteEnvoyeAuCandidat(_candidat.Email,
                    _disponibiliteDuCandidat));
            Assert.False(
                ((FakeEmailService)_emailService).UnEmailDeConfirmationAEteEnvoyeAuCandidat(_recruteur.Email,
                    _disponibiliteDuCandidat));
        }
    }
}