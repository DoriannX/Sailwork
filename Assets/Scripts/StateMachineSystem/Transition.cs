using StateMachineSystem.Interfaces;

namespace StateMachineSystem
{
    /// <summary>
    /// This class is responsible for the transition between states.
    /// </summary>
    public class Transition : ITransition
    {
        public IState to { get; }
        public IPredicate condition { get; }

        public Transition(IState to, IPredicate condition)
        {
            this.to = to;
            this.condition = condition;
        }
    }
}