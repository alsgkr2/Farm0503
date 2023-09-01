using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

namespace DigitalRuby.RainMaker
{
    public class RainScript : BaseRainScript
    {
        public enum Weather { SUNNY, RAIN1, RAIN2, RAIN3 };
        public Weather currentWeather;
        public float weather_time = 4f; // 날씨 바뀌는 간격
        public int next_weather = -1;        //랜덤하게 다음 날씨 지정

        private bool rdTrigger = false;


        //public ParticleSystem vtx_lightning_NoiseTrail;

        [Tooltip("The height above the camera that the rain will start falling from")]
        public float RainHeight = 25.0f;

        [Tooltip("How far the rain particle system is ahead of the player")]
        public float RainForwardOffset = -7.0f;

        [Tooltip("The top y value of the mist particles")]
        public float RainMistHeight = 3.0f;



        private void UpdateRain()
        {
            this.weather_time -= Time.deltaTime;
            if (this.weather_time < 0)
            {
                
                switch (this.next_weather)
                {
                    case 0:
                        ChangeWeather(Weather.SUNNY);       //맑음으로 바꿔줌               
                        weather_time = 10f;                  //다음 날씨 계산(0 - 맑음, 1,2,3(세기) - 비)
                        this.next_weather = Random.Range(-1,5);
                        break;
                    case 1:
                        ChangeWeather(Weather.RAIN1);
                        weather_time = 10f;
                        this.next_weather = Random.Range(-1, 5);
                        break;
                    case 2:
                        ChangeWeather(Weather.RAIN2);
                        weather_time = 10f;
                        this.next_weather = Random.Range(-1, 5);
                        break;
                    case 3:
                        ChangeWeather(Weather.RAIN3);
                        weather_time = 10f;
                        this.next_weather = Random.Range(-1, 5);
                        //vtx_lightning_NoiseTrail.Play();    //번개
                        break;
                    case 4:
                        ChangeWeather(Weather.RAIN2);
                        weather_time = 10f;
                        this.next_weather = Random.Range(-1, 5);
                        //vtx_lightning_NoiseTrail.Stop();
                        break;
                    case 5:
                        ChangeWeather(Weather.RAIN1);
                        weather_time = 10f;
                        this.next_weather = Random.Range(-1, 5);
                        break;
                    default:
                        this.next_weather = Random.Range(-1, 5);
                        ChangeWeather(Weather.SUNNY);
                        weather_time = 10f;
                        break;
                }
            }




            // keep rain and mist above the player
            if (RainFallParticleSystem != null)
            {
                if (FollowCamera)
                {
                    var s = RainFallParticleSystem.shape;
                    s.shapeType = ParticleSystemShapeType.ConeVolume;
                    RainFallParticleSystem.transform.position = Camera.transform.position;
                    RainFallParticleSystem.transform.Translate(0.0f, RainHeight, RainForwardOffset);
                    RainFallParticleSystem.transform.rotation = Quaternion.Euler(0.0f, Camera.transform.rotation.eulerAngles.y, 0.0f);
                    if (RainMistParticleSystem != null)
                    {
                        var s2 = RainMistParticleSystem.shape;
                        s2.shapeType = ParticleSystemShapeType.Hemisphere;
                        Vector3 pos = Camera.transform.position;
                        pos.y += RainMistHeight;
                        RainMistParticleSystem.transform.position = pos;
                    }
                }
                else
                {
                    var s = RainFallParticleSystem.shape;
                    s.shapeType = ParticleSystemShapeType.Box;
                    if (RainMistParticleSystem != null)
                    {
                        var s2 = RainMistParticleSystem.shape;
                        s2.shapeType = ParticleSystemShapeType.Box;
                        Vector3 pos = RainFallParticleSystem.transform.position;
                        pos.y += RainMistHeight;
                        pos.y -= RainHeight;
                        RainMistParticleSystem.transform.position = pos;
                    }
                }
            }
        }

        protected override void Start()
        {
            currentWeather = Weather.SUNNY;
            RainIntensity = 0f;
            next_weather = 10;  // 다음날씨 비

            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            UpdateRain();
        }


        public void ChangeWeather(Weather weatherType)
        {
            if (weatherType != this.currentWeather)
            {
                switch (weatherType)
                {
                    case Weather.SUNNY:
                        currentWeather = Weather.SUNNY;
                        RainIntensity = 0f;
                        break;
                    case Weather.RAIN1:
                        currentWeather = Weather.RAIN1;
                        RainIntensity = 0.32f;
                        break;
                    case Weather.RAIN2:
                        currentWeather = Weather.RAIN2;
                        RainIntensity = 0.65f;
                        break;
                    case Weather.RAIN3:
                        currentWeather = Weather.RAIN3;
                        RainIntensity = 1f;
                        break;
                }
            }
        }
    }
}