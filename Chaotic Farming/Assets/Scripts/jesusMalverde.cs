using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jesusMalverde : MonoBehaviour
{
    public bool canActivate = false;
    public taskSystems ordersSystem;
    public Sprite normal, activated;
    public SpriteRenderer rock;

    [Header("Timer of Jesus")]
    public float timeToJesus = 150f;
    public float Timer = 150f;
    public float multiplier = 1f;

    [Header("Signs")]
    public GameObject sign;
    public Transform signParent;

    public void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else if(Timer <= 0 && !canActivate)
        {
            rock.sprite = activated;
            canActivate = true;
            MusicScript.instance.playJesusReady(1.25f);
            GameObject signal = Instantiate(sign, signParent) as GameObject;
            signal.GetComponent<fadeSign>().textToDisplay.text = "JESUS MALVERDE POWER UP IS READY";
        }
    }

    public void jesusMalverdePowerUP()
    {
        multiplier++;

        timeToJesus = timeToJesus * multiplier;

        ordersSystem.jesusMalverde();
        ordersSystem.jesusMalverde();
        canActivate = false;
        rock.sprite = normal;
        Timer = timeToJesus;
        Debug.Log("The power of the god of Crops!");

        GameObject signal = Instantiate(sign, signParent) as GameObject;
        signal.GetComponent<fadeSign>().textToDisplay.text = "The power of the god of Crops!";
    }
}
