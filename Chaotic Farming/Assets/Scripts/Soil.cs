using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public enum state
{
    available,
    canPlant,
    planted,
    toCrop
}

public class Soil : MonoBehaviour
{
    public state currentStage = state.available;

    public bool canInteract = true;

    [Header("Soil Sprites")]
    public Sprite soilToPlant;
    public Sprite soilWatered;
    public Sprite soilNormal;

    [Header("Sprite Renderers")]
    public SpriteRenderer plantHolder;
    public SpriteRenderer thisSprite;

    [Header("Timer")]
    public float timeToCrop = 60f;
    public float Timer = 60f;
    public float boostOfWater = 20f;
    public bool wateredSeed = false;

    [Header("Harvest")]
    public Plants harvest;
    public Sprite emptyHolder;

    [Header("Slider")]
    public Slider counterSlider;

    private void Start()
    {
        counterSlider.minValue = 0;
        counterSlider.maxValue = timeToCrop;
        counterSlider.value = timeToCrop;
    }

    public IEnumerator activeToPlant()
    {
        thisSprite.sprite = soilToPlant;
        currentStage = state.canPlant;

        yield return new WaitForSecondsRealtime(6f);

        if (currentStage == state.canPlant)
        {
            thisSprite.sprite = soilNormal;
            currentStage = state.available;
        }
    }

    public void plantedSoil(Plants seedToHarvest)
    {
        currentStage = state.planted;
        plantHolder.sprite = seedToHarvest.seeds;
        harvest = seedToHarvest;
    }

    public void Watered()
    {
        if(currentStage == state.planted || currentStage == state.canPlant)
        {
            //water soil
            thisSprite.sprite = soilWatered;
            wateredSeed = true;
        }
    }

    public void Harvest()
    {
        if(currentStage == state.toCrop)
        {
            wateredSeed = false;
            thisSprite.sprite = soilNormal;
            plantHolder.sprite = emptyHolder;
            harvest = null;
            wateredSeed = false;
            canInteract = true;

            currentStage = state.available;
        }
    }

    public void Update()
    {
        //check time before harvest
        if(currentStage == state.planted && wateredSeed)
        {
            //timer with boost of water
            if(Timer > 0)
            {
                Timer -= Time.deltaTime * boostOfWater;
            }
        }
        else if(currentStage == state.planted && !wateredSeed)
        {
            //timer slower without boost
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
        }

        if(Timer <= 0)
        {
            //can harvest
            canInteract = true;
            Timer = timeToCrop;
            plantHolder.sprite = harvest.harvest;
            currentStage = state.toCrop;
        }

        if(currentStage != state.planted)
        {
            counterSlider.gameObject.SetActive(false);
        }
        else
        {
            counterSlider.gameObject.SetActive(true);
            counterSlider.value = Timer;
        }
        
    }

}
