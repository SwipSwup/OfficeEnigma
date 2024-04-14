using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Anomalies
{
    //TODO write custom inspector
    public class AnomalyManager : MonoBehaviour
    {
        [Range(0f, 100f)] [SerializeField] private float probabilityForBaseFloor;

        [SerializeField] private FloorAnomalyWrapper[] floors;

        [Header("Anomalies")] [SerializeField] private GameObject[] gimmickFloors;
        [SerializeField] private GameObject[] easyFloors;
        [SerializeField] private GameObject[] normalFloors;
        [SerializeField] private GameObject[] hardFloors;
        [SerializeField] private GameObject[] impossibleFloors;

        public int HighestDefinedFloor { get; private set; }
        private void Start()
        {
            HighestDefinedFloor = floors.Length;
        }

        public bool TryGetAnomaly(int floorLevel, out GameObject anomaly)
        {
            anomaly = null;
            float randomValue = Random.Range(0f, 100f);

            if (randomValue < probabilityForBaseFloor)
            {
                return false;
            }

            floorLevel = floorLevel > floors.Length ? floors.Length : floorLevel; 
            randomValue = Random.Range(0f, 100f);

            foreach (AnomalyWrapper anomalyWrapper in floors[floorLevel - 1].anomalies)
            {
                if (randomValue <= anomalyWrapper.probability)
                {
                    anomaly = GetRandomAnomalyFloorOfType(anomalyWrapper.type);
                    return anomaly != null;
                }

                randomValue -= anomalyWrapper.probability;
            }

            return false;
        }

        private GameObject GetRandomAnomalyFloorOfType(AnomalyType type) => type switch
        {
            AnomalyType.Gimmick => gimmickFloors[Random.Range(0, gimmickFloors.Length)],
            AnomalyType.Easy => easyFloors[Random.Range(0, easyFloors.Length)],
            AnomalyType.Normal => normalFloors[Random.Range(0, normalFloors.Length)],
            AnomalyType.Hard => hardFloors[Random.Range(0, hardFloors.Length)],
            AnomalyType.Impossible => impossibleFloors[Random.Range(0, impossibleFloors.Length)],
            _ => null
        };

        private void OnValidate()
        {
            foreach (FloorAnomalyWrapper floor in floors)
            {
                //TODO value check if its 100%
                
                
            }
        }
    }
}