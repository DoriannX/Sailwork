using UnityEngine;

namespace SailorSystems
{
    public class Task : MonoBehaviour
    {
        public bool done { get; private set; } = false;
        [SerializeField] private TaskInfo info;
    }
}