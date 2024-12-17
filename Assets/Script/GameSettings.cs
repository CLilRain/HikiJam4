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

public static class InteractionExtensionMethods
{
    public static bool IsOnInteractableLayer(this Collider other)
    {
        return GameSettings.Instance.interactableLayer.Contains(other.gameObject);
        //return (1 << other.gameObject.layer) == GameSettings.Instance.collectableLayer;
    }
}