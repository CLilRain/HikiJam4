using System.Collections;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public float MaxHealth = 100;

    public static GameSettings Instance;

    public LayerMask interactableLayer;
    public LayerMask playerWeaponLayer;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
