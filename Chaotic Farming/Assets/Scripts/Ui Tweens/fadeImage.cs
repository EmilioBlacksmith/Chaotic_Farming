using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class fadeImage : MonoBehaviour
{
    public Image whiteScreen;
    public float timeToFade;

    private void OnEnable()
    {
        whiteScreen = this.GetComponent<Image>();
        whiteScreen.DOFade(0f, timeToFade).OnComplete(() => Destroy(this.gameObject));
    }
}
