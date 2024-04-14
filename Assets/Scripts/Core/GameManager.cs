using System;
using GamePlay.Anomalies;
using GamePlay.FloorManager;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static AnomalyManager AnomalyManager;
        public static FloorManager FloorManager;
        
        private void Start()
        {
            AnomalyManager = FindObjectOfType<AnomalyManager>();
            FloorManager = FindObjectOfType<FloorManager>();
            
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