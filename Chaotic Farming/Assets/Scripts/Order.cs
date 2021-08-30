using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public Image imageHolder;
    public Slider sliderTimer;
    public Color Nice, Warning, Danger;
    public Image sliderColor;
    public float timeToCancelOrder = 20f;
    public float Timer = 20f;
    public float dangerZone, warningZone;

    public DeliveryBox deliveryBox;
    public Plants orderToDelete;

    private void Start()
    {
        
        sliderTimer.maxValue = timeToCancelOrder;
        sliderTimer.minValue = 0;
        sliderTimer.value = Timer;
    }

    public void Update()
    {
        if(Timer > 0 && !GameManager.instance.gameOver)
        {
            Timer -= Time.deltaTime;
        }
        else if(Timer <= 0 && !GameManager.instance.gameOver)
        {
            // order deleted
            deliveryBox.orderNotCompleted(orderToDelete);
        }
        else
        {
            //lost
        }

        sliderTimer.value = Timer;
        checkColor();
    }

     private void checkColor()
    {
        if(Timer > warningZone)
        {
            sliderColor.color = Nice;
        }else if(Timer > dangerZone && Timer <= warningZone)
        {
            //keep color yellow
            sliderColor.color = Warning;
        }
        else
        {
            //keep color red
            sliderColor.color = Danger;
        }
    }
}
