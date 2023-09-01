using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;

    public float Loss_Work_Time;//줄어드는 작업 시간 
    public float Loss_pirodo_Amount;//줄어드는 피로도량
    public float Plus_Furit_Amount;

    public int pirodo = 0; // 피로도 감소 변수
    public int hunger = 0; // 허기 감소 변수

    private void Start()
    {
        instance = this;
    }

}
