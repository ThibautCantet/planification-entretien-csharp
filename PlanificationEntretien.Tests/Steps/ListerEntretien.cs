using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                RecruteurRepository.Save(recruteur);
            }
        }

        [Given(@"les candidats existants")]
        public void GivenLesCandidatsExistants(Table table)
        {
            var candidats = table.Rows.Select(row => new Candidat(int.Parse(row.Values.ToList()[0]), row.Values.ToList()[2], row.Values.ToList()[1], int.Parse(row.Values.ToList()[3])));
            foreach (var candidat in candidats)
            {
                CandidatRepository.Save(candidat);
            }
        }

        [Given(@"les entretiens existants")]
        public void GivenLesEntretiensExistants(Table table)
        {
            var entretiens = table.Rows.Select(row => BuildEntretien(row.Values.ToList()[1], row.Values.ToList()[2], row.Values.ToList()[3]));
            foreach (var entretien in entretiens)
            {
                EntretienRepository.Save(entretien);
            }
        }

        private Entretien BuildEntretien(string emailRecruteur, string emailCandidat, string time)
        {
            var recruteur = RecruteurRepository.FindByEmail(emailRecruteur);
            var candidat = CandidatRepository.FindByEmail(emailCandidat);
            var horaire = DateTime.ParseExact(time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
            return Entretien.of(candidat , recruteur, horaire);
        }

        [When(@"on liste les tous les entretiens")]
        public void WhenOnListeLesTousLesEntretiens()
        {
            var entretienService = new uc.ListerEntretien(EntretienRepository);
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