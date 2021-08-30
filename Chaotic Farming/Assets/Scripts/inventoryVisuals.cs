using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryVisuals : MonoBehaviour
{
    [SerializeField] private GameObject[] inventorySelection;

    public void changeSelected(int cellSelected)
    {
        for(int i = 0; i < inventorySelection.Length; i++)
        {
            if(i == cellSelected)
            {
                inventorySelection[i].SetActive(true);
            }
            else
            {
                inventorySelection[i].SetActive(false);
            }
        }
    }
}
