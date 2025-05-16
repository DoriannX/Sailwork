using System;
using StateMachineSystem.Interfaces;

namespace StateMachineSystem
{
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