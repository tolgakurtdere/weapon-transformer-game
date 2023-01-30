using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LoxiGames.Utility
{
    public class RagdollSwitcher : MonoBehaviour
    {
        [SerializeField, Required] private GameObject animatedBody;
        [SerializeField, Required] private GameObject ragdollBody;
        [SerializeField] private Vector3Int forceMin;
        [SerializeField] private Vector3Int forceMax;

        private Collider[] _colliders;

        private void Awake()
        {
            _colliders = GetComponents<Collider>();
        }

        [Button, DisableInEditorMode]
        public void TurnOnRagdoll()
        {
            Helpers.CopyTransformData(animatedBody.transform, ragdollBody.transform);
            animatedBody.SetActive(false);
            ragdollBody.SetActive(true);

            foreach (var col in _colliders)
            {
                col.enabled = false;
            }

            AddForce(ragdollBody.transform);
            StartCoroutine(SetActiveFalse(2f));
        }

        private void AddForce(Transform t)
        {
            var force = Vector3Int.zero;
            force.x = Random.Range(forceMin.x, forceMax.x);
            force.y = Random.Range(forceMin.y, forceMax.y);
            force.z = Random.Range(forceMin.z, forceMax.z);

            for (var i = 0; i < t.childCount; i++)
            {
                var child = t.GetChild(i);

                var rb = t.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForce(force);
                }

                AddForce(child);
            }
        }

        private IEnumerator SetActiveFalse(float duration)
        {
            yield return new WaitForSeconds(duration);
            ragdollBody.SetActive(false);
        }
    }
}