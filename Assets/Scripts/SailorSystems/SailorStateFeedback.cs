using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SailorSystems
{
    public class SailorStateFeedback : MonoBehaviour
    {
        private SailorController sailorController;
        private HoverFeedback hoverFeedback;
        [SerializeField] private Image tiredImage;
        [SerializeField] private Color availableColor;
        [SerializeField] private Color tiredColor;
        [SerializeField] private Color doingColor;
        [SerializeField] private Color waitingColor;


        private void Awake()
        {
            sailorController = GetComponent<SailorController>();
            hoverFeedback = GetComponent<HoverFeedback>();
        }

        private void Start()
        {
            tiredImage.gameObject.SetActive(false);
            sailorController.sailorColor = availableColor;
            sailorController.availableState.onStateEnter += OnAvailableStateEnter;
            sailorController.tiredState.onStateEnter += OnTiredStateEnter;
            sailorController.tiredState.onStateExit += () => tiredImage.gameObject.SetActive(false);
            sailorController.doingState.onStateEnter += () => sailorController.sailorColor = doingColor;
            sailorController.waitingState.onStateEnter += () => sailorController.sailorColor = waitingColor;
            sailorController.availableState.onStateExit += OnAvailableStateExit;
        }

        private void OnTiredStateEnter()
        {
            sailorController.sailorColor = tiredColor;
            tiredImage.gameObject.SetActive(true);
        }

        private void OnAvailableStateEnter()
        {
            sailorController.sailorColor = availableColor;
            hoverFeedback.SetActive(true);
        }

        private void OnAvailableStateExit()
        {
            hoverFeedback.SetActive(false);
        }
    }
}