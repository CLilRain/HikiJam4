using System.Collections;
using UnityEngine;

public class Bobber : MonoBehaviour
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
        cachedTransform.position = startPos + GlobalBobbingPosition.Position;
        cachedTransform.rotation = GlobalBobbingPosition.Rotation;
    }
}
