using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Anomalies
{
    [Serializable]
    public class FloorAnomalyWrapper
    {
        [SerializeField]
        public AnomalyWrapper[] anomalies =
        {
            new() { type = AnomalyType.Gimmick, probability = 0f },
            new() { type = AnomalyType.Easy, probability = 0f },
            new() { type = AnomalyType.Normal, probability = 0f },
            new() { type = AnomalyType.Hard, probability = 0f },
            new() { type = AnomalyType.Impossible, probability = 0f }
        };
    }
    
    [Serializable]
    public struct AnomalyWrapper
    {
        [SerializeField] public AnomalyType type;

        [SerializeField] public float probability;
    }

    [Serializable]
    public enum AnomalyType
    {
        Gimmick,
        Easy,
        Normal,
        Hard,
        Impossible
    }
}