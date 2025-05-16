using SailorSystems;

namespace StateMachineSystem.States
{
    public class TiredState : BaseState
    {
        private SailorMovement sailorMovement;
        private SailorFatigueManager sailorFatigueManager;
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