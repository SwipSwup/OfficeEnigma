using System;
using UnityEngine;

namespace Core.GameManager
{
    [RequireComponent(typeof(BoxCollider))]
    public class GameEndTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Game.CompleteRun();
            }
        }
    }
}