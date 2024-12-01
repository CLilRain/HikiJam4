using UnityEngine;
using System.Collections;

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //

public class Plane : MonoBehaviour
{
    [Header("References")]
    public AirPlaneGuns guns;
    public Transform cockpit;
    public Transform respawnPoint;

    public GameObject prop;
    public GameObject propBlured;

    [Header("State")]
    public bool engineOn;

    [Header("Settings")]
    public float frontSpeed = 20f;
    public float SteerSpeed = 5f;

    PlayerController player;
    Rigidbody rb;

    Vector3 vel;

    private void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (engineOn && player != null)
        {
            MovementControl();

            propBlured.transform.Rotate(1000 * Time.fixedDeltaTime, 0, 0);
        }
    }

    void MovementControl()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.velocity = transform.forward * frontSpeed * Time.fixedDeltaTime;
        rb.rotation = rb.rotation
            * Quaternion.AngleAxis(y * SteerSpeed * Time.fixedDeltaTime, transform.right)
            * Quaternion.AngleAxis(y * SteerSpeed * Time.fixedDeltaTime, transform.right);
    }


    public void PlayerEnters(PlayerController player)
    {
        this.player = player;
        engineOn = true;
    }

    public void PlayeExits()
    {
        engineOn = false;

    }




    public void Respawn()
    {
        transform.position = respawnPoint.position;
    }

    void PropellerActivate()
    {
        prop.SetActive(false);
        propBlured.SetActive(true);
    }

    void PropellerDisable()
    {
        prop.SetActive(true);
        propBlured.SetActive(false);
    }
}

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //