using UnityEngine;
using DG.Tweening;
using TMPro;

public class fadeSign : MonoBehaviour
{
    public float timeToFade = 1.5f;
    public Vector2 toMovement;
    public RectTransform rect;
    public TextMeshProUGUI textToDisplay;

    private void OnEnable()
    {
        textToDisplay = this.GetComponent<TextMeshProUGUI>();
        rect = this.GetComponent<RectTransform>();
        textToDisplay.DOFade(0, timeToFade);
        rect.DOAnchorPos(toMovement,timeToFade).OnComplete(() => Destroy(this.gameObject));
    }

}
