using System;
using UnityEngine;

namespace GamePlay.Anomalies
{
    [Serializable]
    public class FloorAnomalyWrapper
    {
        [SerializeField]
        internal AnomalyWrapper[] anomalies;
    }
}