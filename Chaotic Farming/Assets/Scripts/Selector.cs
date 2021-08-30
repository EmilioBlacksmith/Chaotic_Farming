using UnityEngine;

public enum stateOfSelector
{
    selectingSoil,
    selectingSeedBox,
    selectingDelivery,
    selectingJesus,
    selectingNothing
}

public class Selector : MonoBehaviour
{
    public stateOfSelector currentState;
    [SerializeField] private Tools player;
    [SerializeField] private Soil soil;

    [Header("Cursor Changes")]
    public Texture2D mouseNormal;
    public Texture2D mouseSelecting;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soil"))
        {
            soil = other.GetComponent<Soil>();
            player.selectedSoil = soil;
            currentState = stateOfSelector.selectingSoil;
            Cursor.SetCursor(mouseSelecting, Vector2.zero, CursorMode.Auto);
        }else if(other.CompareTag("Seed Box"))
        {
            player.possibleCurrentObj = other.GetComponent<BoxSeeds>().seedForHarvest;
            currentState = stateOfSelector.selectingSeedBox;
            Cursor.SetCursor(mouseSelecting, Vector2.zero, CursorMode.Auto);
        }
        else if (other.CompareTag("Delivery"))
        {
            currentState = stateOfSelector.selectingDelivery;
            Cursor.SetCursor(mouseSelecting, Vector2.zero, CursorMode.Auto);
        }
        else if (other.CompareTag("Jesus"))
        {
            currentState = stateOfSelector.selectingJesus;
            Cursor.SetCursor(mouseSelecting, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Soil"))
        {
            player.selectedSoil = null;
            currentState = stateOfSelector.selectingNothing;
            Cursor.SetCursor(mouseNormal, Vector2.zero, CursorMode.Auto);
        }
        else if (other.CompareTag("Seed Box"))
        {
            player.possibleCurrentObj = null;
            currentState = stateOfSelector.selectingNothing;
            Cursor.SetCursor(mouseNormal, Vector2.zero, CursorMode.Auto);
        }
        else if (other.CompareTag("Delivery"))
        {
            currentState = stateOfSelector.selectingNothing;
            Cursor.SetCursor(mouseNormal, Vector2.zero, CursorMode.Auto);
        }
        else if (other.CompareTag("Jesus"))
        {
            currentState = stateOfSelector.selectingNothing;
            Cursor.SetCursor(mouseNormal, Vector2.zero, CursorMode.Auto);
        }
    }
}
