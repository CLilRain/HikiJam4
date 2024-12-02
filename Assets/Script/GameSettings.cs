using System.Collections;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public float MaxHealth = 100;

    public static GameSettings Instance;

    public LayerMask collectableLayer;


    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
