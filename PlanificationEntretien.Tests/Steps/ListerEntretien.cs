using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PlanificationEntretien.model;
using PlanificationEntretien.repository;
using PlanificationEntretien.service;
using TechTalk.SpecFlow;
using Xunit;

namespace PlanificationEntretien.Steps
{
    [Binding]
    public class ListerEntretien
    {
        private readonly EntretienRepository _inMemoryEntretienRepository = new EntretienRepository();
        private RecruteurRepository _inMemoryRecruteurRepository = new RecruteurRepository();
        private CandidatRepository _inMemoryCandidatRepository = new CandidatRepository();
        private IEnumerable<Entretien> _entretiens;

        [Given(@"les recruteurs existants")]
        public void GivenLesRecruteursExistants(Table table)
        {
            var recruteurs = table.Rows.Select(row => new Recruteur( row[2], row[1], int.Parse(row[3])));
            foreach (var recruteur in recruteurs)
            {
                _inMemoryRecruteurRepository.Save(recruteur);
            }
        }

        [Given(@"les candidats existants")]
        public void GivenLesCandidatsExistants(Table table)
        {
            var candidats = table.Rows.Select(row => new Candidat( row[2], row[1], int.Parse(row[3])));
            foreach (var candidat in candidats)
            {
                _inMemoryCandidatRepository.Save(candidat);
            }
        }

        [Given(@"les entretiens existants")]
        public void GivenLesEntretiensExistants(Table table)
        {
            var entretiens = table.Rows.Select(row => BuildEntretien(row[1], row[2], row[3]));
            foreach (var entretien in entretiens)
            {
                _inMemoryEntretienRepository.Save(entretien);
            }
        }

        private Entretien BuildEntretien(string emailRecruteur, string emailCandidat, string time)
        {
            var recruteur = _inMemoryRecruteurRepository.FindByEmail(emailRecruteur);
            var candidat = _inMemoryCandidatRepository.FindByEmail(emailCandidat);
            var horaire = DateTime.ParseExact(time, "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture);
            return new Entretien(candidat , recruteur, horaire);
        }

        [When(@"on liste les tous les entretiens")]
        public void WhenOnListeLesTousLesEntretiens()
        {
            var entretienService = new EntretienService(_inMemoryEntretienRepository, null, _inMemoryCandidatRepository, _inMemoryRecruteurRepository);
            _entretiens = entretienService.ListerEntretiens();
        }

        [Then(@"on récupères les entretiens suivants")]
        public void ThenOnRecuperesLesEntretiensSuivants(Table table)
        {
            var entretiens = table.Rows.Select(row => BuildEntretien(row[1], row[2], row[4]));
            Assert.Equal(_entretiens, entretiens);
        }
    }
}