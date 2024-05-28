using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.TeleportationManager
{
    public class TeleportationManager : MonoBehaviour
    {
        [SerializeField] private PortalPair[] portalPairs;

        private void Start()
        {
            InitPortals();
        }

        private void InitPortals()
        {
            foreach (PortalPair pair in portalPairs)
            {
                pair.From.OnPlayerEnter += offset =>
                {
                    DisablePortalPair(pair, true);
                    TeleportPlayer(
                        pair,
                        Vector3.Angle(
                            pair.To.transform.forward,
                            pair.From.transform.forward
                        ),
                        offset
                    );
                };
                pair.To.OnPlayerExit += () => { DisablePortalPair(pair, false); };
            }
        }

        private void TeleportPlayer(PortalPair pair, float rotationOffset, Vector3 positionOffset)
        {
            Transform transformTo = pair.To.transform;
            Transform transformFrom = pair.From.transform;

            float angle = Vector3.Angle(transformTo.forward, transformFrom.forward);
            positionOffset = Quaternion.AngleAxis(angle, Vector3.up) * positionOffset;
            
            GameManager.GameManager.PlayerCharacter.TeleportPlayer(transformTo.position + positionOffset, rotationOffset);
        }

        private void DisablePortalPair(PortalPair pair, bool disable)
        {
            pair.From.disabled = pair.To.disabled = disable;
        }

        private void OnDrawGizmos()
        {
            foreach (PortalPair pair in portalPairs)
            {
                DrawPortal(pair.From, Color.cyan);

                DrawArrow(pair.From.transform.position,
                    pair.To.transform.position - pair.From.transform.position, Color.green);
                DrawArrow(pair.From.transform.position, pair.From.transform.forward, Color.blue);
                DrawArrow(pair.To.transform.position, pair.To.transform.forward, Color.blue);

                DrawPortal(pair.To, Color.yellow);
            }
        }

        public static void DrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f,
            float arrowHeadAngle = 20.0f)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) *
                            new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) *
                           new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }

        private void DrawPortal(TeleportPortal portal, Color color)
        {
            Gizmos.color = color;
            BoxCollider collider = portal.GetComponent<BoxCollider>();
            Transform portalTransform = portal.transform;

            Matrix4x4 rotationMatrix = Matrix4x4.TRS(portalTransform.position, portalTransform.rotation,
                portalTransform.lossyScale);
            Gizmos.matrix = rotationMatrix;

            Gizmos.DrawWireCube(
                collider.center,
                collider.size
            );

            Gizmos.matrix = Matrix4x4.identity;
        }
    }

    [Serializable]
    internal struct PortalPair
    {
        [FormerlySerializedAs("portalOne")] [SerializeField]
        internal TeleportPortal From;

        [FormerlySerializedAs("portalTwo")] [SerializeField]
        internal TeleportPortal To;
    }
}