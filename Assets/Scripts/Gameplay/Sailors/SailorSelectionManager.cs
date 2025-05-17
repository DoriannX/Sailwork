using UnityEngine;

namespace Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for managing the selection state of a sailor.
    /// </summary>
    public class SailorSelectionManager : MonoBehaviour
    {
        public bool selected { get; private set; }

        public void Select(bool state)
        {
            selected = state;
        }
    }
}