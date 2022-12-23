using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.application;
using PlanificationEntretien.domain;
using uc = PlanificationEntretien.use_case;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class ListerEntretien : ATest
    {
   
        private IEnumerable<IEntretien> _entretiens;

        [Given(@"les recruteurs existants")]
        public void GivenLesRecruteursExistants(Table table)
        {
            var recruteurs = table.Rows.Select(row => RecruteurATest.BuildRecruteur(row));
            foreach (var recruteur in recruteurs)
            {
                _recruteurPort.Save(recruteur);
            }
        }

        [Given(@"les candidats existants")]
        public void GivenLesCandidatsExistants(Table table)
        {
            var candidats = table.Rows.Select(row => new Candidat( row.Values.ToList()[2], row.Values.ToList()[1], int.Parse(row.Values.ToList()[3])));
            foreach (var candidat in candidats)
            {
                _candidatPort.Save(candidat);
            }
        }

        [Given(@"les entretiens existants")]
        public void GivenLesEntretiensExistants(Table table)
        {
            var entretiens = table.Rows.Select(row => BuildEntretien(row.Values.ToList()[1], row.Values.ToList()[2], row.Values.ToList()[3]));
            foreach (var entretien in entretiens)
            {
                _entretienPort.Save(entretien);
            }
        }

        private Entretien BuildEntretien(string emailRecruteur, string emailCandidat, string time)
        {
            var recruteur = _recruteurPort.FindByEmail(emailRecruteur);
            var candidat = _candidatPort.FindByEmail(emailCandidat);
            var horaire = DateTime.ParseExact(time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
            return new Entretien(candidat , recruteur, horaire);
        }

        [When(@"on liste les tous les entretiens")]
        public void WhenOnListeLesTousLesEntretiens()
        {
            var entretienService = new uc.ListerEntretien(_entretienPort);
            _entretiens = entretienService.Execute();
        }

        [Then(@"on récupères les entretiens suivants")]
        public void ThenOnRecuperesLesEntretiensSuivants(Table table)
        {
            var entretiens = table.Rows.Select(row => BuildEntretien(row.Values.ToList()[1], row.Values.ToList()[2], row.Values.ToList()[4]));
            Assert.Equal(_entretiens, entretiens);
        }
    }
}