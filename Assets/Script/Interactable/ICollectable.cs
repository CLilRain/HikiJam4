using System.Collections;
using UnityEngine;

namespace Collectable
{
    public enum CollectableTypes
    {
        Coin,
    }


    public interface ICollectable
    {
        public CollectableTypes GetCollectableType();

        public void OnCollect();
    }
}