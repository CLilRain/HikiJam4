using UnityEngine;

public class FloatingCloud : MonoBehaviour
{
    Vector3 moveDirection = new Vector3(0f, 0f, 1f);
    float bound = 300f;
    float wrapAroundOffset = 600f;

    private Transform cachedTransform;

    void Start()
    {
        cachedTransform = transform;
    }

    void FixedUpdate()
    {
        cachedTransform.Translate(moveDirection * Time.fixedDeltaTime);

        if (cachedTransform.position.z > bound)
        {
            var pos = cachedTransform.position;
            pos.z -= wrapAroundOffset;
            cachedTransform .position = pos;
        }
    }
}
