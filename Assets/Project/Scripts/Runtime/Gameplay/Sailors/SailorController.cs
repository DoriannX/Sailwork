using Project.Scripts.Runtime.Core.Interfaces;
using Project.Scripts.Runtime.StateMachineSystem;
using Project.Scripts.Runtime.StateMachineSystem.Interfaces;
using Project.Scripts.Runtime.StateMachineSystem.States;
using UnityEngine;

namespace Project.Scripts.Runtime.Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for managing the state of the sailor.
    /// </summary>
    [RequireComponent(typeof(SailorTaskManager))]
    [RequireComponent(typeof(SailorFatigueManager))]
    [RequireComponent(typeof(SailorMovement))]
    [RequireComponent(typeof(SailorSelectionManager))]
    public class SailorController : MonoBehaviour, IInteractable
    {
        #region State Machine

        private StateMachine stateMachine;

        #endregion

        #region Managers

        public SailorTaskManager taskManager { get; private set; }
        private SailorFatigueManager sailorFatigueManager;
        private SailorMovement sailorMovement;
        public SailorSelectionManager sailorSelectionManager { get; private set; }

        #endregion

        #region States

        public TiredState tiredState { get; private set; }
        public AvailableState availableState { get; private set; }
        public WaitingState waitingState { get; private set; }
        public DoingTaskState doingState { get; private set; }

        #endregion


        private void Awake()
        {
            InitializeComponents();
            CreateStateMachine();
            taskManager.onTaskCompleted += sailorFatigueManager.AddFatigue;
        }

        private void Start()
        {
            InitializeTransitions();
            stateMachine.SetState(availableState);
        }

        private void Update()
        {
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        private void CreateStateMachine()
        {
            stateMachine = new StateMachine();
            availableState = new AvailableState(taskManager, sailorMovement);
            waitingState = new WaitingState();
            doingState = new DoingTaskState(taskManager);
            tiredState = new TiredState(sailorMovement, sailorFatigueManager);
        }

        private void InitializeComponents()
        {
            taskManager = GetComponent<SailorTaskManager>();
            sailorFatigueManager = GetComponent<SailorFatigueManager>();
            sailorMovement = GetComponent<SailorMovement>();
            sailorSelectionManager = GetComponent<SailorSelectionManager>();
        }

        /// <summary>
        /// This method is used to add transitions to different states based on different
        /// conditions using a predicate system. Scalability is achieved by using the predicate system.
        /// </summary>
        private void InitializeTransitions()
        {
            Any(tiredState, new FuncPredicate(() => sailorFatigueManager.fatiguePercentage >= 1));
            Any(availableState, new FuncPredicate(ReturnToAvailable));
            At(availableState, doingState, new FuncPredicate(() => taskManager.IsAtTask()));
            At(availableState, waitingState, new FuncPredicate(() => sailorSelectionManager.selected));
            At(doingState, waitingState,
                new FuncPredicate(() => !taskManager.isWorking && sailorSelectionManager.selected));
        }

        /// <summary>
        /// This function is used to check if the sailor is available to return to the available state.
        /// </summary>
        /// <returns> if the sailor has to return to avaiable state </returns>
        private bool ReturnToAvailable()
        {
            return sailorFatigueManager.fatiguePercentage < 1 && !sailorFatigueManager.isResting &&
                   !taskManager.isWorking && !sailorSelectionManager.selected && !taskManager.IsAtTask();
        }

        /// <summary>
        /// Helper function to add transitions to the state machine.
        /// </summary>
        /// <param name="from"> the start state </param>
        /// <param name="to"> the state to transition to </param>
        /// <param name="predicate"> the predicate to check the conditions </param>
        private void At(IState from, IState to, IPredicate predicate)
        {
            stateMachine.AddTransition(from, to, predicate);
        }

        /// <summary>
        /// Helper function to add any state transitions to the state machine.
        /// </summary>
        /// <param name="to"> the state to transition to</param>
        /// <param name="predicate"> the predicate to check the conditions </param>
        private void Any(IState to, IPredicate predicate)
        {
            stateMachine.AddAnyTransition(to, predicate);
        }
    }
}