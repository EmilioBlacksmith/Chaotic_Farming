using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class pingPong : MonoBehaviour
{
    public Vector3 finalSize;
    public Transform logos;

    private void OnEnable()
    {
        logos = transform;
        logos.DOScale(finalSize, 1.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
