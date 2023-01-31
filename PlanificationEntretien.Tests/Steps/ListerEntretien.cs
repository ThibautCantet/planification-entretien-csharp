using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Candidat = PlanificationEntretien.domain.candidat.Candidat;
using entretientCandidat = PlanificationEntretien.domain.entretien;
using Recruteur = PlanificationEntretien.domain.entretien.Recruteur;
using PlanificationEntretien.domain.entretien;
using PlanificationEntretien.infrastructure.controller;
using uc = PlanificationEntretien.use_case;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class ListerEntretien : ATest
    {
        private IActionResult _listerEntretientActionResult;

        [Given(@"les recruteurs existants")]
        public void GivenLesRecruteursExistants(Table table)
        {
            var recruteurs = table.Rows.Select(row => RecruteurATest.BuildRecruteur(row));
            foreach (var recruteur in recruteurs)
            {
                RecruteurRepository.Save(recruteur);
            }
        }

        [Given(@"les candidats existants")]
        public void GivenLesCandidatsExistants(Table table)
        {
            var candidats = table.Rows.Select(row =>
                new Candidat(int.Parse(row.Values.ToList()[0]), row.Values.ToList()[2], row.Values.ToList()[1], int.Parse(row.Values.ToList()[3])));
            foreach (var candidat in candidats)
            {
                CandidatRepository.Save(candidat);
            }
        }

        [Given(@"les entretiens existants")]
        public void GivenLesEntretiensExistants(Table table)
        {
            var entretiens = table.Rows.Select(row =>
                BuildEntretien(int.Parse(row.Values.ToList()[0]), row.Values.ToList()[1], row.Values.ToList()[2], row.Values.ToList()[3], row.Values.ToList()[4]));
            foreach (var entretien in entretiens)
            {
                EntretienRepository.Save(entretien);
            }
        }

        private Entretien BuildEntretien(int id, string emailRecruteur, string emailCandidat, string time, string status)
        {
            var recruteur = RecruteurRepository.FindByEmail(emailRecruteur);
            var candidat = CandidatRepository.FindByEmail(emailCandidat);
            var horaire = DateTime.ParseExact(time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
            Status.TryParse<Status>(status, out var statusValue);
            return Entretien.of(
                id,
                new entretientCandidat.Candidat(
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

        [When(@"on liste les tous les entretiens")]
        public void WhenOnListeLesTousLesEntretiens()
        {
            var listerEntretien = new uc.ListerEntretien(EntretienRepository);
            var entretienController = new EntretienController(null, listerEntretien, CandidatRepository, RecruteurRepository);
            _listerEntretientActionResult = entretienController.Lister();
        }

        [Then(@"on récupères les entretiens suivants")]
        public void ThenOnRecuperesLesEntretiensSuivants(Table table)
        {
            var okResult = Assert.IsType<OkObjectResult>(_listerEntretientActionResult);
            List<EntretienResponse> entretiensResponse = Assert.IsType<List<EntretienResponse>>(okResult.Value);
            
            var entretiens = table.Rows.Select(row =>
                BuildEntretienResponse(row.Values.ToList()[1], row.Values.ToList()[2], row.Values.ToList()[4], row.Values.ToList()[5]));
            Assert.Equal(entretiensResponse, entretiens);
        }
        
        private EntretienResponse BuildEntretienResponse(string emailRecruteur, string emailCandidat, string time, string status)
        {
            var horaire = DateTime.ParseExact(time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
            Status.TryParse<Status>(status, out var statusValue);
            return new EntretienResponse(emailCandidat, emailRecruteur, horaire, statusValue);
        }
    }
}