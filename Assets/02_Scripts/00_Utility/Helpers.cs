using UnityEngine;

namespace LoxiGames.Utility
{
    public static class Helpers
    {
        public static void CopyTransformData(Transform sourceTransform, Transform destinationTransform,
            bool includeScale = false)
        {
            if (sourceTransform.childCount != destinationTransform.childCount)
            {
                Debug.LogError("Source and Destination do not have same child count!" +
                               " Source: " + sourceTransform.childCount +
                               " Destination:" + destinationTransform.childCount);

                return;
            }

            for (var i = 0; i < sourceTransform.childCount; i++)
            {
                var source = sourceTransform.GetChild(i);
                var destination = destinationTransform.GetChild(i);

                destination.position = source.position;
                destination.rotation = source.rotation;
                if (includeScale) destination.localScale = source.localScale;

                CopyTransformData(source, destination);
            }
        }

        public static void CopyLocalTransformData(Transform sourceTransform, Transform destinationTransform,
            bool includeScale = false)
        {
            if (sourceTransform.childCount != destinationTransform.childCount)
            {
                Debug.LogError("Source and Destination do not have same child count!" +
                               " Source: " + sourceTransform.childCount +
                               " Destination:" + destinationTransform.childCount);

                return;
            }

            for (var i = 0; i < sourceTransform.childCount; i++)
            {
                var source = sourceTransform.GetChild(i);
                var destination = destinationTransform.GetChild(i);

                destination.localPosition = source.localPosition;
                destination.localRotation = source.localRotation;
                if (includeScale) destination.localScale = source.localScale;

                CopyLocalTransformData(source, destination, includeScale);
            }
        }

        public static void ResetRigidbodies(Transform tr, bool resetAngularVelocity = true)
        {
            for (var i = 0; i < tr.childCount; i++)
            {
                var child = tr.GetChild(i);

                var rb = tr.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.velocity = Vector3.zero;
                    if (resetAngularVelocity) rb.angularVelocity = Vector3.zero;
                }

                ResetRigidbodies(child);
            }
        }

        public static void SetRigidbodiesMaxSpeed(Transform tr, float maxSpeed, float maxAngularSpeed)
        {
            for (var i = 0; i < tr.childCount; i++)
            {
                var child = tr.GetChild(i);

                var rb = tr.GetComponent<Rigidbody>();
                if (rb)
                {
                    if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
                    {
                        rb.velocity = rb.velocity.normalized * maxSpeed;
                    }

                    if (rb.angularVelocity.sqrMagnitude > maxAngularSpeed * maxAngularSpeed)
                    {
                        rb.angularVelocity = rb.angularVelocity.normalized * maxAngularSpeed;
                    }
                }

                SetRigidbodiesMaxSpeed(child, maxSpeed, maxAngularSpeed);
            }
        }

        public static void SetRigidbodiesVelocity(Transform tr, Vector3 vel)
        {
            for (var i = 0; i < tr.childCount; i++)
            {
                var child = tr.GetChild(i);

                var rb = tr.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.velocity = vel;
                }

                SetRigidbodiesVelocity(child, vel);
            }
        }
    }
}