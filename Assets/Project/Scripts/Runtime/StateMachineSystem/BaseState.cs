using System;
using Project.Scripts.Runtime.StateMachineSystem.Interfaces;

namespace Project.Scripts.Runtime.StateMachineSystem
{
    /// <summary>
    /// This class is the base class for all states in the state machine.
    /// </summary>
    public abstract class BaseState : IState {
        public event Action onStateEnter;
        public event Action onStateExit;
    
        protected const float crossFadeDuration = 0.1f;
    
        public virtual void OnEnter() {
            // noop
            onStateEnter?.Invoke();
        }

        public virtual void Update() {
            // noop
        }

        public virtual void FixedUpdate() {
            // noop
        }

        public virtual void OnExit() {
            // noop
            onStateExit?.Invoke();
        }
    }
}