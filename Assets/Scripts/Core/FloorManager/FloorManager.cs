using UnityEngine;

namespace Core.FloorManager
{
    public class FloorManager : MonoBehaviour
    {
        [SerializeField] private GameObject baseFloorPrimitives;
        [SerializeField] private GameObject baseFloorProps;

        [SerializeField] private int startFloorLevel = 10;

        private int _currentFloorLevel;

        private GameObject _loadedFloor;
        public int StartFloorLevel => startFloorLevel;
        public int CurrenFloorLevel => _currentFloorLevel;

        private void Start()
        {
            Instantiate(baseFloorPrimitives);
            _loadedFloor = Instantiate(baseFloorProps);
            _currentFloorLevel = startFloorLevel;
        }

        public void SpawnNextFloor(bool resetIfAnomaly)
        {
            Debug.Log(_currentFloorLevel);

            if (resetIfAnomaly && _loadedFloor.CompareTag("Anomaly"))
            {
                _currentFloorLevel = startFloorLevel;
                Debug.Log("Reset");
            }          
            
            SpawnFloorAtLevel(_currentFloorLevel--);
        }

        public void SpawnFloorAtLevel(int floorLevel)
        {
            SpawnFloor(GameManager.GameManager.AnomalyManager.TryGetAnomaly(floorLevel, out GameObject anomaly)
                ? anomaly
                : baseFloorProps);
        }

        private void SpawnFloor(GameObject floor)
        {
            if(_loadedFloor != null)
                Destroy(_loadedFloor);
            
            _loadedFloor = Instantiate(floor);
        }
    }
}