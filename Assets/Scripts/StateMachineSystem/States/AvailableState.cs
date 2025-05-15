using SailorSystems;
using UnityEngine;

namespace StateMachineSystem.States
{
    public class AvailableState : BaseState
    {
        private SailorTaskManager taskManager;
        private SailorMovement movement;
        public AvailableState(SailorTaskManager taskManager, SailorMovement movement)
        {
            this.taskManager = taskManager;
            this.movement = movement;
        }

        public override void Update()
        {
            base.Update();
            SearchForTask();
        }

        private void SearchForTask()
        {
            if (taskManager.IsTaskAvailable())
            {
                movement.GoTo(taskManager.GetNearestTaskPosition());
            }
            else
            {
                movement.Mop();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            movement.Stop();
        }
    }
}