using UnityEngine;

public static class LayerMaskContains
{
    public static bool Contains(this LayerMask layerMask, GameObject gameObject)
    {
        return (1 << gameObject.layer & layerMask) != 0;
    }
}
