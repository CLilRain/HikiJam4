using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    Walk,
    Plane,
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public FirstPersonMovement playerMove;
    public FirstPersonLook playerLook;

    public Transform repsawnPoint;
    public int HP;

    float cooldownTimer;

    AbilityManager abiilties;
    PlayerMode state;

    const float bottomBound = -70f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        abiilties = AbilityManager.Instance;
    }

    void Update()
    {
        if (state == PlayerMode.Plane)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            abiilties.ShowGhostCloud();
            // preview ghost
        }
        if (Input.GetMouseButtonUp(0))
        {
            abiilties.SpawnCloud();
        }

        if (transform.position.y < bottomBound)
        {
            transform.position = repsawnPoint.position;
        }
    }

    public void EnterPlane(Plane plane)
    {
        playerMove.Disable();
        state = PlayerMode.Plane;
        //transform.position = plane.cockpit.position;
        //transform.parent = plane.cockpit.parent;

        //plane.PlayerEnters(this);
    }

    public void ExitPlane()
    {
        playerMove.Enable();
        state = PlayerMode.Walk;

        transform.parent = null;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

    }
}