using System;
using Core;
using UnityEngine;

namespace GamePlay.FloorManager
{
    public class FloorManager : MonoBehaviour
    {
        [SerializeField] private GameObject baseFloorPrimitives;
        [SerializeField] private GameObject baseFloorProps;
        
        [SerializeField]
        private int startFloorLevel = 10;
        
        private int _currentFloorLevel;

        private GameObject _loadedFloor;
        public int StartFloorLevel => startFloorLevel;

        private void Start()
        {
            Instantiate(baseFloorPrimitives);
            //_loadedFloor = Instantiate(baseFloorProps);
            SpawnFloor(startFloorLevel);
        }

        public void SpawnFloor(int floorLevel)
        {
            if (GameManager.AnomalyManager.TryGetAnomaly(floorLevel, out GameObject anomaly))
            {
                _loadedFloor = Instantiate(anomaly);
            }
        }
    }
}