using UnityEngine;

namespace Collectable
{
    public class CollectableEssence : MonoBehaviour, ICollectable
    {
        public CollectableTypes GetCollectableType() => CollectableTypes.Coin;

        public void OnCollect(PlayerController player)
        {
            player.CollectEssence();
            Destroy(gameObject);
        }
    }
}