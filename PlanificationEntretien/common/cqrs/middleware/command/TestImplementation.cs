namespace PlanificationEntretien.common.cqrs.middleware.command;

public class TestImplementation
{
    public ICommandHandler<Command> Get()
    {
        return null; //new MyCommandHandler();
    }

    public interface ICommandHandler<C> where C : Command
    {
        int Handle(C command);
    }

    public interface Command // Updated to public
    {

    }

    public class MyCommand : Command
    {

    }

    public class MyCommandHandler : ICommandHandler<MyCommand> // Updated to use ICommandHandler
    {
        public int Handle(MyCommand command) // Updated to public
        {
            throw new System.NotImplementedException();
        }
    }
}