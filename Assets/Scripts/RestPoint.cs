using System.Collections.Generic;
using System.Linq;
using SailorSystems;
using UnityEngine;

public class RestPoint : MonoBehaviour
{
    [SerializeField, Min(0.01f)] private float timeToRest = 5f;
    private List<SailorFatigueManager> sailorsFatigueManagers = new();

    private void OnTriggerEnter(Collider other)
    {
        SailorFatigueManager sailorFatigueManager = other.GetComponent<SailorFatigueManager>();
        if (sailorFatigueManager != null && sailorFatigueManager.fatiguePercentage >= 1)
        {
            sailorsFatigueManagers.Add(sailorFatigueManager);
        }
    }

    private void Update()
    {
        if (sailorsFatigueManagers.Count <= 0)
        {
            return;
        }
        foreach (var sailorFatigueManager in sailorsFatigueManagers.ToList())
        {
            sailorFatigueManager.Rest(Time.deltaTime / timeToRest);
            if (sailorFatigueManager.fatiguePercentage <= 0)
            {
                sailorsFatigueManagers.Remove(sailorFatigueManager);
            }
        }
    }
}
