
namespace StateMachineSystem.Interfaces
{
    public interface ITransition {
        IState To { get; }
        IPredicate condition { get; }
    }
}