using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taskSystems : MonoBehaviour
{
    public Plants[] possibleOrders;
    public DeliveryBox deliveryBox;
    private int maxRandom = 400;
    private int multiplier = 100;

    [Header("Timer to Order.")]
    public float timeToNewOrder = 150f;
    public float Timer = 150f;
    public float multiplierToTimer = 10f;

    [Header("Global Timer && exponential changer")]
    public float exponencialChange = 0f;
    public float change1, change2, change3, change4, change5;
    public AudioSource MusicListener;

    public void Update()
    {
        if (GameManager.instance.gameOver)
        {

        }
        else
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime * (randomNum() * multiplierToTimer);
            }
            else
            {
                Timer = timeToNewOrder + (randomNum() * (multiplierToTimer * 2));
                newOrder();
            }

            if (exponencialChange >= 0 && exponencialChange < change1)
            {
                exponencialChange += Time.deltaTime;
                timeToNewOrder = 500;
                MusicListener.pitch = .9f;
            }
            if (exponencialChange >= change1 && exponencialChange < change2)
            {
                exponencialChange += Time.deltaTime;
                timeToNewOrder = 350;
                MusicListener.pitch = .95f;
            }
            else if (exponencialChange >= change2 && exponencialChange < change3)
            {
                exponencialChange += Time.deltaTime;
                timeToNewOrder = 250;
                MusicListener.pitch = 1f;
            }
            else if (exponencialChange >= change3 && exponencialChange < change4)
            {
                exponencialChange += Time.deltaTime;
                timeToNewOrder = 175;
                MusicListener.pitch = 1.05f;
            }
            else if (exponencialChange >= change4 && exponencialChange < change5)
            {
                exponencialChange += Time.deltaTime;
                timeToNewOrder = 125;
                MusicListener.pitch = 1.1f;
            }
            else if (exponencialChange >= change5)
            {
                timeToNewOrder = 100;
                MusicListener.pitch = 1.15f;
            }
        }
    }

    public void jesusMalverde()
    {
        //power up restarts exponencial breath and all orders made
        exponencialChange = 0;
        deliveryBox.completeAllOrders();
    }

    public void newOrder()
    {
        deliveryBox.newOrderToList(possibleOrders[randomNum()]);
        MusicScript.instance.playNewOrder(1f);
    }

    public int randomNum()
    {
        int random = Random.Range(0, maxRandom);
        int ran = random / multiplier;
        ran = Mathf.RoundToInt(ran);
        return ran;
    }
}
