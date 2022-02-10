using System;
using System.Globalization;
using Moq;
using Xunit;

namespace PlanificationEntretien
{
    public class EntretienServiceUTest
    {
        private static readonly Mock<IEntretienRepository> EntretienRepository = new Mock<IEntretienRepository>();
        private static readonly Mock<IEmailService> EmailService = new Mock<IEmailService>();

        private readonly EntretienService _entretienService = new EntretienService(EntretienRepository.Object, EmailService.Object);

        private readonly Candidat _candidat = new Candidat("Java", "candidat@mail.com", 5);
        private readonly Recruteur _recruteur = new Recruteur("Java", "recruteur@soat.fr", 10);

        private readonly DateTime _disponibiliteCandidat =
            DateTime.ParseExact($"10/02/2022 12:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

        private readonly DateTime _disponibiliteRecruteur =
            DateTime.ParseExact($"10/02/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture);

        [Fact]
        void Should_save_entretien()
        {
            // when
            _entretienService.planifier(_candidat, _recruteur, _disponibiliteCandidat, _disponibiliteRecruteur);

            // then
            var entretien = new Entretien(_disponibiliteCandidat, "candidat@mail.com", "recruteur@soat.fr");
            EntretienRepository.Verify(repository => repository.Save(It.Is<Entretien>(e => e.Equals(entretien))));
        }
        
        [Fact]
        void Should_return_saved_entretien() {
            // given
            var entretien = new Entretien(_disponibiliteCandidat, "candidat@mail.com", "recruteur@soat.fr");
            EntretienRepository.Setup(x => x.Save(It.IsAny<Entretien>())).Returns(entretien);

            // when
            Entretien result = _entretienService.planifier(_candidat, _recruteur, _disponibiliteCandidat, _disponibiliteRecruteur);

            // then
            Assert.Equal(entretien, result);
        }
        
        [Fact]
        void Should_send_emails_to_candidat_and_recruteur() {
            // when
            _entretienService.planifier(_candidat, _recruteur, _disponibiliteCandidat, _disponibiliteRecruteur);

            // then
            EmailService.Verify(emailService => emailService.SendToCandidat("candidat@mail.com"));
            EmailService.Verify(emailService => emailService.SendToRecruteur("recruteur@soat.fr"));
        }
    }
}