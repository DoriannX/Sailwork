using NUnit.Framework;
using SelectionSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for managing the feedback of the sailor's state.
    /// </summary>
    [RequireComponent(typeof(SailorController))]
    [RequireComponent(typeof(SailorColorManager))]
    [RequireComponent(typeof(HoverFeedback))]
    public class SailorStateFeedback : MonoBehaviour
    {
        #region Private References
        private SailorController sailorController;
        private SailorColorManager sailorColorManager;
        private HoverFeedback hoverFeedback;
        #endregion
        
        #region Serialized Fields
        [Header("UI Elements")]
        [SerializeField] private Image tiredImage;
        
        [Header("State Colors")]
        [SerializeField] private Color availableColor;
        [SerializeField] private Color tiredColor;
        [SerializeField] private Color doingColor;
        [SerializeField] private Color waitingColor;
        #endregion

        private void Awake()
        {
            Assert.IsNotNull(tiredImage);
            InitializeComponents();
        }

        private void Start()
        {
            InitializeFeedbacks();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            sailorController.availableState.onStateEnter += OnAvailableStateEnter;
            sailorController.tiredState.onStateEnter += OnTiredStateEnter;
            sailorController.tiredState.onStateExit += () => tiredImage.gameObject.SetActive(false);
            sailorController.doingState.onStateEnter += () => sailorColorManager.SetSailorColor(doingColor);
            sailorController.waitingState.onStateEnter += () => sailorColorManager.SetSailorColor(waitingColor);
            sailorController.availableState.onStateExit += OnAvailableStateExit;
        }

        private void InitializeFeedbacks()
        {
            tiredImage.gameObject.SetActive(false);
            sailorColorManager.SetSailorColor(availableColor);
        }

        private void InitializeComponents()
        {
            sailorController = GetComponent<SailorController>();
            sailorColorManager = GetComponent<SailorColorManager>();
            hoverFeedback = GetComponent<HoverFeedback>();
        }

        private void OnTiredStateEnter()
        {
            sailorColorManager.SetSailorColor(tiredColor);
            tiredImage.gameObject.SetActive(true);
        }

        private void OnAvailableStateEnter()
        {
            sailorColorManager.SetSailorColor(availableColor); 
            hoverFeedback.SetActive(true);
        }

        private void OnAvailableStateExit()
        {
            hoverFeedback.SetActive(false);
        }
    }
}