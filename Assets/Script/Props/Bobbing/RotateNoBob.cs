using System.Collections;
using UnityEngine;

public class RotateNoBob : MonoBehaviour
{
    Transform cachedTransform;
    Vector3 startPos;

    void Awake()
    {
        cachedTransform = transform;
        startPos = cachedTransform.position;
    }

    void Update()
    {
        cachedTransform.rotation = GlobalBobbingPosition.Rotation;
    }
}
