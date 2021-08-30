using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBox : MonoBehaviour
{
    public List<Plants> ordersList;
    public List<GameObject> toDeliverUIOrder;
    [Space]
    public Transform deliveryUIHolder;
    public GameObject newOrderUICell;

    [Header("Signs")]
    public GameObject sign;
    public Transform signParent;

    public void newOrderToList(Plants plantForOrder)
    {
        ordersList.Add(plantForOrder);
        GameObject i = Instantiate(newOrderUICell, deliveryUIHolder) as GameObject;
        toDeliverUIOrder.Add(i);

        Order orderObj = i.GetComponent<Order>(); 
        orderObj.imageHolder.sprite = plantForOrder.harvest;
        orderObj.deliveryBox = this;
        orderObj.orderToDelete = plantForOrder;

        GameObject signal = Instantiate(sign, signParent) as GameObject;
        signal.GetComponent<fadeSign>().textToDisplay.text = "new order";
    }

    public void Delivered(Plants plantsForOrder, int numObjs)
    {
        int num = numObjs;
        for(int i = 0; i < ordersList.Count; i++)
        {
            if(ordersList[i].plantName == plantsForOrder.plantName)
            {
                ordersList.RemoveAt(i);
                Destroy(toDeliverUIOrder[i],.01f);
                toDeliverUIOrder.RemoveAt(i);
                GameManager.instance.SB_OrderDelivered();
                num--;
                if (num <= 0)
                {
                    Debug.Log("Order Completed");

                    GameObject signal = Instantiate(sign, signParent) as GameObject;
                    signal.GetComponent<fadeSign>().textToDisplay.text = "order Completed";
                    break;                    
                }
            }
        }
    }

    public void orderNotCompleted(Plants plantsForOrder)
    {
        for (int i = 0; i < ordersList.Count; i++)
        {
            if (ordersList[i].plantName == plantsForOrder.plantName)
            {
                ordersList.RemoveAt(i);
                Destroy(toDeliverUIOrder[i]);
                toDeliverUIOrder.RemoveAt(i);
                Debug.Log("lost order");

                GameObject signal = Instantiate(sign, signParent) as GameObject;
                signal.GetComponent<fadeSign>().textToDisplay.text = "order lost";
                GameManager.instance.SB_OrderCanceled();
                MusicScript.instance.playError(1.25f);
                break;
            }
        }
    }

    public void completeAllOrders()
    {
        for (int i = 0; i < ordersList.Count; i++)
        {
            GameManager.instance.SB_OrderDelivered();
            Destroy(toDeliverUIOrder[i], .04f);
            ordersList.RemoveAt(i);
            toDeliverUIOrder.RemoveAt(i);

            GameObject signal = Instantiate(sign, signParent) as GameObject;
            signal.GetComponent<fadeSign>().textToDisplay.text = "¡MARTIN MALVERDEEEEE!";
        }
    }
}
