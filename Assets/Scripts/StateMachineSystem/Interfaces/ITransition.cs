
namespace StateMachineSystem.Interfaces
{
    /// <summary>
    /// This interface is used to define a transition between states in a state machine.
    /// </summary>
    public interface ITransition {
        IState to { get; }
        IPredicate condition { get; }
    }
}