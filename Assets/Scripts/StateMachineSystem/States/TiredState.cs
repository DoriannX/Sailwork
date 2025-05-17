using Gameplay.Sailors;

namespace StateMachineSystem.States
{
    /// <summary>
    /// This class is responsible for managing the state of a sailor when they are doing a task.
    /// </summary>
    public class TiredState : BaseState
    {
        private readonly SailorMovement sailorMovement;
        private readonly SailorFatigueManager sailorFatigueManager;
        public TiredState(SailorMovement sailorMovement, SailorFatigueManager sailorFatigueManager)
        {
            this.sailorMovement = sailorMovement;
            this.sailorFatigueManager = sailorFatigueManager;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            sailorMovement.GoTo(sailorFatigueManager.restPointPos);
        }
    }
}