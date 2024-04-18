using System;
using GamePlay.Anomalies;
using GamePlay.FloorManager;
using GamePlay.Player;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static AnomalyManager AnomalyManager;
        public static FloorManager FloorManager;
        public static PlayerCharacter PlayerCharacter;
        
        private void Start()
        {
            // TODO mybe change location
            Cursor.visible = false;
            
            //TODO not very optimal
            AnomalyManager = FindObjectOfType<AnomalyManager>();
            FloorManager = FindObjectOfType<FloorManager>();
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