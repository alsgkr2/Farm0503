using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fade_In_Out : MonoBehaviour
{
    public float fadeSpeed;


    public Image FadeInOut; //Image 컴포넌트

    public bool isFading; // 페이드 중인지 여부
    private float currentAlpha; // 현재 알파값
    public static Fade_In_Out instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentAlpha = FadeInOut.color.a;
        isFading = false;

    }

    private void Update()
    {
     
    }
    public void StartFade()//실행하면 페이드인아웃 실행
    {

        if (!isFading)
        {
            StartCoroutine("FadeOut");

        }
    }

    private IEnumerator FadeOut()//아두워지기
    {
        isFading = true;

        while (currentAlpha < 2f)
        {
            currentAlpha += fadeSpeed * Time.deltaTime; // 페이드 속도만큼 알파값 증가
            FadeInOut.color = new Color(FadeInOut.color.r, FadeInOut.color.g, FadeInOut.color.b, currentAlpha); // UI 이미지의 알파값 설정
            yield return null; // 한 프레임 대기
        }
        StartCoroutine("FadeIn");
       
    }

    private IEnumerator FadeIn()//밝아지기
    {
        while (currentAlpha > 0f)
        {
            currentAlpha -= fadeSpeed * Time.deltaTime; // 페이드 속도만큼 알파값 감소
            FadeInOut.color = new Color(FadeInOut.color.r, FadeInOut.color.g, FadeInOut.color.b, currentAlpha); // UI 이미지의 알파값 설정
            yield return null; // 한 프레임 대기
        }

        isFading = false;
    }


}
