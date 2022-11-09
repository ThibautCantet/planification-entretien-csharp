using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PlanificationEntretien.domain;
using PlanificationEntretien.infrastructure.memory;
using PlanificationEntretien.use_case;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class ListerEntretien
    {
        private readonly IEntretienPort _entretienPort = new InMemoryEntretienAdapter();
        private IRecruteurPort _recruteurPort = new InMemoryRecruteurAdapter();
        private ICandidatPort _candidatPort = new InMemoryCandidatAdapter();
        private IEnumerable<Entretien> _entretiens;

        [Given(@"les recruteurs existants")]
        public void GivenLesRecruteursExistants(Table table)
        {
            var values = new List<string>(table.Rows[0].Values);
            var recruteurs = table.Rows.Select(row => new Recruteur( values[2], values[1], int.Parse(values[3])));
            foreach (var recruteur in recruteurs)
            {
                _recruteurPort.Save(recruteur);
            }
        }

        [Given(@"les candidats existants")]
        public void GivenLesCandidatsExistants(Table table)
        {
            var values = new List<string>(table.Rows[0].Values);
            var candidats = table.Rows.Select(row => new Candidat( values[2], values[1], int.Parse(values[3])));
            foreach (var candidat in candidats)
            {
                _candidatPort.Save(candidat);
            }
        }

        [Given(@"les entretiens existants")]
        public void GivenLesEntretiensExistants(Table table)
        {
            var values = new List<string>(table.Rows[0].Values);
            var entretiens = table.Rows.Select(row => BuildEntretien(values, values[1], values[2], values[3]));
            foreach (var entretien in entretiens)
            {
                _entretienPort.Save(entretien);
            }
        }

        private Entretien BuildEntretien(List<string> values, string emailRecruteur, string emailCandidat, string time)
        {
            var recruteur = _recruteurPort.FindByEmail(emailRecruteur);
            var candidat = _candidatPort.FindByEmail(emailCandidat);
            var horaire = DateTime.ParseExact(time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
            return new Entretien(candidat , recruteur, horaire);
        }

        [When(@"on liste les tous les entretiens")]
        public void WhenOnListeLesTousLesEntretiens()
        {
            var entretienService = new EntretienService(_entretienPort, null, _candidatPort, _recruteurPort);
            _entretiens = entretienService.ListerEntretiens();
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