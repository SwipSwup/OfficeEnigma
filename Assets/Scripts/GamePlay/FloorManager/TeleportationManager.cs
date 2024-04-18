using System;
using Core;
using GamePlay.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.FloorManager
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
                pair.portalOne.OnPlayerEnter += offset =>
                {
                    DisablePortalPair(pair, true);
                    TeleportPlayer(
                        pair.portalTwo.transform,
                        Vector3.Angle(
                            pair.portalTwo.transform.forward,
                            pair.portalOne.transform.forward
                        ),
                        offset
                    );
                };

                pair.portalTwo.OnPlayerEnter += offset =>
                {
                    DisablePortalPair(pair, true);
                    TeleportPlayer(
                        pair.portalOne.transform,
                        Vector3.Angle(
                            pair.portalOne.transform.forward,
                            pair.portalTwo.transform.forward
                        ),
                        offset
                    );
                };

                pair.portalOne.OnPlayerExit += () => { DisablePortalPair(pair, false); };

                pair.portalTwo.OnPlayerExit += () => { DisablePortalPair(pair, false); };
            }
        }

        private void TeleportPlayer(Transform portal, float rotationOffset, Vector3 positionOffset)
        {
            Debug.Log(positionOffset);
            
            GameManager.PlayerCharacter.TeleportPlayer(
                positionOffset +
                portal.position,
                rotationOffset
            );
            
            /*GameManager.PlayerCharacter.TeleportPlayer(
                portal.forward * positionOffset.z +
                portal.up * positionOffset.y +
                portal.right * positionOffset.x +
                portal.position,
                rotationOffset
            );*/
        }

        private void DisablePortalPair(PortalPair pair, bool disable)
        {
            pair.portalOne.disabled = pair.portalTwo.disabled = disable;
        }
    }

    [Serializable]
    internal struct PortalPair
    {
        [SerializeField] internal TeleportPortal portalOne;
        [SerializeField] internal TeleportPortal portalTwo;
    }
}