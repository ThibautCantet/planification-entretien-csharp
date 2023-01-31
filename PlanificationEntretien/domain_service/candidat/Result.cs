using Planification_Entretien.domain;

namespace Planification_Entretien.domain_service.candidat;

public record Result<T>(Event Event, T Value);