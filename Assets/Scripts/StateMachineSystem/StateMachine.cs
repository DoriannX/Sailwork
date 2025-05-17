using System;
using System.Collections.Generic;
using StateMachineSystem.Interfaces;

namespace StateMachineSystem
{
    /// <summary>
    /// This class is responsible for managing the state machine.
    /// </summary>
    public class StateMachine {
        private StateNode current;
        private readonly Dictionary<Type, StateNode> nodes = new();
        private readonly HashSet<ITransition> anyTransitions = new();

        /// <summary>
        /// This method is used to check if the state machine has to transition to a new state and update the current state.
        /// </summary>
        public void Update() {
            ITransition transition = GetTransition();
            if (transition != null) 
                ChangeState(transition.to);
            
            current.state?.Update();
        }
        
        public void FixedUpdate() {
            current.state?.FixedUpdate();
        }

        public void SetState(IState state) {
            current = nodes[state.GetType()];
            current.state?.OnEnter();
        }

        /// <summary>
        /// This method is used to change the current state of the state machine
        /// by calling the OnExit method of the previous state and the OnEnter method of the new state.
        /// </summary>
        /// <param name="state"></param>
        private void ChangeState(IState state) {
            if (state == current.state) return;
            
            IState previousState = current.state;
            IState nextState = nodes[state.GetType()].state;
            
            previousState?.OnExit();
            nextState?.OnEnter();
            current = nodes[state.GetType()];
        }


        /// <summary>
        /// This method is used to get first the any transition that is valid and then the current state transition.
        /// </summary>
        /// <returns></returns>
        private ITransition GetTransition() {
            foreach (ITransition transition in anyTransitions)
                if (transition.condition.Evaluate())
                    return transition;
            
            foreach (ITransition transition in current.transitions)
                if (transition.condition.Evaluate())
                    return transition;
            
            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition) {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).state, condition);
        }
        
        public void AddAnyTransition(IState to, IPredicate condition) {
            anyTransitions.Add(new Transition(GetOrAddNode(to).state, condition));
        }

        /// <summary>
        /// This method is used to check if the state is already in the state machine,
        /// if not, it will add it and return it
        /// </summary>
        /// <param name="state"> the state to check</param>
        /// <returns> the node added to the state machine </returns>
        private StateNode GetOrAddNode(IState state) {
            StateNode node = nodes.GetValueOrDefault(state.GetType());

            if (node != null)
            {
                return node;
            }
            node = new StateNode(state);
            nodes.Add(state.GetType(), node);

            return node;
        }

        /// <summary>
        /// This class is responsible for managing the state nodes of the state machine.
        /// </summary>
        private class StateNode {
            public IState state { get; }
            public HashSet<ITransition> transitions { get; }
            
            public StateNode(IState state) {
                this.state = state;
                transitions = new HashSet<ITransition>();
            }
            
            public void AddTransition(IState to, IPredicate condition) {
                transitions.Add(new Transition(to, condition));
            }
        }
    }
}