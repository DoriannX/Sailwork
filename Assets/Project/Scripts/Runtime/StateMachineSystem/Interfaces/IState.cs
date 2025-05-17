
namespace Project.Scripts.Runtime.StateMachineSystem.Interfaces
{
    /// <summary>
    /// This interface is used to define a state in the state machine.
    /// </summary>
    public interface IState {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}