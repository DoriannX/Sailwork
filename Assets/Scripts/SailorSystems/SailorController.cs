using System;
using Interfaces;
using StateMachineSystem;
using StateMachineSystem.Interfaces;
using StateMachineSystem.States;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SailorSystems
{
    public class SailorController : MonoBehaviour, IInteractable
    {
        [SerializeField] private Color defaultColor;
        
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
        

        private void Awake()
        {
            stateMachine = new StateMachine();
            taskManager = GetComponent<SailorTaskManager>();
            sailorFatigueManager = GetComponent<SailorFatigueManager>();
            taskManager.onTaskCompleted += sailorFatigueManager.AddFatigue;
            sailorMovement = GetComponent<SailorMovement>();
            spriteRenderers = GetComponentsInChildren<Renderer>();
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.material = new Material(spriteRenderer.material) { color = defaultColor };
            }
        }

        private void Start()
        {
            AvailableState availableState = new AvailableState(taskManager, sailorMovement);
            WaitingState waitingState = new WaitingState();
            DoingTaskState doingState = new DoingTaskState(taskManager);
            TiredState tiredState = new TiredState(sailorMovement, sailorFatigueManager);
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

        public void ResetColor()
        {
            sailorColor = defaultColor;
        }

        public void Select(bool state)
        {
            selected = state;
        }
    }
}