using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;

    public float Loss_Work_Time;//�پ��� �۾� �ð� 
    public float Loss_pirodo_Amount;//�پ��� �Ƿε���
    public float Plus_Furit_Amount;

    public int pirodo = 0; // �Ƿε� ���� ����
    public int hunger = 0; // ��� ���� ����

    private void Start()
    {
        instance = this;
    }

}
