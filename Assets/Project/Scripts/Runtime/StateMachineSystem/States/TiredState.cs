using System;
using Project.Scripts.Runtime.Gameplay.Sailors;

namespace Project.Scripts.Runtime.StateMachineSystem.States
{
    /// <summary>
    /// This class is responsible for managing the state of a sailor when they are doing a task.
    /// </summary>
    public class TiredState : BaseState
    {
        private readonly SailorMovement sailorMovement;
        private readonly SailorFatigueManager sailorFatigueManager;
        public static event Action onStarted;
        public TiredState(SailorMovement sailorMovement, SailorFatigueManager sailorFatigueManager)
        {
            this.sailorMovement = sailorMovement;
            this.sailorFatigueManager = sailorFatigueManager;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            onStarted?.Invoke();
            sailorMovement.GoTo(sailorFatigueManager.restPointPos);
        }
    }
}