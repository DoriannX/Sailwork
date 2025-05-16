using Interfaces;
using StateMachineSystem;
using StateMachineSystem.Interfaces;
using StateMachineSystem.States;
using UnityEngine;

namespace SailorSystems
{
    public class SailorController : MonoBehaviour, IInteractable
    {
        
        private StateMachine stateMachine;
        public SailorTaskManager taskManager { get; private set; }
        private SailorFatigueManager sailorFatigueManager;
        private SailorMovement sailorMovement;
        private Renderer[] spriteRenderers;
        private Color _sailorColor;
        public Color sailorColor
        {
            get => _sailorColor;
            set
            {
                _sailorColor = value;
                foreach (var spriteRenderer in spriteRenderers)
                {
                    spriteRenderer.material.color = value;
                }
            }
        }

        private bool selected;
        public TiredState tiredState { get; private set; }
        public AvailableState availableState { get; private set; }
        public WaitingState waitingState { get; private set; }
        public DoingTaskState doingState { get; private set; }


        private void Awake()
        {
            stateMachine = new StateMachine();
            taskManager = GetComponent<SailorTaskManager>();
            sailorFatigueManager = GetComponent<SailorFatigueManager>();
            taskManager.onTaskCompleted += sailorFatigueManager.AddFatigue;
            sailorMovement = GetComponent<SailorMovement>();
            spriteRenderers = GetComponentsInChildren<Renderer>();
            availableState = new AvailableState(taskManager, sailorMovement);
            waitingState = new WaitingState();
            doingState = new DoingTaskState(taskManager);
            tiredState = new TiredState(sailorMovement, sailorFatigueManager);
        }

        private void Start()
        {
            Any(tiredState, new FuncPredicate(() => sailorFatigueManager.fatiguePercentage >= 1));
            Any(availableState, new FuncPredicate(ReturnToAvailable));
            At(availableState, doingState, new FuncPredicate(() => taskManager.IsAtTask()));
            At(availableState, waitingState, new FuncPredicate(() => selected));
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

        private bool ReturnToAvailable()
        {
            return sailorFatigueManager.fatiguePercentage < 1 && !sailorFatigueManager.isResting && !taskManager.isWorking && !selected && !taskManager.IsAtTask();
        }

        private void At(IState from, IState to, IPredicate predicate)
        {
            stateMachine.AddTransition(from, to, predicate);
        }

        private void Any(IState to, IPredicate predicate)
        {
            stateMachine.AddAnyTransition(to, predicate);
        }

        public void Select(bool state)
        {
            selected = state;
        }
    }
}