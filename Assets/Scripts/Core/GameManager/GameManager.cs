using System;
using Core.Anomalies;
using GamePlay.Player;
using UnityEngine;

namespace Core.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static AnomalyManager AnomalyManager;
        public static FloorManager.FloorManager FloorManager;
        public static PlayerCharacter PlayerCharacter;
        public static GameManager Game;
        
        private void Start()
        {
            Initialize();
            Verify();
            
            StartGameTimer();
        }

        private void Update()
        {
            UpdateGameTimer();
        }

        private void Initialize()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            //TODO not very optimal
            AnomalyManager = FindObjectOfType<AnomalyManager>();
            FloorManager = FindObjectOfType<FloorManager.FloorManager>();
            PlayerCharacter = FindObjectOfType<PlayerCharacter>();
            Game = this;
        }

        private void Verify()
        {
            if (AnomalyManager.HighestDefinedFloor < FloorManager.StartFloorLevel)
            {
                Debug.LogWarning("Not enough Floors defined in anomaly manager");
            }
        }

        public void CompleteRun()
        {
            StartGameTimer();
        }

        private float _elapsedGameTime = 0f;
        private bool _gameTimerActive = false;
        public void StartGameTimer() => _gameTimerActive = true;

        public void StopGameTimer()
        {
            _gameTimerActive = false;
            int minutes = Mathf.FloorToInt(_elapsedGameTime / 60F);
            int seconds = Mathf.FloorToInt(_elapsedGameTime - minutes * 60);

            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            
            Debug.Log("Run completed in " + niceTime);
        }

        private void UpdateGameTimer()
        {
            if(!_gameTimerActive)
                return;
            _elapsedGameTime += Time.deltaTime;
        }
    }
}