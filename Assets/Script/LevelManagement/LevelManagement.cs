using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{



    public GameObject[] levelPrefabs;

    GameObject spawnedLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            LoadLevel(0);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            LoadLevel(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            LoadLevel(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            LoadLevel(3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            LoadLevel(4);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            LoadLevel(5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            LoadLevel(6);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            LoadLevel(7);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            LoadLevel(8);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            LoadLevel(9);

    }

    public void LoadLevel(int index)
    {
        if (levelPrefabs.Length > index)
        {
            DespawnExisting();
            spawnedLevel = Instantiate(levelPrefabs[index], transform);
        }
    }

    private void DespawnExisting()
    {
        if (spawnedLevel != null)
        {
            Destroy(spawnedLevel);
        }
    }
}
