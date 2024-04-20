using Core.Anomalies;
using GamePlay.Player;
using UnityEngine;

namespace Core.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static AnomalyManager AnomalyManager;
        public static FloorManager.FloorManager FloorManager;
        public static PlayerCharacter PlayerCharacter;
        
        private void Start()
        {
            // TODO mybe change location
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            //TODO not very optimal
            AnomalyManager = FindObjectOfType<AnomalyManager>();
            FloorManager = FindObjectOfType<FloorManager.FloorManager>();
            PlayerCharacter = FindObjectOfType<PlayerCharacter>();
            
            Verify();
        }

        private void Verify()
        {
            if (AnomalyManager.HighestDefinedFloor < FloorManager.StartFloorLevel)
            {
                Debug.LogWarning("Not enough Floors defined in anomaly manager");
            }
        }
    }
}