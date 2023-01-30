using UnityEngine;

namespace LoxiGames.Utility.Stack
{
    public abstract class Stackable : MonoBehaviour
    {
        public MyStack ownerStack;
        public Stackable next;
        public Stackable prev;
        public float offsetOverride;

        private Vector3 _positionVelocity;
        private Vector3 _scaleVelocity;

        public void FollowPosition(Stackable element)
        {
            if (!ownerStack.followPosition) return;
            if (!element) return;

            var tr = transform;
            var pos = tr.position;
            var targetPos = element.transform.position;
            var newPos = ownerStack.smoothTime < 0.3f
                ? Vector3.SmoothDamp(pos, targetPos, ref _positionVelocity, ownerStack.smoothTime, ownerStack.maxSpeed)
                : Vector3.Lerp(pos, targetPos, ownerStack.smoothTime);

            newPos.y = targetPos.y + ownerStack.stackOffset + element.offsetOverride;
            newPos.z = targetPos.z;
            tr.position = newPos;
        }

        public void FollowRotation(Stackable element)
        {
            if (!ownerStack.followRotation) return;
            if (element == null) return;

            var tr = transform;
            var targetTr = element.transform;

            tr.LookAt(targetTr.position, targetTr.up);
        }

        public void FollowScale(Stackable element)
        {
            if (!ownerStack.followScale) return;
            if (!element) return;

            var tr = transform;
            var scale = tr.localScale;
            var targetScale = element.transform.localScale;

            tr.localScale = Vector3.SmoothDamp(scale, targetScale, ref _scaleVelocity, ownerStack.smoothTime,
                ownerStack.maxSpeed);
        }
    }
}