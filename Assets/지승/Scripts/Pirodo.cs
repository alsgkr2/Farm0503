using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pirodo : MonoBehaviour
{
    public Farm_Move farm_Move;
    public Slider pirodo_Slider;
    public Slider hunger_Slider;
    public int p_m_Check = 0;
    public bool pirodo_Check = false;
    public bool hunger_Check = false;
    public static Pirodo instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pirodo_P_M(float p_value)
    {
        StartCoroutine("Pirodo_P_M_Corutine", p_value);
    }
    public void Hunger_P_M(float h_Value)
    {
        StartCoroutine("Hunger_P_M_Corutine", h_Value);
    }

    IEnumerator Pirodo_P_M_Corutine(int p_value_C)
    {
        float start_Value = pirodo_Slider.value; // 슬라이어 초기값 저장
        while (true)
        {

            if (p_value_C > p_m_Check) // 피로도 회복
            {
                if(pirodo_Slider.value >= pirodo_Slider.maxValue) // 피로도 회복량이 제한된 값보다 크다면
                {
                    pirodo_Slider.value = pirodo_Slider.maxValue; // 현재 피로도를 최대값으로 유지해줌
                    break;
                }
                else // 피로도 회복량이 아직 꽉 안찼으면
                {
                    pirodo_Slider.value += p_value_C * Time.deltaTime; // 피로도 회복
                }
                
                if (pirodo_Slider.value >= start_Value + p_value_C) // 현재 피로도가 초기 피로도 + 더해준 값 보다 크면 반복문 종료
                {
                    break;
                }
            }
            else // 피로도 감소
            {
                if (pirodo_Slider.value <= 0) // 피로도값이 0보다 작으면
                {
                    pirodo_Slider.value = 5; // 피로도값을 0으로 유지
                    break;
                }
                else // 피로도가 0보다 작지 않으면
                {
                    pirodo_Slider.value += p_value_C * Time.deltaTime; // 피로도값 줄여줌
                }

                if (pirodo_Slider.value <= start_Value + p_value_C) // 현재 피로도값이 초기 피로도값 + 빼준 값보다 작으면 종료
                {
                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator Hunger_P_M_Corutine(int h_value_C)
    {
        float start_Value = hunger_Slider.value; // 슬라이어 초기값 저장
        while (true)
        {
            
            if (h_value_C > p_m_Check) // 허기 회복
            {
                if(hunger_Slider.value >= hunger_Slider.maxValue) // 허기값이 최대값보다 크면
                {
                    hunger_Slider.value = hunger_Slider.maxValue; // 허기 값을 최대값으로 유지
                    break;
                }
                else // 허기값이 최대값이 아니라면
                {
                    hunger_Slider.value += h_value_C * Time.deltaTime; // 허기값 더해줌
                }
                
                if (hunger_Slider.value <= start_Value + h_value_C) // 현재 허기값이 초기 허기값 + 더해준 값보다 크면 종료
                {
                    break;
                }
            }
            else // 허기 감소
            {
                if(hunger_Slider.value <= 0) // 허기값이 0보다 작으면
                {
                    hunger_Slider.value = 5; // 허기값을 5으로 유지
                    break;
                }
                else // 허기값으 0보다 작지 않으면
                {
                    hunger_Slider.value += h_value_C * Time.deltaTime; // 허기값 줄여줌
                }
                
                if (hunger_Slider.value <= start_Value + h_value_C) // 현재 허기값이 초기 허기값 + 빼준 값보다 작으면 종료
                {
                    break;
                }
            }
            yield return null;
        }
    }
}
