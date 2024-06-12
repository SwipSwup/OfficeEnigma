using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.TeleportationManager
{
    [RequireComponent(typeof(BoxCollider))]
    public class TeleportPortal : MonoBehaviour
    {
        public Action<Vector3> OnPlayerEnter;
        public Action OnPlayerExit;

        public UnityEvent OnTeleport;

        public bool disabled;

        private void OnTriggerEnter(Collider other)
        {
            if (disabled)
                return;

            if (other.CompareTag("Player"))
            {
                OnPlayerEnter?.Invoke(CalculateOffset(other.gameObject.transform));
                OnTeleport?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerExit?.Invoke();
            }
        }

        private Vector3 CalculateOffset(Transform other)
        {
            Vector3 offset = other.position - transform.position;
            return offset;
        }
    }
}