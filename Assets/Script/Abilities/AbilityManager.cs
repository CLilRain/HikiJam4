using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance;

    public PlatformCloudPool cloudSpawner;
    public Transform ghostCloud;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ghostCloud.rotation = Quaternion.identity;
    }


    #region Cloud

    public void ShowGhostCloud()
    {
        ghostCloud.gameObject.SetActive(true);
    }

    public void HideGhostCloud()
    {
        ghostCloud.gameObject.SetActive(false);
    }

    public void SpawnCloud()
    {
        HideGhostCloud();
        cloudSpawner.Spawn(ghostCloud.position);
    }

    #endregion
}