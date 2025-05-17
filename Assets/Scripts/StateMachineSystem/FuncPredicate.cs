using System;
using StateMachineSystem.Interfaces;

namespace StateMachineSystem
{
    /// <summary>
    /// This class is responsible for evaluating a condition using a function.
    /// </summary>
    public class FuncPredicate : IPredicate {
        private readonly Func<bool> func;
        
        public FuncPredicate(Func<bool> func) {
            this.func = func;
        }
        
        public bool Evaluate() => func.Invoke();
    }
}