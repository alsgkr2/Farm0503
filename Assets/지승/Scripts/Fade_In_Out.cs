using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fade_In_Out : MonoBehaviour
{
    public float fadeSpeed;


    public Image FadeInOut; //Image ������Ʈ

    public bool isFading; // ���̵� ������ ����
    private float currentAlpha; // ���� ���İ�
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
    public void StartFade()//�����ϸ� ���̵��ξƿ� ����
    {

        if (!isFading)
        {
            StartCoroutine("FadeOut");

        }
    }

    private IEnumerator FadeOut()//�Ƶο�����
    {
        isFading = true;

        while (currentAlpha < 2f)
        {
            currentAlpha += fadeSpeed * Time.deltaTime; // ���̵� �ӵ���ŭ ���İ� ����
            FadeInOut.color = new Color(FadeInOut.color.r, FadeInOut.color.g, FadeInOut.color.b, currentAlpha); // UI �̹����� ���İ� ����
            yield return null; // �� ������ ���
        }
        StartCoroutine("FadeIn");
       
    }

    private IEnumerator FadeIn()//�������
    {
        while (currentAlpha > 0f)
        {
            currentAlpha -= fadeSpeed * Time.deltaTime; // ���̵� �ӵ���ŭ ���İ� ����
            FadeInOut.color = new Color(FadeInOut.color.r, FadeInOut.color.g, FadeInOut.color.b, currentAlpha); // UI �̹����� ���İ� ����
            yield return null; // �� ������ ���
        }

        isFading = false;
    }


}
