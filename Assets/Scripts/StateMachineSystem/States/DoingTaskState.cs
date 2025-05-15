using SailorSystems;
using UnityEngine;

namespace StateMachineSystem.States
{
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