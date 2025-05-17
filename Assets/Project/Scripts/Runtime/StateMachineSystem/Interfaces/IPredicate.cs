namespace Project.Scripts.Runtime.StateMachineSystem.Interfaces
{
    /// <summary>
    /// This interface is used to evaluate a condition for a transition in the state machine.
    /// </summary>
    public interface IPredicate {
        bool Evaluate();
    }
}