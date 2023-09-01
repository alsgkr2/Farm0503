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
    [SerializeField] Material[] field_Material;//0 -> 기본 밭 색 1 -> 물 준 색


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

    public void Water_Full(bool water) // 밭에 물을 주는 함수
    {
        water_Ok = water; // 밭에 물을 줬을 때 함수가 실행되며, 정해진 값을 밭 체력에 더해줌




    }

    void Water_None()
    {
        // 밭 체력이 49이하면 색깔 바꿔줌
        if (water_Ok == false)
        {
            gameObject.GetComponent<MeshRenderer>().material = field_Material[0];
        }
        else if (water_Ok == true)
        {
            gameObject.GetComponent<MeshRenderer>().material = field_Material[1];
        }

        if (water_Ok == false && Farm_Move.instance.sleep_Ok == true) // 밭 체력이 0 이하일때 밭 삭제
        {
            Destroy(gameObject);
        }
    }

    /*public void Water_Full(float WaterAmount) // 밭에 물을 주는 함수
    {
        Water_Count += WaterAmount; // 밭에 물을 줬을 때 함수가 실행되며, 정해진 값을 밭 체력에 더해줌


        if (Water_Count >= Water_Max_Count) // 밭 체력의 상한선을 정해두고 넘었을 때 최대치 넣어줌
        {
            Water_Count = 1000f;
        }

    }*/

    /*void Water_None()
    {
        // 밭 체력이 49이하면 색깔 바꿔줌
        if (Water_Count <= 500)
        {
            gameObject.GetComponent<MeshRenderer>().material = field_Material[0];
        }
        else if (Water_Count >= 501)
        {
            gameObject.GetComponent<MeshRenderer>().material = field_Material[1];
        }

        Water_Count -= Time.deltaTime; // 밭 체력을 매 프레임마다 줄여줌
        if (Water_Count <= 0) // 밭 체력이 0 이하일때 밭 삭제
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
            //print("비내리는중");
        }
        else
        {
            Water_Full(false);
            //print("비 안내림");
        }
    }
}
