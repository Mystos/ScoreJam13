using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerActionControls playerActionControls;

    public LayerMask collisionLayer;
    private Vector3 velocity;

    public float turnSmoothTime = 0.1f;
    public float moveSpeed = 4f;

    //Dash
    public float dashDistance = 8f;
    public float dashDashTime = 1f;
    public float dashStartTimer = 1f;
    public float dashCooldown = 1f;
    private float dashCooldownTimer = 0f;

    private float currentDashTimer;

    bool isDashing;
    bool isDashAvailable = true;


    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.IsGamePaused)
        {
            return;
        }
        // Mouse Orientation
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(playerActionControls.Player.MousePosition.ReadValue<Vector2>());

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // Read the movement value
        Vector2 movementInput = playerActionControls.Player.Move.ReadValue<Vector2>();
        //Debug.Log(movementInput);

        Vector3 movDir = new Vector3(movementInput.x, movementInput.y, 0f);

        if(movDir != Vector3.zero)
            Debug.DrawLine(this.transform.position, dashDistance * movDir, Color.red);

        if (!isDashing)
        {
            controller.Move(movDir.normalized * moveSpeed * Time.deltaTime);
        }

        // Dash
        if (playerActionControls.Player.Dash.triggered && movDir != Vector3.zero && isDashAvailable)
        {
            isDashing = true;
            currentDashTimer = dashStartTimer;

            isDashAvailable = false;
            dashCooldownTimer = dashCooldown;

            this.gameObject.GetComponentInChildren<DashTrail>().SetEnabled(true);

            //Debug.Log(movDir);
        }

        if (!isDashAvailable)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0)
            {
                isDashAvailable = true;
            }
        }



        if (isDashing)
        {
            
            float dashDistanceRaycast = dashDistance;
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, movDir, dashDistance, collisionLayer);
            if (hit)
            {
                dashDistanceRaycast = hit.distance;
            }

            Vector3 dashPosArrival = movDir.normalized * dashDistanceRaycast;

            controller.Move(dashPosArrival * dashDashTime);

            currentDashTimer -= Time.deltaTime;

            if(currentDashTimer <= 0)
            {
                isDashing = false;
                this.gameObject.GetComponentInChildren<DashTrail>().SetEnabled(false);

            }
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
