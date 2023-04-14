namespace PlanificationEntretien.entretien.query.application;

public interface IEntretienProjectionDao
{
    void IncrementEntretienAnnule();

    int EntretiensAnnules();
}