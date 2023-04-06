using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.application_service;
using Candidat = PlanificationEntretien.candidat.domain.Candidat;
using Recruteur = PlanificationEntretien.recruteur.domain.Recruteur;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.infrastructure.controller;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.infrastructure;
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
        private PlanifierEntretienCommandHandler _planifierEntretienCommandHandler;
        private readonly IEmailService _emailService = new FakeEmailService();
        private CreatedAtActionResult _createEntretienResponse;
        private MessageBus _messageBus = new ();

        [Given(
            @"un candidat ""(.*)"" \(""(.*)""\) avec ""(.*)"" ans d’expériences qui est disponible ""(.*)"" à ""(.*)""")]
        public void GivenUnCandidatAvecAnsDExperiencesQuiEstDisponibleA(string language, string email,
            string experienceInYears, string date, string time)
        {
            var id = CandidatRepository().Next();
            _candidat = new Candidat(id, language, email, Int32.Parse(experienceInYears));
            var saveCandidatId = CandidatRepository().Save(_candidat);
            _candidat = new Candidat(saveCandidatId, _candidat.Language, _candidat.Email, _candidat.ExperienceEnAnnees);
            _disponibiliteDuCandidat =
                DateTime.ParseExact(date + " " + time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
        }

        [Given(@"un recruteur ""(.*)"" \(""(.*)""\) qui a ""(.*)"" ans d’XP qui est dispo ""(.*)"" à ""(.*)""")]
        public void GivenUnRecruteurQuiAAnsDxpQuiEstDispo(string language, string email, string experienceInYears,
            string date, string time)
        {
            _recruteur = new Recruteur(language, email, Int32.Parse(experienceInYears));
            var saveRecruteurId = RecruteurRepository().Save(_recruteur);
            _recruteur = new Recruteur(saveRecruteurId, _recruteur.Language, _recruteur.Email,
                _recruteur.ExperienceEnAnnees);
            _dateDeDisponibiliteDuRecruteur =
                DateTime.ParseExact(date + " " + time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
        }

        [When(@"on tente une planification d’entretien")]
        public void WhenOnTenteUnePlanificationDEntretien()
        {
            var _entretienCréeListener = new EntretienCréeListener(new RendreRecruteurIndisponibleCommandHandler(RecruteurRepository()), _messageBus);
            _planifierEntretienCommandHandler = new PlanifierEntretienCommandHandler(EntretienRepository(), _emailService, _messageBus);
            var entretienController =
                new EntretienController(_planifierEntretienCommandHandler, null, null, CandidatRepository(), RecruteurRepository());

            _createEntretienResponse = entretienController.Create(new CreateEntretienRequest(_candidat.Id,
                _recruteur.Id,
                _disponibiliteDuCandidat,
                _dateDeDisponibiliteDuRecruteur)) as CreatedAtActionResult;
        }

        [Then(@"L’entretien est planifié avec un status ""(.*)""")]
        public void ThenLEntretienEstPlanifie(String status)
        {
            Assert.IsType<CreatedAtActionResult>(_createEntretienResponse);
            Assert.IsType<CreateEntretienResponse>(_createEntretienResponse.Value);
            var createEntretienRequest = _createEntretienResponse.Value as CreateEntretienResponse;
            Assert.Equal(createEntretienRequest.EmailCandidat, _candidat.Email);
            Assert.Equal(createEntretienRequest.EmailRecruteur, _recruteur.Email);
            Assert.Equal(createEntretienRequest.Horaire, _disponibiliteDuCandidat);
            
            var createEntretienResponse = _createEntretienResponse.Value as CreateEntretienResponse;
            Assert.NotEqual(0, createEntretienResponse.EntretienId);

            Entretien entretien = EntretienRepository().FindById(createEntretienResponse.EntretienId);
            Status.TryParse<Status>(status, out var statusValue);
            Entretien expectedEntretien = Entretien.of(
                entretien.Id,
                new entretien.domain.Candidat(
                    _candidat.Id,
                    _candidat.Language,
                    _candidat.Email,
                    _candidat.ExperienceEnAnnees),
                new entretien.domain.Recruteur(
                    _recruteur.Id,
                    _recruteur.Language,
                    _recruteur.Email,
                    _recruteur.ExperienceEnAnnees),
                _disponibiliteDuCandidat,
                statusValue);
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
            Entretien entretien = EntretienRepository().FindByCandidat(_candidat.Email);
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

        [Then(@"le recruteur ""(.*)"" n'est plus disponible")]
        public void ThenLeRecruteurNestPlusDisponible(string email)
        {
            var recruteur = RecruteurRepository().FindByEmail(email);

            Assert.False(recruteur.EstDisponible);
        }
    }
}