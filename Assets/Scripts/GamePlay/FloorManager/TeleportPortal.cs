using System;
using UnityEditor;
using UnityEngine;

namespace GamePlay.FloorManager
{
    [RequireComponent(typeof(BoxCollider))]
    public class TeleportPortal : MonoBehaviour
    {
        public Action<Vector3> OnPlayerEnter;
        public Action OnPlayerExit;

        public bool disabled;

        private void OnTriggerEnter(Collider other)
        {
            if (disabled)
                return;

            if (other.CompareTag("Player"))
            {
                OnPlayerEnter?.Invoke(CalculateOffset(other.gameObject.transform));
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

            float angle = Vector3.Angle(transform.forward, offset);
            float distance = Vector3.Distance(transform.position, other.position);

            return new Vector3(
                Mathf.Cos(angle) * distance,
                offset.y,
                Mathf.Sin(angle) * distance
            );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            BoxCollider collider = GetComponent<BoxCollider>();

            Gizmos.DrawCube(transform.position + collider.center, collider.size);
        }
    }
}