using Project.Scripts.Runtime.Gameplay.Sailors;

namespace Project.Scripts.Runtime.StateMachineSystem.States
{
    /// <summary>
    /// This class is responsible for managing the state of a sailor when they are doing a task.
    /// </summary>
    public class DoingTaskState : BaseState
    {
        private readonly SailorTaskManager taskManager;

        public DoingTaskState(SailorTaskManager taskManager)
        {
            this.taskManager = taskManager;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            taskManager.StartTask();
        }

        public override void Update()
        {
            base.Update();
            taskManager.Work();
        }
    }
}