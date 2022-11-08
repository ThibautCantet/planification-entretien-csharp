using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PlanificationEntretien.domain;
using PlanificationEntretien.infrastructure.memory;
using uc = PlanificationEntretien.use_case;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class ListerEntretien
    {
        private readonly IEntretienRepository _entretienRepository = new InMemoryEntretienRepository();
        private IRecruteurRepository _recruteurRepository = new InMemoryRecruteurRepository();
        private ICandidatRepository _candidatRepository = new InMemoryCandidatRepository();
        private IEnumerable<Entretien> _entretiens;

        [Given(@"les recruteurs existants")]
        public void GivenLesRecruteursExistants(Table table)
        {
            var values = new List<string>(table.Rows[0].Values);
            var recruteurs = table.Rows.Select(row => new Recruteur( values[2], values[1], int.Parse(values[3])));
            foreach (var recruteur in recruteurs)
            {
                _recruteurRepository.Save(recruteur);
            }
        }

        [Given(@"les candidats existants")]
        public void GivenLesCandidatsExistants(Table table)
        {
            var values = new List<string>(table.Rows[0].Values);
            var candidats = table.Rows.Select(row => new Candidat( values[2], values[1], int.Parse(values[3])));
            foreach (var candidat in candidats)
            {
                _candidatRepository.Save(candidat);
            }
        }

        [Given(@"les entretiens existants")]
        public void GivenLesEntretiensExistants(Table table)
        {
            var values = new List<string>(table.Rows[0].Values);
            var entretiens = table.Rows.Select(row => BuildEntretien(values, values[1], values[2], values[3]));
            foreach (var entretien in entretiens)
            {
                _entretienRepository.Save(entretien);
            }
        }

        private Entretien BuildEntretien(List<string> values, string emailRecruteur, string emailCandidat, string time)
        {
            var recruteur = _recruteurRepository.FindByEmail(emailRecruteur);
            var candidat = _candidatRepository.FindByEmail(emailCandidat);
            var horaire = DateTime.ParseExact(time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
            return new Entretien(candidat , recruteur, horaire);
        }

        [When(@"on liste les tous les entretiens")]
        public void WhenOnListeLesTousLesEntretiens()
        {
            var entretienService = new uc.ListerEntretien(_entretienRepository);
            _entretiens = entretienService.Execute();
        }

        [Then(@"on récupères les entretiens suivants")]
        public void ThenOnRecuperesLesEntretiensSuivants(Table table)
        {
            var values = new List<string>(table.Rows[0].Values);
            var entretiens = table.Rows.Select(row => BuildEntretien(values, values[1], values[2], values[4]));
            Assert.Equal(_entretiens, entretiens);
        }
    }
}