using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class introScript : MonoBehaviour
{
    public Image whitescreen;
    public Transform logos;
    public Vector3 finalSize;
    public MenuChanger menu;

    private void Start()
    {
        whitescreen.DOFade(0f, 1f).SetEase(Ease.InQuad);
        logos.DOScale(finalSize, 1f).SetLoops(6, LoopType.Yoyo).OnComplete(() => whiteOut());
    }

    void whiteOut()
    {
        whitescreen.DOFade(255f, 2f).SetEase(Ease.InOutExpo).OnComplete(() => menu.sceneChanger(1));
    }
}
