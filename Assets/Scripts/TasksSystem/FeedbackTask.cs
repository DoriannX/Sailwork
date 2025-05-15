using SailorSystems;
using UnityEngine;

namespace TasksSystem
{
    public class FeedbackTask : MonoBehaviour
    {
        private Task task;

        private void Awake()
        {
            task = GetComponent<Task>();
            
            task.onAvailable += OnTaskAvailable;
            task.onComplete += OnTaskComplete;
        }

        private void OnTaskComplete()
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }

        private void OnTaskAvailable()
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
}