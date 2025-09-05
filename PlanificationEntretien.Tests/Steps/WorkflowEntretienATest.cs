using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain.entretien;
using PlanificationEntretien.domain.recruteur;
using PlanificationEntretien.infrastructure.controller;
using PlanificationEntretien.use_case;
using TechTalk.SpecFlow;
using Xunit;

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
                new Candidat.domain.Candidat(int.Parse(row.Values.ToList()[0]), row.Values.ToList()[2], row.Values.ToList()[1], int.Parse(row.Values.ToList()[3])));
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
                new EntretienController(null, null, validerEntretien, null);

            _validateEntretienResponse = entretienController.Valider(entretienId);
        }

        [When(@"on annule l'entretien (.*)")]
        public void WhenOnAnnuleLentretien(int entretienId)
        {
            _entretienId = entretienId;
            var annulerEntretien = new AnnulerEntretien(EntretienRepository);
            var entretienController =
                new EntretienController(null, null, null, annulerEntretien);

            _validateEntretienResponse = entretienController.Annuler(entretienId);
        }
        
        [Then(@"l'operation est un success")]
        public void ThenOperationEstUnSuccess()
        {
            Assert.IsType<OkResult>(_validateEntretienResponse);
        }
        
        [Then(@"la validation echoue")]
        public void ThenLaValidationEchoue()
        {
            Assert.IsType<BadRequestResult>(_validateEntretienResponse);
        }

        [Then(@"on récupères les entretiens suivants en base")]
        public void ThenOnRecuperesLesEntretiensSuivantsEnBase(Table table)
        {
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
            var statusValue = Status.Parse<Status>(status, true);
            return Entretien.of(
                id,
                new CandidatEvalué(
                    candidat.Id,
                    candidat.Language,
                    candidat.Email,
                    candidat.ExperienceEnAnnees),
                new RecruteurAssigné(
                    recruteur.Id,
                    recruteur.Language,
                    recruteur.Email,
                    recruteur.ExperienceEnAnnees),
                horaire,
                statusValue);
        }
    }
}