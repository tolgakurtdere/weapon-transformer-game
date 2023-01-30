using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LoxiGames.Utility.Stack
{
    public class MyStack : MonoBehaviour
    {
        public float maxSpeed = 120f;
        public float stackOffset = 1f;
        public float smoothTime = 0.125f;
        public bool followPosition;
        public bool followRotation;
        public bool followScale;

        [field: ShowInInspector, ReadOnly] private List<Stackable> StackElements { get; } = new();

        private void LateUpdate()
        {
            StackElements.ForEach(s =>
            {
                s.FollowPosition(s.prev);
                s.FollowRotation(s.prev);
                s.FollowScale(s.next);
            });
        }

        public void AddStackElement(Stackable element)
        {
            Stackable lastElement = null;
            if (StackElements.Count > 0)
            {
                lastElement = StackElements[^1];
            }
            else
            {
                var elementTr = element.transform;
                elementTr.SetParent(transform);
                elementTr.localPosition = Vector3.zero;
                elementTr.localRotation = Quaternion.identity;
            }

            element.ownerStack = this;
            element.next = null;
            element.prev = lastElement;
            if (lastElement) lastElement.next = element;
            StackElements.Add(element);
        }

        public void RemoveStackElement(Stackable element)
        {
            if (!StackElements.Contains(element)) return;

            var nextElement = element.next;
            var prevElement = element.prev;
            if (nextElement) nextElement.prev = prevElement;
            if (prevElement) prevElement.next = nextElement;

            element.next = null;
            element.prev = null;
            element.ownerStack = null;
            StackElements.Remove(element);
        }

        public void RemoveStackElementsFront(Stackable startElement,
            Action<Stackable> onElementRemoved = null)
        {
            if (!StackElements.Contains(startElement)) return;

            var initIndex = StackElements.IndexOf(startElement);
            for (var i = StackElements.Count - 1; i >= initIndex; i--)
            {
                var element = StackElements[i];
                var prevElement = element.prev;
                if (prevElement) prevElement.next = null;
                element.next = null;
                element.prev = null;
                element.ownerStack = null;
                onElementRemoved?.Invoke(StackElements[i]);
                StackElements.Remove(StackElements[i]);
            }
        }

        public void RemoveStackElementsFront(int count, Action<Stackable> onElementRemoved = null)
        {
            if (count > StackElements.Count) return;
            var limit = StackElements.Count - count;

            for (var i = StackElements.Count - 1; i >= limit; i--)
            {
                var element = StackElements[i];
                var prevElement = element.prev;
                if (prevElement) prevElement.next = null;
                element.next = null;
                element.prev = null;
                element.ownerStack = null;
                onElementRemoved?.Invoke(StackElements[i]);
                StackElements.Remove(StackElements[i]);
            }
        }
    }
}