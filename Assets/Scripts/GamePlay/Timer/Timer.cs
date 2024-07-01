using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private RawImage backGround;
        [SerializeField] private TMP_Text timeText;
        
        private float time = 0f;

        public static Timer Singelton;

        private void Start()
        {
            Singelton = this;
        }

        private void Update()
        {
            
            time += Time.deltaTime;
        }

        public void ShowTime()
        {
            backGround.gameObject.SetActive(true);

            LeanTween.value(gameObject, f =>
            {
                backGround.color =
                    new Color(backGround.color.r, backGround.color.g, backGround.color.b, f);
            }, 0f, 1f, 1f);
            LeanTween.value(gameObject,
                f => { timeText.color = new Color(timeText.color.r, timeText.color.g, timeText.color.b, f); }, 0f, 1f,
                2f);
            timeText.SetText(TimeSpan.FromSeconds(time).ToString("hh':'mm':'ss'.'fff"));
        }
    }
}