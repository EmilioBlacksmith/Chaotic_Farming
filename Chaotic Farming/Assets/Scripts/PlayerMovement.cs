using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Clamping Zone")]
    public Vector2 zoneOfAction;

    [Header("Movement")]
    public float speed;
    public PlayerInput playerInput;

    [Header("Looking")]
    public Camera mainCamera;
    private Vector2 vectorLooking;
    private Vector2 pointToLook;

    [Header("Farming")]
    public GameObject soilToPlant;
    public Transform Crosshair;

    [Header("Animations")]
    public Animator playerAnimator;
    public float horizontal,vertical;
    public float Moving;

    private Vector2 vectorMovement;
    private Rigidbody2D rgb;

    [Header("timerFootstep")]
    public float timerFootstep = .5f;
    public float timeToStep = .5f;

    void Start()
    {
        rgb = this.GetComponent<Rigidbody2D>();
    }
    
    public void OnMove(InputAction.CallbackContext value)
    {
        vectorMovement = value.ReadValue<Vector2>();

        if (value.started)
        {
            Moving = 1;
            horizontal = vectorMovement.x;
            vertical = vectorMovement.y;
        }
        else if(value.canceled)
        {
            Moving = 0;
        }
        
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        vectorLooking = value.ReadValue<Vector2>();
        vectorLooking.Normalize();
        pointToLook = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    void Update()
    {
        if (GameManager.instance.gameOver)
        {

        }
        else
        {
            rgb.velocity += vectorMovement * speed * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -zoneOfAction.x, zoneOfAction.x),
                Mathf.Clamp(transform.position.y, -zoneOfAction.y, zoneOfAction.y),
                0);

            Vector2 direction = pointToLook - (Vector2)transform.position; //direction from Center to Cursor
            Vector2 normalizedDirection = direction.normalized;
            normalizedDirection.x = Mathf.Round(normalizedDirection.x);
            normalizedDirection.y = Mathf.Round(normalizedDirection.y);

            Crosshair.position = new Vector3(Mathf.Round(transform.position.x),
                Mathf.Round(transform.position.y),
                Mathf.Round(transform.position.z)) + (Vector3)normalizedDirection;
        }

    }

    private void LateUpdate()
    {
        if (GameManager.instance.gameOver)
        {

        }
        else
        {
            playerAnimator.SetFloat("Horizontal", horizontal);
            playerAnimator.SetFloat("Vertical", vertical);
            playerAnimator.SetFloat("Speed", Moving);
        }
    }
}
