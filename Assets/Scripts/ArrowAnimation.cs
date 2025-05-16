using DG.Tweening;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 1.0f;
    [SerializeField] private float rotationAmount = 360f;
    [SerializeField] private Ease rotationEase = Ease.Linear;

    private void OnEnable()
    {
        transform.DOLocalRotate(new Vector3(0, 0, rotationAmount), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(rotationEase)
            .SetLoops(-1, LoopType.Restart);
    }
    
    private void OnDisable()
    {
        transform.DOKill();
        transform.localRotation = Quaternion.identity;
    }
}