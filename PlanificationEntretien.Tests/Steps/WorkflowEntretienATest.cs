using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain.entretien;
using PlanificationEntretien.infrastructure.controller;
using PlanificationEntretien.application_service.entretien;
using TechTalk.SpecFlow;
using Xunit;
using candidat = PlanificationEntretien.domain.candidat;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class WorkflowEntretienATest : ATest
    {
        private IActionResult _validateEntretienResponse;
        private int _entretienId;

        [Given(@"les recruteurs existants ci-dessous")]
        public void GivenLesRecruteursExistantsCiDessous(Table table)
        {
            var recruteurs = table.Rows.Select(row => RecruteurATest.BuildRecruteur(row));
            foreach (var recruteur in recruteurs)
            {
                RecruteurRepository.Save(recruteur);
            }
        }

        [Given(@"les candidats existants ci-dessous")]
        public void GivenLesCandidatsExistantsCiDessous(Table table)
        {
            var candidats = table.Rows.Select(row =>
                new candidat.Candidat(int.Parse(row.Values.ToList()[0]), row.Values.ToList()[2], row.Values.ToList()[1], int.Parse(row.Values.ToList()[3])));
            foreach (var candidat in candidats)
            {
                CandidatRepository.Save(candidat);
            }
        }

        [Given(@"les entretiens existants ci-dessous")]
        public void GivenLesEntretiensExistantsCiDessous(Table table)
        {
            var entretiens = table.Rows.Select(row =>
                BuildEntretien(int.Parse(row.Values.ToList()[0]), row.Values.ToList()[1], row.Values.ToList()[2], row.Values.ToList()[3], row.Values.ToList()[4]));
            foreach (var entretien in entretiens)
            {
                EntretienRepository.Save(entretien);
            }
        }

        [When(@"on valide l'entretien (.*)")]
        public void WhenOnValideLentretien(int entretienId)
        {
            _entretienId = entretienId;
            var validerEntretien = new ValiderEntretien(EntretienRepository);
            var entretienController =
                new EntretienController(null, null, validerEntretien, CandidatRepository, RecruteurRepository);

            _validateEntretienResponse = entretienController.Valider(entretienId);
        }

        [Then(@"on récupères les entretiens suivants en base")]
        public void ThenOnRecuperesLesEntretiensSuivantsEnBase(Table table)
        {
            Assert.IsType<OkResult>(_validateEntretienResponse);

            var entretien = EntretienRepository.FindById(_entretienId);
            var entretiens = table.Rows.Select(row =>
                BuildEntretien(int.Parse(row.Values.ToList()[0]), row.Values.ToList()[1], row.Values.ToList()[2], row.Values.ToList()[4], row.Values.ToList()[5]));
            Assert.Equal(entretien, entretiens.First());
        }

        private Entretien BuildEntretien(int id, string emailRecruteur, string emailCandidat, string time, string status)
        {
            var recruteur = RecruteurRepository.FindByEmail(emailRecruteur);
            var candidat = CandidatRepository.FindByEmail(emailCandidat);
            var horaire = DateTime.ParseExact(time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
            Status.TryParse<Status>(status, out var statusValue);
            return Entretien.of(
                id,
                new Candidat(
                    candidat.Id,
                    candidat.Language,
                    candidat.Email,
                    candidat.ExperienceEnAnnees),
                new Recruteur(
                    recruteur.Id,
                    recruteur.Language,
                    recruteur.Email,
                    recruteur.ExperienceEnAnnees),
                horaire,
                statusValue);
        }
    }
}