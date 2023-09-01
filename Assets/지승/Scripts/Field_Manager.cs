using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field_Manager : MonoBehaviour
{
    public static Field_Manager instance;

    // Field_Manager
    //public float Water_Count = 500f;
    //public float Water_Max_Count = 1000f;
    public bool water_Ok = false;
    public bool Seed_True;
    public GameObject Seed;
    [SerializeField] Material[] field_Material;//0 -> �⺻ �� �� 1 -> �� �� ��


    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {

    }

    private void Update()
    {
        Seed_IN_Field();
        RainCheck();

    }

    public void Water_Full(bool water) // �翡 ���� �ִ� �Լ�
    {
        water_Ok = water; // �翡 ���� ���� �� �Լ��� ����Ǹ�, ������ ���� �� ü�¿� ������




    }

    void Water_None()
    {
        // �� ü���� 49���ϸ� ���� �ٲ���
        if (water_Ok == false)
        {
            gameObject.GetComponent<MeshRenderer>().material = field_Material[0];
        }
        else if (water_Ok == true)
        {
            gameObject.GetComponent<MeshRenderer>().material = field_Material[1];
        }

        if (water_Ok == false && Farm_Move.instance.sleep_Ok == true) // �� ü���� 0 �����϶� �� ����
        {
            Destroy(gameObject);
        }
    }

    /*public void Water_Full(float WaterAmount) // �翡 ���� �ִ� �Լ�
    {
        Water_Count += WaterAmount; // �翡 ���� ���� �� �Լ��� ����Ǹ�, ������ ���� �� ü�¿� ������


        if (Water_Count >= Water_Max_Count) // �� ü���� ���Ѽ��� ���صΰ� �Ѿ��� �� �ִ�ġ �־���
        {
            Water_Count = 1000f;
        }

    }*/

    /*void Water_None()
    {
        // �� ü���� 49���ϸ� ���� �ٲ���
        if (Water_Count <= 500)
        {
            gameObject.GetComponent<MeshRenderer>().material = field_Material[0];
        }
        else if (Water_Count >= 501)
        {
            gameObject.GetComponent<MeshRenderer>().material = field_Material[1];
        }

        Water_Count -= Time.deltaTime; // �� ü���� �� �����Ӹ��� �ٿ���
        if (Water_Count <= 0) // �� ü���� 0 �����϶� �� ����
        {
            Destroy(gameObject);
        }
    }*/

    void Seed_IN_Field()
    {
/*        if (GameObject.FindWithTag("Seed") != null)
        {
            Seed_True = false;
        }
        else */
        if (Seed_True == true /*&& GameObject.FindWithTag("Seed")*/)
        {
            Water_None();
        }
        else
        {

        }
    }

    void RainCheck()
    {
        if (FindObjectOfType<BaseRainScript>().RainIntensity > 0)
        {
            Water_Full(true);
            //print("�񳻸�����");
        }
        else
        {
            Water_Full(false);
            //print("�� �ȳ���");
        }
    }
}
