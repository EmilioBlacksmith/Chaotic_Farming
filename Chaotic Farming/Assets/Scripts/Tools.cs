using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum tools
{
    Hoe,
    WateringCan,
    /*Scythe,*/
    CurrentObj
}

public class Tools : MonoBehaviour
{
    public tools currentTool;
    public Soil selectedSoil;
    public Animator animator;

    [Header("Inventory & Inventory Changer")]
    public int toolNumber = 3;
    public int currentToolNumber = 0;
    public Plants possibleCurrentObj;
    public Plants currentObject;
    public Selector crosshair;
    public Transform objectHolder;
    public Image inventoryHolder;
    public bool currentObjCropped;

    [Header("Scroller and Visuals")]
    public inventoryVisuals UIInventory;
    public bool canScroll = true;
    public Sprite emptyInventory;

    [Header("Pile of Objs")]
    public string currentCropName;
    public int numObjsOnInventory = 0;

    [Header("Delivery")]
    public DeliveryBox boxOfDelivery;

    [Header("Jesus Malverde")]
    public jesusMalverde jesusStatue;

    [Header("Signs")]
    public GameObject sign;
    public Transform signParent;

    public void OnClick(InputAction.CallbackContext value)
    {
        if (GameManager.instance.gameOver)
        {

        }
        else
        {
            if (value.started)
            {
                animator.SetBool("Action", true);
                #region harvest and seed selection
                if (crosshair.currentState == stateOfSelector.selectingSoil && selectedSoil != null)
                {

                    switch (currentTool)
                    {
                        case tools.CurrentObj:
                            if (selectedSoil.canInteract && selectedSoil.currentStage == state.canPlant)
                            {
                                if (currentObject != null && !currentObjCropped/*&& !currentObject.Cropped*/)
                                {
                                    selectedSoil.plantedSoil(currentObject);
                                    MusicScript.instance.playGrass(.9f);
                                }
                                else
                                {
                                    Debug.Log("cannot plant");
                                    GameObject signal = Instantiate(sign, signParent) as GameObject;
                                    signal.GetComponent<fadeSign>().textToDisplay.text = "cannot plant this obj";
                                    MusicScript.instance.playError(.9f);
                                }
                            }
                            else if (selectedSoil.canInteract && selectedSoil.currentStage == state.toCrop)
                            {
                                if (numObjsOnInventory > 0)
                                {
                                    if (selectedSoil.harvest.plantName == currentCropName)
                                    {
                                        // ---------------- I M P O R T A N T --------------------------
                                        //MAKE DE PILE OF OBJECTS ON THE INVENTORY
                                        /*numObjsOnInventory++;

                                        currentObject = selectedSoil.harvest;
                                        inventoryHolder.sprite = currentObject.harvest;
                                        currentObjCropped = true;
                                        Debug.Log("you have " + numObjsOnInventory + " of the " + currentCropName);

                                        selectedSoil.Harvest();*/
                                        Debug.Log("You cannot pile more than one object");
                                        GameObject signal = Instantiate(sign, signParent) as GameObject;
                                        signal.GetComponent<fadeSign>().textToDisplay.text = "you cannot pile more than one obj";
                                        MusicScript.instance.playError(.75f);
                                    }
                                    else
                                    {
                                        Debug.Log("You cannot pile more than one type of object");
                                        GameObject signal = Instantiate(sign, signParent) as GameObject;
                                        signal.GetComponent<fadeSign>().textToDisplay.text = "you cannot pile more than one type of obj";
                                        MusicScript.instance.playError(.75f);
                                    }
                                }
                                else
                                {

                                    currentObject = selectedSoil.harvest;
                                    inventoryHolder.sprite = currentObject.harvest;
                                    currentObjCropped = true;

                                    currentCropName = selectedSoil.harvest.plantName;
                                    numObjsOnInventory = 1;

                                    selectedSoil.Harvest();
                                    MusicScript.instance.playGrass(.9f);
                                }
                            }
                            break;
                        case tools.Hoe:
                            if (selectedSoil.canInteract && selectedSoil.currentStage == state.available)
                            {
                                selectedSoil.StartCoroutine("activeToPlant");
                                MusicScript.instance.playGrass(.9f);
                            }
                            else if (selectedSoil.canInteract && selectedSoil.currentStage == state.toCrop)
                            {
                                if (numObjsOnInventory > 0)
                                {
                                    if (selectedSoil.harvest.plantName == currentCropName)
                                    {
                                        // ---------------- I M P O R T A N T --------------------------
                                        //MAKE DE PILE OF OBJECTS ON THE INVENTORY
                                        /*numObjsOnInventory++;

                                        currentObject = selectedSoil.harvest;
                                        inventoryHolder.sprite = currentObject.harvest;
                                        currentObjCropped = true;
                                        Debug.Log("you have " + numObjsOnInventory + " of the " + currentCropName);

                                        selectedSoil.Harvest();*/
                                        Debug.Log("You cannot pile more than one object");
                                        GameObject signal = Instantiate(sign, signParent) as GameObject;
                                        signal.GetComponent<fadeSign>().textToDisplay.text = "you cannot pile more than one obj";
                                        MusicScript.instance.playError(.75f);
                                    }
                                    else
                                    {
                                        Debug.Log("You cannot pile more than one type of object");
                                        GameObject signal = Instantiate(sign, signParent) as GameObject;
                                        signal.GetComponent<fadeSign>().textToDisplay.text = "you cannot pile more than one type of obj";
                                        MusicScript.instance.playError(.75f);
                                    }
                                }
                                else
                                {

                                    currentObject = selectedSoil.harvest;
                                    inventoryHolder.sprite = currentObject.harvest;
                                    currentObjCropped = true;

                                    currentCropName = selectedSoil.harvest.plantName;
                                    numObjsOnInventory = 1;

                                    selectedSoil.Harvest();
                                    MusicScript.instance.playGrass(.9f);
                                }
                            }
                            break;
                        /*case tools.Scythe:
                            if(selectedSoil.canInteract && selectedSoil.currentStage == state.toCrop)
                            {
                                selectedSoil.Harvest();
                                currentObject = selectedSoil.harvest;
                                inventoryHolder.sprite = currentObject.harvest;
                            }
                            break;
                        */
                        case tools.WateringCan:
                            if (selectedSoil.canInteract && selectedSoil.currentStage == state.canPlant)
                            {
                                selectedSoil.Watered();
                                MusicScript.instance.playWatered(.85f);
                            }
                            else if (selectedSoil.canInteract && selectedSoil.currentStage == state.planted)
                            {
                                selectedSoil.Watered();
                                MusicScript.instance.playWatered(.85f);
                            }
                            else if (selectedSoil.canInteract && selectedSoil.currentStage == state.toCrop)
                            {
                                if (numObjsOnInventory > 0)
                                {
                                    if (selectedSoil.harvest.plantName == currentCropName)
                                    {
                                        // ---------------- I M P O R T A N T --------------------------
                                        //MAKE DE PILE OF OBJECTS ON THE INVENTORY
                                        /*numObjsOnInventory++;

                                        currentObject = selectedSoil.harvest;
                                        inventoryHolder.sprite = currentObject.harvest;
                                        currentObjCropped = true;
                                        Debug.Log("you have " + numObjsOnInventory + " of the " + currentCropName);

                                        selectedSoil.Harvest();*/
                                        Debug.Log("You cannot pile more than one object");
                                        GameObject signal = Instantiate(sign, signParent) as GameObject;
                                        signal.GetComponent<fadeSign>().textToDisplay.text = "you cannot pile more than one obj";
                                        MusicScript.instance.playError(.75f);
                                    }
                                    else
                                    {
                                        Debug.Log("You cannot pile more than one type of object");
                                        GameObject signal = Instantiate(sign, signParent) as GameObject;
                                        signal.GetComponent<fadeSign>().textToDisplay.text = "you cannot pile more than one type of obj";
                                        MusicScript.instance.playError(.75f);
                                    }
                                }
                                else
                                {

                                    currentObject = selectedSoil.harvest;
                                    inventoryHolder.sprite = currentObject.harvest;
                                    currentObjCropped = true;

                                    currentCropName = selectedSoil.harvest.plantName;
                                    numObjsOnInventory = 1;

                                    selectedSoil.Harvest();
                                    MusicScript.instance.playGrass(.9f);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if (crosshair.currentState == stateOfSelector.selectingSeedBox && possibleCurrentObj != null)
                {
                    if (currentObjCropped)
                    {
                        //you will delete the current crop out of your inventory
                        Debug.Log("You will delete the object you're holding...");
                        GameObject signal = Instantiate(sign, signParent) as GameObject;
                        signal.GetComponent<fadeSign>().textToDisplay.text = "You will delete the object you're holding...";
                        MusicScript.instance.playError(.75f);
                    }
                    else
                    {
                        currentObject = possibleCurrentObj;
                        inventoryHolder.sprite = currentObject.seeds;
                        currentObjCropped = false;
                        numObjsOnInventory = 0;
                        MusicScript.instance.playGrass(.9f);
                    }
                }
                #endregion

                #region delivery selection
                else if (crosshair.currentState == stateOfSelector.selectingDelivery && currentObjCropped)
                {
                    //current selected is the delivery box, and the obj we have in hand is the cropped plant
                    boxOfDelivery.Delivered(currentObject, numObjsOnInventory);
                    currentObjCropped = false;
                    numObjsOnInventory = 0;
                    inventoryHolder.sprite = emptyInventory;
                    MusicScript.instance.playNice(.9f);
                }
                else if (crosshair.currentState == stateOfSelector.selectingDelivery && !currentObjCropped)
                {
                    //have a seed on the hands
                    Debug.Log("Can't Deliver, this is a seed");
                    GameObject signal = Instantiate(sign, signParent) as GameObject;
                    signal.GetComponent<fadeSign>().textToDisplay.text = "Can't Deliver, this is a seed";
                    MusicScript.instance.playError(.75f);
                }
                #endregion
                else if(crosshair.currentState == stateOfSelector.selectingJesus && jesusStatue.canActivate)
                {
                    jesusStatue.jesusMalverdePowerUP();
                    MusicScript.instance.playJesusMalverde(1f);
                }
                else
                {
                    //do nothing
                    MusicScript.instance.playGrass(.9f);
                }
            }
            else if (value.canceled || value.performed)
            {
                animator.SetBool("Action", false);
            }
        }
        
    }

    public void onToolChange(string toolToChange)
    {
        if (!GameManager.instance.gameOver)
        {
            switch (toolToChange)
            {
                case "1":
                    currentTool = tools.Hoe;
                    UIInventory.changeSelected(0);
                    MusicScript.instance.playPop(.75f);
                    break;
                case "2":
                    currentTool = tools.WateringCan;
                    UIInventory.changeSelected(1);
                    MusicScript.instance.playPop(.75f);
                    break;/*
            case "3":
                currentTool = tools.Scythe;
                UIInventory.changeSelected(2);
                break;*/
                case "3":
                    currentTool = tools.CurrentObj;
                    UIInventory.changeSelected(2);
                    MusicScript.instance.playPop(.75f);
                    break;
            }
        }
    }

    public void onToolChangeShoulders(InputAction.CallbackContext value)
    {
        if (!GameManager.instance.gameOver)
        {
            if (value.performed)
            {
                float scroller = value.ReadValue<float>();
                if (scroller >= 0.01f && canScroll)
                {

                    currentToolNumber++;
                    if (currentToolNumber > toolNumber)
                    {
                        currentToolNumber = 0;
                    }

                    switch (currentToolNumber)
                    {
                        case 0:
                            currentTool = tools.Hoe;
                            UIInventory.changeSelected(0);
                            MusicScript.instance.playPop(.75f);
                            break;
                        case 1:
                            currentTool = tools.WateringCan;
                            UIInventory.changeSelected(1);
                            MusicScript.instance.playPop(.75f);
                            break;/*
                    case 2:
                        currentTool = tools.Scythe;
                        UIInventory.changeSelected(2);
                        break;*/
                        case 2:
                            currentTool = tools.CurrentObj;
                            UIInventory.changeSelected(2);
                            MusicScript.instance.playPop(.75f);
                            break;
                    }
                    canScroll = false;
                }
                else if (scroller <= -0.01f && canScroll)
                {
                    currentToolNumber--;
                    if (currentToolNumber < 0)
                    {
                        currentToolNumber = 3;
                    }

                    switch (currentToolNumber)
                    {
                        case 0:
                            currentTool = tools.Hoe;
                            UIInventory.changeSelected(0);
                            MusicScript.instance.playPop(.75f);
                            break;
                        case 1:
                            currentTool = tools.WateringCan;
                            UIInventory.changeSelected(1);
                            MusicScript.instance.playPop(.75f);
                            break;/*
                    case 2:
                        currentTool = tools.Scythe;
                        UIInventory.changeSelected(2);
                        break;*/
                        case 2:
                            currentTool = tools.CurrentObj;
                            UIInventory.changeSelected(2);
                            MusicScript.instance.playPop(.75f);
                            break;
                    }
                    canScroll = false;
                }
                else
                {
                    canScroll = true;
                }
            }
        }
            
    }
}
