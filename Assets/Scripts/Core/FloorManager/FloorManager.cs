using TMPro;
using UnityEngine;

namespace Core.FloorManager
{
    public class FloorManager : MonoBehaviour
    {
        [SerializeField] private GameObject baseFloorPrimitives;
        [SerializeField] private GameObject endFloorPrimitives;
        [SerializeField] private GameObject baseFloorProps;
        [SerializeField] private GameObject endFloorProps;
        [SerializeField] private TMP_Text floorNumberText;

        [SerializeField] private int startFloorLevel = 10;

        private int _currentFloorLevel;

        private GameObject _loadedFloorProps;
        private GameObject _loadedFloorPrimitives;
        public int StartFloorLevel => startFloorLevel;
        public int CurrenFloorLevel => _currentFloorLevel;

        private void Start()
        {
            SpawnFloorPrimitives(baseFloorPrimitives);
            SpawnFloorProps(baseFloorProps);
            _currentFloorLevel = startFloorLevel;
            floorNumberText.SetText((_currentFloorLevel + 1).ToString());
        }

        public void SpawnNextFloor(string resetTag)
        {
            if (CurrenFloorLevel <= 0)
            {
                SpawnEndFloor();
                return;
            } 
            
            if (_loadedFloorProps.CompareTag(resetTag))
            {
                _currentFloorLevel = startFloorLevel;
                Debug.Log("Reset");
            }          
            
            SpawnFloorAtLevel(_currentFloorLevel--);
        }

        private void SpawnEndFloor()
        {
            SpawnFloorPrimitives(endFloorPrimitives);
            SpawnFloorProps(endFloorProps);
        }

        public void SpawnFloorAtLevel(int floorLevel)
        {
            Debug.Log("Spawn floor: " + floorLevel);
            floorNumberText.SetText(floorLevel.ToString());
            
            SpawnFloorProps(GameManager.GameManager.AnomalyManager.TryGetAnomaly(floorLevel, out GameObject anomaly)
                ? anomaly
                : baseFloorProps);
        }

        private void SpawnFloorProps(GameObject floorProps)
        {
            if(_loadedFloorProps != null)
                Destroy(_loadedFloorProps);
            
            _loadedFloorProps = Instantiate(floorProps);
        }
        
        private void SpawnFloorPrimitives(GameObject floorPrimitives)
        {
            if(_loadedFloorPrimitives != null)
                Destroy(_loadedFloorPrimitives);
            
            _loadedFloorPrimitives = Instantiate(floorPrimitives);
        }
    }
}