using Gameplay.Sailors;

namespace StateMachineSystem.States
{
    /// <summary>
    /// This class is responsible for the available state of the sailor.
    /// </summary>
    public class AvailableState : BaseState
    {
        private readonly SailorTaskManager taskManager;
        private readonly SailorMovement movement;

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

        /// <summary>
        /// This method is used to search for a task and mop while doing it if no task is available.
        /// </summary>
        private void SearchForTask()
        {
            if (taskManager.IsTaskAvailable())
            {
                movement.GoTo(taskManager.GetNextTask().transform.position);
            }
            else
            {
                taskManager.AskNearestTask();
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