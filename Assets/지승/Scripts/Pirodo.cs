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
        float start_Value = pirodo_Slider.value; // �����̾� �ʱⰪ ����
        while (true)
        {

            if (p_value_C > p_m_Check) // �Ƿε� ȸ��
            {
                if(pirodo_Slider.value >= pirodo_Slider.maxValue) // �Ƿε� ȸ������ ���ѵ� ������ ũ�ٸ�
                {
                    pirodo_Slider.value = pirodo_Slider.maxValue; // ���� �Ƿε��� �ִ밪���� ��������
                    break;
                }
                else // �Ƿε� ȸ������ ���� �� ��á����
                {
                    pirodo_Slider.value += p_value_C * Time.deltaTime; // �Ƿε� ȸ��
                }
                
                if (pirodo_Slider.value >= start_Value + p_value_C) // ���� �Ƿε��� �ʱ� �Ƿε� + ������ �� ���� ũ�� �ݺ��� ����
                {
                    break;
                }
            }
            else // �Ƿε� ����
            {
                if (pirodo_Slider.value <= 0) // �Ƿε����� 0���� ������
                {
                    pirodo_Slider.value = 5; // �Ƿε����� 0���� ����
                    break;
                }
                else // �Ƿε��� 0���� ���� ������
                {
                    pirodo_Slider.value += p_value_C * Time.deltaTime; // �Ƿε��� �ٿ���
                }

                if (pirodo_Slider.value <= start_Value + p_value_C) // ���� �Ƿε����� �ʱ� �Ƿε��� + ���� ������ ������ ����
                {
                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator Hunger_P_M_Corutine(int h_value_C)
    {
        float start_Value = hunger_Slider.value; // �����̾� �ʱⰪ ����
        while (true)
        {
            
            if (h_value_C > p_m_Check) // ��� ȸ��
            {
                if(hunger_Slider.value >= hunger_Slider.maxValue) // ��Ⱚ�� �ִ밪���� ũ��
                {
                    hunger_Slider.value = hunger_Slider.maxValue; // ��� ���� �ִ밪���� ����
                    break;
                }
                else // ��Ⱚ�� �ִ밪�� �ƴ϶��
                {
                    hunger_Slider.value += h_value_C * Time.deltaTime; // ��Ⱚ ������
                }
                
                if (hunger_Slider.value <= start_Value + h_value_C) // ���� ��Ⱚ�� �ʱ� ��Ⱚ + ������ ������ ũ�� ����
                {
                    break;
                }
            }
            else // ��� ����
            {
                if(hunger_Slider.value <= 0) // ��Ⱚ�� 0���� ������
                {
                    hunger_Slider.value = 5; // ��Ⱚ�� 5���� ����
                    break;
                }
                else // ��Ⱚ�� 0���� ���� ������
                {
                    hunger_Slider.value += h_value_C * Time.deltaTime; // ��Ⱚ �ٿ���
                }
                
                if (hunger_Slider.value <= start_Value + h_value_C) // ���� ��Ⱚ�� �ʱ� ��Ⱚ + ���� ������ ������ ����
                {
                    break;
                }
            }
            yield return null;
        }
    }
}
