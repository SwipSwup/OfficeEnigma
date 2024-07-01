using System;
using UnityEngine;

namespace GamePlay.Trigger
{
    public class EndTrigger : MonoBehaviour
    {
        [SerializeField] private float stopTime = 5f;
        private bool stopGame = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Timer.Timer.Singelton.ShowTime();
                stopGame = true;
            }
        }

        private float timer = 0f;
        private void Update()
        {
            if(!stopGame) return;

            timer += Time.deltaTime;

            if (timer >= stopTime)
            {
                Application.Quit();
                Debug.Log("Quit");
            }
        }
    }
}