using System;
using UnityEngine;

namespace LoxiGames.Utility
{
    public class TriggerHandler : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerStay;
        public event Action<Collider> TriggerExit;
        public event Action<Collision> CollisionEnter;
        public event Action<Collision> CollisionStay;
        public event Action<Collision> CollisionExit;

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            TriggerStay?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit?.Invoke(other);
        }

        private void OnCollisionEnter(Collision other)
        {
            CollisionEnter?.Invoke(other);
        }

        private void OnCollisionStay(Collision other)
        {
            CollisionStay?.Invoke(other);
        }

        private void OnCollisionExit(Collision other)
        {
            CollisionExit?.Invoke(other);
        }
    }
}