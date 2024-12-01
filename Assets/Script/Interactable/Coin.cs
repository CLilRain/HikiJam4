using UnityEngine;

namespace Collectable
{
    public class Coin : MonoBehaviour, ICollectable
    {
        public CollectableTypes GetCollectableType() => CollectableTypes.Coin;

        public void OnCollect()
        {
            Destroy(gameObject);
        }
    }
}