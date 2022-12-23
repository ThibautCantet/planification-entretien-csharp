using PlanificationEntretien.domain;
using PlanificationEntretien.infrastructure.memory;

namespace PlanificationEntretien.Steps;

public abstract class ATest
{
    protected IEntretienPort _entretienPort = new InMemoryEntretienAdapter();
    protected IRecruteurPort _recruteurPort = new InMemoryRecruteurAdapter();
    protected ICandidatPort _candidatPort = new InMemoryCandidatAdapter();
}