using System;
using UnityEngine;

namespace GamePlay.Anomalies
{
    [Serializable]
    public class AnomalyWrapper
    {
        [SerializeField] internal AnomalyType type;

        [Range(0f, 100f)]
        [SerializeField] internal float probability;
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