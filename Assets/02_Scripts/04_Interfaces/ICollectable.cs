using UnityEngine;

namespace WeaponTransformer
{
    public interface ICollectable
    {
        Collider Collider { get; }
        void Collect();
    }
}